using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExplorationMenu : ACMenu
{
    private LineRendererUI _right;
    private LineRendererUI _up;
    private LineRendererUI _hypotenuse;

    [SerializeField] private Transform linesRoot;

    [SerializeField] private Vector3 bottomLeftVertex = Vector3.zero;
    [SerializeField] private Vector3 rightVertex = Vector3.right * 10f;
    [SerializeField] private Vector3 upVertex = Vector3.up * 10f;
    [SerializeField] private float width = 1.0f;
    [SerializeField] private Sprite lineSprite;

    [SerializeField] private float bias = 1.0f;

    [SerializeField] private VertexHandle handleRight;
    [SerializeField] private VertexHandle handleUp;

    [SerializeField] private Button backButton;

    [SerializeField] private TextMeshProUGUI hypoLabel;
    [SerializeField] private TextMeshProUGUI rightLabel;
    [SerializeField] private TextMeshProUGUI upLabel;

    [SerializeField] private int decimalPlaces = 1;
    [SerializeField] private float divisor = 5f;


    [SerializeField] private TextMeshProUGUI pythaLabel;

    public override void Open()
    {
        base.Open();

        handleRight.transform.localPosition = rightVertex;
        handleUp.transform.localPosition = upVertex;

        handleRight.onPositionChanged += newPos =>
        {
            rightVertex = newPos;
            UpdateLines();
            UpdateLabels();
        };

        handleUp.onPositionChanged += newPos =>
        {
            upVertex = newPos;
            UpdateLines();
            UpdateLabels();
        };

        GenerateLines();
        
        GenerateRect(_right, "rightRect", true);
        GenerateRect(_up, "upRect");
        GenerateRect(_hypotenuse, "hypoRect");
        
        UpdateLines();
        InitLabels();
        UpdateLabels();
        

        UpdateRect(_right);
        UpdateRect(_up);
        UpdateRect(_hypotenuse);
        
    }

    private void InitLabels()
    {
        hypoLabel.rectTransform.SetParent(_hypotenuse.rectTransform);
        rightLabel.rectTransform.SetParent(_right.rectTransform);
        upLabel.rectTransform.SetParent(_up.rectTransform);
        hypoLabel.transform.localPosition = Vector3.zero;
        rightLabel.transform.localPosition = Vector3.zero;
        upLabel.transform.localPosition = Vector3.zero;
    }

    private void UpdateLabels()
    {
        if (!_right || !_up || !_hypotenuse)
            return;

        float rightValue = (float) Math.Round(_right.rectTransform.sizeDelta.x / divisor, decimalPlaces);
        rightLabel.text = rightValue.ToString($"F{decimalPlaces}");

        float upValue = (float) Math.Round(_up.rectTransform.sizeDelta.x / divisor, decimalPlaces);
        upLabel.text = upValue.ToString($"F{decimalPlaces}");

        float hypoValue =
            (float) Math.Round(Mathf.Sqrt((rightValue * rightValue) + (upValue * upValue)), decimalPlaces);
        hypoLabel.text = hypoValue.ToString($"F{decimalPlaces}");
        hypoLabel.rectTransform.rotation = Quaternion.identity;

        pythaLabel.text =
            "<color=red>A<sup>2</sup></color> + <color=green>B<sup>2</sup></color> = <color=blue>C<sup>2</sup></color>";

        pythaLabel.text +=
            $"\n<color=red>{rightValue.ToString($"F{decimalPlaces}")}<sup>2</sup></color> + <color=green>{upValue.ToString($"F{decimalPlaces}")}<sup>2</sup></color> = <color=blue>" +
            $"{((rightValue * rightValue) + (upValue * upValue)).ToString($"F{decimalPlaces}")}" + "</color>";
        pythaLabel.text +=
            $"\n \u221a(<color=red>{(rightValue * rightValue).ToString($"F{decimalPlaces}")}</color> + <color=green>{(upValue * upValue).ToString($"F{decimalPlaces}")}</color>) = <color=blue>{hypoValue.ToString($"F{decimalPlaces}")}</color>";
    }

    private void GenerateLines()
    {
        if (_right)
            Destroy(_right.gameObject);

        if (_up)
            Destroy(_up.gameObject);

        if (_hypotenuse)
            Destroy(_hypotenuse.gameObject);

        _right = CreateLine(bottomLeftVertex, rightVertex, Color.red, "RightLine");
        _up = CreateLine(bottomLeftVertex, upVertex, Color.green, "UpLine");
        _hypotenuse = CreateLine(upVertex, rightVertex, Color.blue, "Hypotenuse");
    }

    private void GenerateRect(LineRendererUI line, string rectName = "rect", bool flipped = false)
    {
        GameObject rect = new GameObject(rectName, typeof(RectTransform));
        rect.transform.SetParent(line.transform);

        rect.transform.localPosition = Vector3.zero;
        rect.transform.localRotation = Quaternion.identity;
        rect.transform.localScale = Vector3.one;

        line.rect = rect;

        rect.AddComponent<Image>();
        
        ((RectTransform) rect.transform).pivot = new Vector2(0.5f, flipped ? 1.0f : 0.0f);
    }

    private void UpdateRect(LineRendererUI line)
    {
        GameObject rect = line.rect;

        Image image = rect.GetComponent<Image>();
        image.color = new Color(line.image.color.r, line.image.color.g, line.image.color.b, 0.5f);

        RectTransform rectTransform = rect.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(line.rectTransform.sizeDelta.x, line.rectTransform.sizeDelta.x);
    }

    private void UpdateLines()
    {
        if (!_right || !_up || !_hypotenuse)
            return;

        _right.UpdateLine(bottomLeftVertex + (bottomLeftVertex - rightVertex).normalized * bias,
            rightVertex + (rightVertex - bottomLeftVertex).normalized * bias, width);
        _up.UpdateLine(bottomLeftVertex + (bottomLeftVertex - upVertex).normalized * bias,
            upVertex + (upVertex - bottomLeftVertex).normalized * bias, width);
        _hypotenuse.UpdateLine(upVertex + (upVertex - rightVertex).normalized * bias,
            rightVertex + (rightVertex - upVertex).normalized * bias, width);

        UpdateRect(_right);
        UpdateRect(_up);
        UpdateRect(_hypotenuse);
    }

    private void OnValidate()
    {
        if (!Application.isPlaying)
            return;

        UpdateLines();
        UpdateLabels();
    }

    private LineRendererUI CreateLine(Vector3 start, Vector3 end, Color color, string lineName = "Line")
    {
        GameObject line = new GameObject(lineName, typeof(RectTransform));
        RectTransform rectTransform = line.GetComponent<RectTransform>();
        rectTransform.SetParent(linesRoot);
        rectTransform.sizeDelta = new Vector2(1, 1);

        Image image = line.AddComponent<Image>();
        image.color = color;
        image.sprite = lineSprite;
        image.type = Image.Type.Sliced;
        // Set Raw Image


        LineRendererUI lineRenderer = line.AddComponent<LineRendererUI>();
        lineRenderer.image = image;
        lineRenderer.rectTransform = rectTransform;
        lineRenderer.UpdateLine(start + (start - end).normalized * bias, end + (end - start).normalized * bias, width);

        return lineRenderer;
    }


    public override void Close()
    {
        base.Close();
    }

    private void OnEnable()
    {
        backButton.onClick.AddListener(OnBackClicked);
    }

    private void OnBackClicked()
    {
        MainCanvas.Instance.CloseMenu();
    }

    private void OnDisable()
    {
        backButton.onClick.RemoveAllListeners();
    }
}