using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class Choice
    {
        public string goToSection;
        public string choiceText;

        public Choice(string sectionName, string text)
        {
            this.goToSection = sectionName;
            this.choiceText = text;
        }
    }

    public class QuestionHandler : MonoBehaviour
    {



        [SerializeField] Transform choiceLayoutGroup;
        [SerializeField] GameObject choiceObjectPrefab;
 

        public void CreateDecision(Choice[] choices)
        {
            if (choiceLayoutGroup.childCount > 0)
            {
                foreach (Transform child in choiceLayoutGroup)
                {
                    Destroy(child.gameObject);
                }
            }

            choiceLayoutGroup.gameObject.SetActive(true);

            foreach (Choice option in choices)
            {
                GameObject newChoice = Instantiate(choiceObjectPrefab, choiceLayoutGroup);
                newChoice.GetComponentInChildren<TMP_Text>().text = option.choiceText;
                newChoice.GetComponent<QuestionButton>().mySectionName = option.goToSection;
                newChoice.GetComponent<QuestionButton>().questionHandler = this;
            }
        }

        public void MakeChoice(string choiceSection)
        {
            //called by the QuestionButtons 
            Debug.Log("Choice has been made, and the choices name is: " + choiceSection);

            choiceLayoutGroup.gameObject.SetActive(false);
            LineReader.instance.MainJumpToSection(choiceSection);
        }
    }
}

