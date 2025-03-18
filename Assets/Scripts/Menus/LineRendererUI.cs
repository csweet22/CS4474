using UnityEngine;
using UnityEngine.UI;

public class LineRendererUI : MonoBehaviour
{
    public RectTransform rectTransform;
    public RawImage image;

    public void CreateLine(Vector3 startPos, Vector3 endPos)
    {
        Vector2 p1 = new Vector2(endPos.x, endPos.y);
        Vector2 p2 = new Vector2(startPos.x, startPos.y);
        Vector2 midpoint = (p1 + p2) / 2f;

        rectTransform.position = midpoint;

        Vector2 dir = p1 - p2;
        rectTransform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        rectTransform.localScale = new Vector3(dir.magnitude, 1f, 1f);
    }
}