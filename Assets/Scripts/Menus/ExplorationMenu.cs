using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplorationMenu : ACMenu
{
    private LineRendererUI _bottom;
    private LineRendererUI _left;
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

    public override void Open()
    {
        base.Open();
        
        handleRight.transform.localPosition = rightVertex;
        handleUp.transform.localPosition = upVertex;

        handleRight.onPositionChanged += newPos =>
        {
            rightVertex = newPos;
            GenerateLines();
        };

        handleUp.onPositionChanged += newPos =>
        {
            upVertex = newPos;
            GenerateLines();
        };

        GenerateLines();
    }

    private void GenerateLines()
    {
        if (_bottom)
            Destroy(_bottom.gameObject);

        if (_left)
            Destroy(_left.gameObject);

        if (_hypotenuse)
            Destroy(_hypotenuse.gameObject);

        _bottom = CreateLine(bottomLeftVertex, rightVertex, Color.red, "RightLine");
        _left = CreateLine(bottomLeftVertex, upVertex, Color.green, "UpLine");
        _hypotenuse = CreateLine(upVertex, rightVertex, Color.blue, "Hypotenuse");
    }

    private void OnValidate()
    {
        if (!Application.isPlaying)
            return;

        GenerateLines();
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
}