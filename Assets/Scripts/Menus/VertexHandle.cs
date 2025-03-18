using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VertexHandle : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool dragOnSurfaces = true;
    private RectTransform _rt;

    public void OnBeginDrag(PointerEventData eventData)
    {
        var canvas = FindInParents<Canvas>(gameObject);
        if (canvas == null)
            return;


        if (dragOnSurfaces)
            _rt = transform as RectTransform;
        else
            _rt = canvas.transform as RectTransform;

        SetDraggedPosition(eventData);
    }

    public void OnDrag(PointerEventData data)
    {
        SetDraggedPosition(data);
    }

    private void SetDraggedPosition(PointerEventData data)
    {
        if (dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
            _rt = data.pointerEnter.transform as RectTransform;

        var rt = GetComponent<RectTransform>();
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_rt, data.position,
                data.pressEventCamera, out var globalMousePos)){
            rt.position = globalMousePos;
            rt.rotation = _rt.rotation;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    private static T FindInParents<T>(GameObject go) where T : Component
    {
        if (go == null) return null;
        var comp = go.GetComponent<T>();

        if (comp != null)
            return comp;

        Transform t = go.transform.parent;
        while (t != null && comp == null){
            comp = t.gameObject.GetComponent<T>();
            t = t.parent;
        }

        return comp;
    }
}