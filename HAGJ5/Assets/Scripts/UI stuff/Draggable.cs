using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public delegate void DragEndedDelegate(Draggable draggableObj);
    public DragEndedDelegate dragEndedCallback;

    private bool isDragged = false;
    private Vector3 mouseDragStartPos;
    private Vector3 spriteDragstartPos;

    public string itemName;

    private void OnMouseDown()
    {
        isDragged = true;
        mouseDragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spriteDragstartPos = transform.position;
    }

    private void OnMouseDrag()
    {
        if (isDragged)
        {
            transform.position = (Camera.main.ScreenToWorldPoint(Input.mousePosition) + (spriteDragstartPos - mouseDragStartPos));
        }
    }

    private void OnMouseUp()
    {
        isDragged = false;
        dragEndedCallback(this);
    }
}
