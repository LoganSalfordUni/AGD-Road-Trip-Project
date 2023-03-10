using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DialogueSystem;
using TMPro;

public class DinerSitDownScript : MonoBehaviour
{
    //Run this code by enabling the object its attatched too 
    //Inside the diner scene, when the player sits down. I need to disable the player from moving, and then play a little bit of dialogue before letting the player stand up to go to the bathroom

    //Note to self: I think itd be better to handle all these "enable these" "disable these" things through one manager script. and have this script call that one. that way, in the inspector. everything is in one place. make this change if/when ive got time
    //^ diner scene triggers will also need changing 

    bool canPressPToStand;

    [SerializeField] GameObject pressPToStandUpText;

    [SerializeField] GameObject failedStandUpText;//make this say like "e-excuse me?"
    [SerializeField] GameObject succesfulStandUpText;//this should say something like"I need to use the bathroom"


    [SerializeField, Tooltip("When these text boxes are inactive. The player has an easier time standing up")] GameObject[] adultConversationTextBoxes;
    TMP_Text[] conversationTextFields;

    [Header("After standing up")]
    [SerializeField] GameObject[] disableThese;
    [SerializeField] GameObject[] enableThese;

    private void Start()
    {
        //Load some dialogue, then the coroutine. 
        canPressPToStand = false;
        LineReader.instance.ArgumentJumpToSection("start");



        conversationTextFields[0] = adultConversationTextBoxes[0].GetComponent<TMP_Text>();
        conversationTextFields[1] = adultConversationTextBoxes[1].GetComponent<TMP_Text>();
    }




    float pTimer;
    IEnumerator WaitTillYouCanGoToTheBathroom()
    {
        yield return new WaitForSeconds(2f);//after testing, move this time up

        canPressPToStand = true;
        pTimer = 0f;
        pressPToStandUpText.SetActive(true);
        //Load some more dialogue that has occasional breaks in it and allow the player to press a button to stand up
    }


    void Update()
    {
        if (!canPressPToStand)
            return;

        if (pTimer > 0f)
        {
            pTimer -= Time.deltaTime;
            return;
        }
        failedStandUpText.SetActive(false);

        if (Input.GetKeyDown(KeyCode.P))
        {
            //check if the player can stand up based on if the conversation with the adults is at a standstill or not
            pTimer = 0.2f;
            float chanceOfStandUp = 0.5f;
            if (adultConversationTextBoxes[0].activeInHierarchy)
            {
                chanceOfStandUp -= 0.3f;
                if (conversationTextFields[0].text == "...")
                {
                    chanceOfStandUp += 0.3f;
                }
            }
            if (adultConversationTextBoxes[1].activeInHierarchy)
            {
                chanceOfStandUp -= 0.3f;
                if (conversationTextFields[1].text == "...")
                {
                    chanceOfStandUp += 0.3f;
                }
            }


            if (Random.Range(0f, 1f) <= chanceOfStandUp)
            {
                canPressPToStand = false;
                StartCoroutine(SuccesfullyAskToStand());
            }
            else
                failedStandUpText.SetActive(true);
        }
    }

    void StandUp()
    {
        foreach (GameObject go in disableThese)
        {
            Debug.Log("Btw im de-activating and activating a bunch of game objects all at once. Might want to check my variables to make sure theres nothing there that shouldnt be");
            go.SetActive(false);
        }
        foreach (GameObject go in enableThese)
        {
            go.SetActive(true);
        }
    }

    IEnumerator SuccesfullyAskToStand()
    {
        succesfulStandUpText.SetActive(true);
        pressPToStandUpText.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        StandUp();
        succesfulStandUpText.SetActive(false);
    }
}
