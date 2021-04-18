using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedTouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector]
    public Vector2 TouchDir;
    [HideInInspector]
    public float TouchDist;
    [HideInInspector]
    public Vector2 PointerDown;
    [HideInInspector]
    public Vector2 PointerOld;
    [HideInInspector]
    protected int PointerId;
    [HideInInspector]
    public bool Pressed;

    // Update is called once per frame
    void Update()
    {
        if (Pressed)
        {
            if (PointerId >= 0 && PointerId < Input.touches.Length)
            {
                TouchDir = Input.touches[PointerId].position - PointerOld;
               // TouchDist = Vector2.Distance(Input.touches[PointerId].position, PointerOld);
                PointerOld = Input.touches[PointerId].position;
            }
            else
            {
                TouchDir = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - PointerOld;
              //  TouchDist = Vector2.Distance(new Vector2(Input.mousePosition.x, Input.mousePosition.y), PointerOld);
                PointerOld = Input.mousePosition;
            }
            if(Vector2.Distance(PointerOld, PointerDown) > 0)
            TouchDist = Vector2.Distance(PointerOld, PointerDown);
        }
        else
        {
            TouchDir = new Vector2();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        PointerId = eventData.pointerId;
        PointerOld = eventData.position;
        PointerDown = eventData.position;
    }

    
    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }
}
