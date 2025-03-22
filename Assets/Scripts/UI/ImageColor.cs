using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageColor : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float colorTransitionSpeed = 0.01f;

    private Image img;
    private Color originalColor;
    private Coroutine co = null;

    void Start()
    {
        if (TryGetComponent(out Image img))
        {
            this.img = img;
            originalColor = img.color;
        }
        else
            Debug.LogError("Missing Image component!");
    }
    
    public void SetColor(Color c)
    {
        img.color = c;
    }

    public void ResetColor()
    {
        img.color = originalColor;
    }

    public void SetColor(Color c, float delay)
    {
        if (co != null)
        {
            StopCoroutine(co);
        }
        co = StartCoroutine(SetColorWithDelay(c, delay));
    }

    private IEnumerator SetColorWithDelay(Color c, float delay)
    {
        img.color = c;
        yield return new WaitForSeconds(delay);

        float t = 0;
        while (img.color != originalColor)
        {
            img.color = Color.Lerp(img.color, originalColor, t);
            t += colorTransitionSpeed;
            yield return null;
        }
    }
}
