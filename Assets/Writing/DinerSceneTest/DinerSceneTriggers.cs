using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DinerSceneTriggers : MonoBehaviour
{
    //I'm not usre how many trigers this scene is going to require, so I figured I might as well pack them all into one script
    //This could be an event system methinks. 

    [SerializeField] GameObject[] disableThese;
    [SerializeField] GameObject[] enableThese;

    [SerializeField] TMP_Text uiText;
    [SerializeField] string textToDisplay;

    [SerializeField] bool canBeDoneMoreThanOnce = false;

    bool playerInside;

    enum TriggerCode
    {
        none,
        enterBathroom
    }
    [SerializeField] TriggerCode triggerCode = TriggerCode.none;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInside = true;
            uiText.gameObject.SetActive(true);
            uiText.text = textToDisplay;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInside = false;
            uiText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (!playerInside)
            return;


        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Note: This script is about to disable and enable a bunch of objects. If something appears or disappears that shouldnt, take a look at me");
            foreach (GameObject go in disableThese)
                go.SetActive(false);
            foreach (GameObject go in enableThese)
                go.SetActive(true);

            uiText.gameObject.SetActive(false);

            if (triggerCode == TriggerCode.enterBathroom)
            {

            }


            if (!canBeDoneMoreThanOnce)
                this.enabled = false;
        }
    }

    void SitPlayerDown()
    {
        //Sit player down. Disable the player body, enable a new fake body. Tho this can be done with the disable / enable thing
    }

    void EnterBathroom()
    {
        //change the scene
    }
}
