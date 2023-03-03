using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DialogueSystem
{
    public class TextPrinter : MonoBehaviour
    {
        public static TextPrinter instance;
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Debug.Log("This town aint big enough for the two of us (me and this other TextPrinter Singleton");

        }

        [Header("Text fields")]
        [SerializeField] GameObject fatherTextBox;
        [SerializeField] TMP_Text fatherTextField;
        float fatherTextBoxTimer;
        [Space(5)]
        [SerializeField] GameObject motherTextBox;
        [SerializeField] TMP_Text motherTextField;
        float motherTextBoxTimer;
        [Space(5)]
        [SerializeField] GameObject mainTextBox;
        [SerializeField] TMP_Text mainTextField;

        //I wanna add these in at some point, BUT I think I want them to work by having you click through the dialogue. in which case, it might be better to just. do a new dialogue system for them
        //to save work. maybe when this is done printing. add in a bool that waits for the player to click. and then goes to the next dialogue bit (this may run into issues with a pause menu or something too)
        //GameObject neutralTextBox;
        //TMP_Asset neutralTextField;

        bool callNextLineWhenTimerReachesZero;//if this is true. call the next line once the timer reaches zero
        private float timeTillNextLine;//when this reaches 0. Call the line reader to go to the next line

        public void DisplayArgumentText(string textToDisplay, string whichTextBox)
        {
            //about the timer below. Im testing different times. 3.2 is a good fast speed but can be difficult to keep up with 
            float newTimer = (textToDisplay.Split(' ').Length * 0.4f) + (textToDisplay.Split(',').Length * 0.1f) + 0.2f;//figure out how long the timers should last for. you can read about 3.5 words per second. adding in 0.5 seconds for good measure. and a lil extra time for common punctuation marks (. and ,). NOTE: removed the extra time from . becus what if we inconsistently add fullstops to the ends of sentences. the time will be weird
            callNextLineWhenTimerReachesZero = true;
            timeTillNextLine = newTimer;

            if (whichTextBox.Trim().ToLower() == "m")
            {
                motherTextBoxTimer = newTimer;
                StartCoroutine(PrintToMotherTextField(textToDisplay));
            }
            if (whichTextBox.Trim().ToLower() == "f")
            {
                fatherTextBoxTimer = newTimer;
                StartCoroutine(PrintToFatherTextField(textToDisplay));
            }
        }

        bool canClickToNextLine;

        public void DisplayMainText(string dialogue)
        {
            StartCoroutine(PrintToMainTextField(dialogue));
        }

        private void Update()
        {
            if (canClickToNextLine && Input.GetMouseButtonDown(0))
            {
                canClickToNextLine = false;
                LineReader.instance.MainHandleNextLine();
            }
        }

        private void LateUpdate()
        {
            //i want lines to progress automatically. 
            timeTillNextLine -= Time.deltaTime;
            if (callNextLineWhenTimerReachesZero && timeTillNextLine <= 0f)
            {
                Debug.Log("going to next line");
                callNextLineWhenTimerReachesZero = false;
                LineReader.instance.ArgumentHandleNextLine();
            }

            if (motherTextBoxTimer <= 0f)
            {
                motherTextBox.SetActive(false);
            }
            else
                motherTextBoxTimer -= Time.deltaTime;

            if (fatherTextBoxTimer <= 0f)
            {
                fatherTextBox.SetActive(false);
            }
            else
                fatherTextBoxTimer -= Time.deltaTime;
        }


        //OAEF add in, that if a part contains "|" then it should be ignored and instead, the line reader should jump to the next line
        IEnumerator PrintToMotherTextField(string textToDisplay)
        {
            motherTextField.text = "";
            motherTextBox.SetActive(true);

            string[] parts = textToDisplay.Split(' ');

            foreach (string part in parts)
            {
                if (part.Contains('|'))
                {
                    LineReader.instance.ArgumentHandleNextLine();
                    continue;
                }

                motherTextField.text += part + " ";
                yield return new WaitForSeconds(0.25f);
            }
        }
        IEnumerator PrintToFatherTextField(string textToDisplay)
        {
            fatherTextField.text = "";
            fatherTextBox.SetActive(true);

            string[] parts = textToDisplay.Split(' ');

            foreach (string part in parts)
            {
                if (part.Contains('|'))
                {
                    LineReader.instance.ArgumentHandleNextLine();
                }

                fatherTextField.text += part + " ";
                yield return new WaitForSeconds(0.15f);//making the father speak a bit faster
            }
        }

        IEnumerator PrintToMainTextField(string textToDisplay)
        {
            mainTextBox.SetActive(true);
            mainTextField.text = "";

            foreach (char c in textToDisplay)
            {
                if (c == '#')
                {
                    yield return new WaitForSeconds(0.1f);
                    continue;
                }

                mainTextField.text += c;
                yield return new WaitForSeconds(0.03f);
            }

            //once the dialogue is printed. the player can click to skip to the next line
            canClickToNextLine = true;
        }
    }
}
////
///okay so plan. the parents text boxes should disappear after they've finished speaking. they also maybe shouldn't have much opaques too them cus theyll constantly disapear reappear 
///dialogue needs to play itself. only the most recent dialogue displayed should count for the timer (though each text box should have its own timer for when it should hide itself.)
///for each timer. calculate it by doing the number of words / 3. then add 0.5 seconds. (people read about 3.5 words per second. so lower that to 3 for slower readers. and 0.5 seconds extra for comfort.)