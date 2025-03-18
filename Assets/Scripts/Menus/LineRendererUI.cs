using UnityEngine;
using UnityEngine.UI;

public class LineRendererUI : MonoBehaviour
{
    public RectTransform rectTransform;
    public Image image;

    public void CreateLine(Vector3 startPos, Vector3 endPos, float width = 1.0f)
    {
        Vector2 p1 = new Vector2(endPos.x, endPos.y);
        Vector2 p2 = new Vector2(startPos.x, startPos.y);


        Vector2 midpoint = (p1 + p2) / 2f;
        rectTransform.localPosition = new Vector3(midpoint.x, midpoint.y, 0f);

        Vector2 dir = p1 - p2;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rectTransform.rotation = Quaternion.Euler(0f, 0f, angle);

        rectTransform.sizeDelta = new Vector2(dir.magnitude, width);
        rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }
}