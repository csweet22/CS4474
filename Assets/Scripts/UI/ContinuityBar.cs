using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContinuityBar : MonoBehaviour
{
    [SerializeField] private GameObject node;
    [SerializeField] private GameObject line;

    private Trackable<int> _currentStep = new Trackable<int>(0);
    private int _totalSteps = 0;

    private GameObject[] _nodes;
    private GameObject[] _lines;

    public void CompleteTo(int step)
    {
        for (int i = 0; i < step; i++){
            if (_nodes[i])
                _nodes[i].GetComponent<Image>().color = new Color(0.14f, 0.8f, 1.0f, 1f);
            if (_lines[i])
                _lines[i].GetComponent<RawImage>().color = new Color(0.14f, 0.8f, 1.0f, 1f);
        }
    }

    public void Init(int total)
    {
        _totalSteps = total;

        _nodes = new GameObject[_totalSteps];
        _lines = new GameObject[_totalSteps];

        for (int i = 0; i < _totalSteps; i++){
            GameObject newNode;
            if (i == _totalSteps - 1){
                newNode = Instantiate(node, transform);
            }
            else{
                newNode = Instantiate(node, transform);
                GameObject newLine = Instantiate(line, transform);
                newLine.transform.localScale = new Vector3(1.5f, 1, 1);
                _lines[i] = newLine;
                ((RectTransform) newLine.transform).sizeDelta = new Vector2(
                    ((RectTransform) newLine.transform).sizeDelta.x / _totalSteps,
                    ((RectTransform) newLine.transform).sizeDelta.y);
            }

            _nodes[i] = newNode;

            TextMeshProUGUI tmp = newNode.GetComponentInChildren<TextMeshProUGUI>();
            tmp.text = (i + 1).ToString();
        }
    }
}