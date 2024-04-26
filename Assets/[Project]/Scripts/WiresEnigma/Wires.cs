using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Wires : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler 

{
    public bool isLeftWire;
    public Color customColor;
    private Image _image;
    private LineRenderer _lineRenderer;
    private Canvas _canvas;
    private bool _isDragStarted = false;
    private WireTask _wireTask;
    public bool isSuccess = false;
    private void Awake() 
    {
        _image = GetComponent<Image>();
        _lineRenderer = GetComponent<LineRenderer>();
        _canvas = GetComponentInParent<Canvas>();
        _wireTask = GetComponentInParent<WireTask>();
    }
    private void Update() 
    {
        if (_isDragStarted)
        {
            Vector2 movePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.transform as RectTransform,
                Input.mousePosition,
                _canvas.worldCamera,
                out movePos);
            
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, _canvas.transform.TransformPoint(movePos));

        }
        else 
        {
            if(!isSuccess)
            {
                _lineRenderer.SetPosition(0, Vector3.zero);
                _lineRenderer.SetPosition(1, Vector3.zero);
            }
            
        }
        bool isHovered = RectTransformUtility.RectangleContainsScreenPoint(transform as RectTransform, Input.mousePosition, _canvas.worldCamera);

        if(isHovered)
        {
            _wireTask.CurrentHoveredWire = this;
        }
    }
    public void SetColor(Color color)
    {
        _image.color = color;
        _lineRenderer.startColor = color;
        _lineRenderer.endColor = color;
        customColor = color;
    }
    public void OnDrag(PointerEventData eventData)
    {

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(! isLeftWire) {return;}
        if(! isSuccess) {return;}
        _isDragStarted = true;
        _wireTask.CurrentDraggedWire = this;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if(_wireTask.CurrentHoveredWire != null)
        {
            if(_wireTask.CurrentHoveredWire.customColor == customColor && _wireTask.CurrentHoveredWire.isLeftWire)
            {
                isSuccess = true;
                _wireTask.CurrentHoveredWire.isSuccess = true;
            }
        }
        _isDragStarted = false;
        _wireTask.CurrentDraggedWire = null;
    }
}