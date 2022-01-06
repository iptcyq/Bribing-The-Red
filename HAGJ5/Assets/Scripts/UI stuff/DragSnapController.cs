using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragSnapController : MonoBehaviour
{
    public List<Transform> snapPoints;
    public List<Draggable> draggableObj;
    public float snapRange = 0.5f;

    public CollectedItems ci;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Draggable draggable in draggableObj)
        {
            draggable.dragEndedCallback = OnDragEnded;
        }
    }

    private void OnDragEnded(Draggable draggable)
    {
        float closestDistance = -1;
        Transform closestSnapPoint = null;
        
        foreach(Transform snapPoint in snapPoints)
        {
            float currentDistance = Vector2.Distance(draggable.transform.position, snapPoint.position);
            if (closestSnapPoint == null || currentDistance < closestDistance)
            {
                closestSnapPoint = snapPoint;
                closestDistance = currentDistance;
            }
        }

        if (closestSnapPoint != null && closestDistance <= snapRange)
        {
            draggable.transform.position = closestSnapPoint.position;

            draggable.gameObject.SetActive(false);

            for (int i =0; i<= ci.itemNames.Length-1; i++)
            {
                if (ci.itemNames[i] == draggable.itemName)
                {
                    ci.itemObtained[i] = true;
                    break;
                }
                if (i == ci.itemNames.Length - 1)
                {
                    ci.noOfUselessStuff++;
                }
            }
        }
    }
}
