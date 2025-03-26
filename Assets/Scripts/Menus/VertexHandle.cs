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

    [SerializeField] private bool freezeX = false;
    [SerializeField] private bool freezeY = false;
    [SerializeField] private bool freezeZ = false;

    [SerializeField] private Transform minPosition, maxPosition;

    public event Action<Vector3> onPositionChanged;

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
            if (!freezeX)
                rt.position = rt.position.Change(x: globalMousePos.x);
            if (!freezeY)
                rt.position = rt.position.Change(y: globalMousePos.y);
            if (!freezeZ)
                rt.position = rt.position.Change(z: globalMousePos.z);

            if (minPosition){
                if (rt.position.x < minPosition.position.x)
                    rt.position = rt.position.Change(x: minPosition.position.x);
                if (rt.position.y < minPosition.position.y)
                    rt.position = rt.position.Change(y: minPosition.position.y);
                if (rt.position.z < minPosition.position.z)
                    rt.position = rt.position.Change(z: minPosition.position.z);
            }

            if (maxPosition){
                if (rt.position.x > maxPosition.position.x)
                    rt.position = rt.position.Change(x: maxPosition.position.x);
                if (rt.position.y > maxPosition.position.y)
                    rt.position = rt.position.Change(y: maxPosition.position.y);
                if (rt.position.z > maxPosition.position.z)
                    rt.position = rt.position.Change(z: maxPosition.position.z);
            }

            if (onPositionChanged != null) onPositionChanged.Invoke(rt.localPosition);
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