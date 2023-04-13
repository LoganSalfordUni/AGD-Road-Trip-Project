using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorAnimation : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag != "Player")
            return;
        Debug.Log("Close the doors");   
        anim.SetTrigger("CloseDoors");
    }
}
