using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float colorTransitionSpeed = 0.01f;

    private Image img;
    private Color originalColor;
    private Coroutine co = null;

    void Start()
    {
        if (!TryGetComponent(out Button _))
            Debug.LogWarning("Object is not a button!");

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
        if (co != null)
        {
            StopCoroutine(co);
        }
        img.color = c;
    }

    public void ResetColor()
    {
        if (co != null)
        {
            StopCoroutine(co);
        }
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

    public void SetVisible(bool visible)
    {
        img.enabled = visible;

        Button button = GetComponentInChildren<Button>();
        if (button)
            button.enabled = visible;
        
        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
        if (text)
            text.enabled = visible;
    }
}
