using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BathroomStallDoor : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] int myDoorNumber;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        BathroomDirector.instance.ClickDoor(myDoorNumber);
    }
}
