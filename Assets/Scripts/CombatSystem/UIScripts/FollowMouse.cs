using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowMouse : MonoBehaviour
{
    private void Update()
    {
        Vector3 mousePos = Camera.main.WorldToScreenPoint(Input.mousePosition);
        this.transform.LookAt(mousePos);
    }
}
