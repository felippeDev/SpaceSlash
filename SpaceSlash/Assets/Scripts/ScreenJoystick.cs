using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenJoystick : MonoBehaviour, IDragHandler
{
    public Vector3 InputDirection { get; set; }

    private void Start()
    {
        InputDirection = Vector3.zero;
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Debug.Log("OnDrag Direction: " + ped.position);

        InputDirection = ped.position;
    }
}
