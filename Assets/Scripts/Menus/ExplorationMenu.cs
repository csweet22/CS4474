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

    [SerializeField] private Vector3 bottomLeftVertex = Vector3.zero;
    [SerializeField] private Vector3 rightVertex = Vector3.right * 10f;
    [SerializeField] private Vector3 upVertex = Vector3.up * 10f;

    public override void Open()
    {
        base.Open();
        _bottom = CreateLine(bottomLeftVertex, rightVertex, Color.red, "BottomLine");
        _left = CreateLine(bottomLeftVertex, upVertex, Color.green, "LeftLine");
        _hypotenuse = CreateLine(upVertex, rightVertex, Color.blue, "Hypotenuse");
    }

    private void OnValidate()
    {
        if (!Application.isPlaying)
            return;

        if (_bottom)
            Destroy(_bottom.gameObject);
        if (_left)
            Destroy(_left.gameObject);
        if (_hypotenuse)
            Destroy(_hypotenuse.gameObject);

        _bottom = CreateLine(bottomLeftVertex, rightVertex, Color.red, "BottomLine");
        _left = CreateLine(bottomLeftVertex, upVertex, Color.green, "LeftLine");
        _hypotenuse = CreateLine(upVertex, rightVertex, Color.blue, "Hypotenuse");
    }

    private LineRendererUI CreateLine(Vector3 start, Vector3 end, Color color, string lineName = "Line")
    {
        GameObject line = new GameObject(lineName, typeof(RectTransform));
        RectTransform rectTransform = line.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(5, 5);
        rectTransform.SetParent(transform);

        RawImage image = line.AddComponent<RawImage>();
        image.color = color;
        // Set Raw Image


        LineRendererUI lineRenderer = line.AddComponent<LineRendererUI>();
        lineRenderer.image = image;
        lineRenderer.rectTransform = rectTransform;
        lineRenderer.CreateLine(start, end);

        return lineRenderer;
    }

    public override void Close()
    {
        base.Close();
    }
}