using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public class LineReader : MonoBehaviour
    {
        public static LineReader instance;
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Debug.Log("Oops. I shouldn't exist");
        }

        private void Start()
        {
            _progressMarkers = new HashSet<string>();

            /*if (TESTLoadTextFileOnPlay)
                currentArgumentFile.LoadTextFile();
            if (TESTLoadTextFile2OnPlay)
                currentMainDialogueFile.LoadTextFile();*/

            //this is for TESTING only
            //ArgumentJumpToSection("start");
            //MainJumpToSection("start");
        }

        [SerializeField, Tooltip("The file for your parents argument.")]
        ParsedText currentArgumentFile;
        //[SerializeField, Tooltip("Load the text file on play. should be used for testing purposes only, not in the build version. will destroy edits made outside the text file")]
        //bool TESTLoadTextFileOnPlay;

        [SerializeField, Tooltip("This file contains any dialogue between yourself, and any interactions")]
        ParsedText currentMainDialogueFile;
        bool TESTLoadTextFile2OnPlay;

        int currentArgumentLineNumber;
        string[] argumentLines;

        //saved parts
        //the dialogue may be interupted and forced to skip to a new section briefly (say if the player does something that interupts. so we save where we're at, so we can return to it later
        Queue<int> savedArgueLineNumbers;
        Queue<string[]> savedArgueSections;


        int currentMainLineNumber;
        string[] mainLines;

        //Questions. (When you answer certain questions or perform certain actions. you may gain a progress marker.)
        [SerializeField] QuestionHandler questionHandler;
        private HashSet<string> _progressMarkers;
        [HideInInspector] public HashSet<string> ProgressMarkers
        {
            get
            {
                return _progressMarkers;
            }
        }
        public void AddProgressMarker(string add)//use this if you want to remember decisions the player has made 
        {
            _progressMarkers.Add(add.ToLower());
        }


        //im splitting the dialogue in two. One, the argument part. is for your parents conversation. it happens in their own time. its upto you if you pay attention or not
        //the second is the main part. This is dialogue the player clicks through. its mostly their own thoughts, but could also be any time other people speak to you

        //how to handle each line
        //dialogue lines should have a f, m (father, mother) on them to decide if the father says it, the mother says it or if it should be a generic text box. This will decide what text box the dialogue goes too
        //in the text printer. when a | is found. jump to the next line early
        public void ArgumentHandleNextLine()
        {
            currentArgumentLineNumber++;
            if (currentArgumentLineNumber >= argumentLines.Length)
            {
                Debug.Log("Reached the end of the section");
                return;
            }

            string currentLine = argumentLines[currentArgumentLineNumber].Trim();
            //Debug.Log(currentLine);

            if (currentLine.StartsWith('!'))
            {
                //its a command
                
                CommandManager.instance.HandleCommand(currentLine);
            }
            else
            {
                string[] parts = currentLine.Split(':');
                if (parts.Length == 0)
                {
                    Debug.Log("Error. A line of dialogue in the argument file, doesn't specify who speaks it");
                }
                else
                    TextPrinter.instance.DisplayArgumentText(parts[1], parts[0]);
            }

        }

        public void MainHandleNextLine()
        {
            currentMainLineNumber++;
            if (currentMainLineNumber >= mainLines.Length)
            {
                Debug.Log("Reached the end of the section");
                return;
            }

            string currentLine = mainLines[currentMainLineNumber].Trim();
            Debug.Log(currentLine);

            if (currentLine.StartsWith('!'))
            {
                //its a command
                CommandManager.instance.HandleCommand(currentLine);
            }
            else if (currentLine.StartsWith('?'))
            {
                //its a question.
                //note i  could do questions as a command, but i think dialogue options will occur frequently enough that they might as well have their own shortform symbol
                string[] stringChoices = (currentLine.Remove(0,1)).Split('/');//remove the first character because we dont want the ? mark being recorded. Then split by / marks to get the different options
                List<Choice> availibleChoices = new List<Choice>();
                foreach (string choice in stringChoices)
                {
                    Debug.Log(choice);
                    string[] choiceParts = choice.Split(':');
                    if (choiceParts.Length == 2)
                    {
                        //if there are 2 strings. The first should be the section name, the second should be the text to display to the screen
                        availibleChoices.Add(new Choice(choiceParts[0].Trim(), choiceParts[1].Trim()));
                    }
                    if (choiceParts.Length == 3)
                    {
                        //if there are 3 strings. The first should be the requirement for having this choice. then the second and third are as normal (section name, text description)
                        if (ProgressMarkers.Contains(choiceParts[0].Trim().ToLower()))
                        {
                            availibleChoices.Add(new Choice(choiceParts[1].Trim(), choiceParts[2].Trim()));
                        }
                    }


                    questionHandler.CreateDecision(availibleChoices.ToArray());
                }
            }
            else
            {
                //its a line of dialogue
                //I DONT want to show a name in the dialogue system. I like the idea of it being a bit ambigous whos speaking at any given point in time
                //string[] parts = currentLine.Split(':');

                TextPrinter.instance.DisplayMainText(currentLine);
            }
        }

        public void MainJumpToSection(string desiredSection)
        {
            if (currentMainDialogueFile == null)
                return;

            desiredSection = desiredSection.Trim().ToLower();
            if (!desiredSection.StartsWith('$'))
                desiredSection = "$" + desiredSection;

            foreach(ParsedText.section section in currentMainDialogueFile.sectionsRuntime)
            {
                if (section.sectionName == desiredSection)
                {
                    mainLines = section.sectionLines;
                    currentMainLineNumber = -1;
                    MainHandleNextLine();
                    return;
                }
            }
            Debug.Log("Section: '" + desiredSection + "' not found");
        }

        public void ArgumentJumpToSection(string desiredSection, bool saveCurrentPlace = false)
        {
            desiredSection = desiredSection.Trim().ToLower();
            if (!desiredSection.StartsWith('$'))
                desiredSection = "$" + desiredSection;

            if (saveCurrentPlace)
            {
                savedArgueLineNumbers.Enqueue(currentArgumentLineNumber);
                savedArgueSections.Enqueue(argumentLines);
            }

            foreach(ParsedText.section section in currentArgumentFile.sectionsRuntime)
            {
                if (section.sectionName == desiredSection)
                {
                    argumentLines = section.sectionLines;
                    currentArgumentLineNumber = -1;
                    ArgumentHandleNextLine();
                    break;
                }
            }
        }
        public void ArgumentReturnToLastSection()
        {
            argumentLines = savedArgueSections.Dequeue();
            currentArgumentLineNumber = savedArgueLineNumbers.Dequeue();
            ArgumentHandleNextLine();
        }

        public void LoadArgumentFile(ParsedText newText)
        {
            currentArgumentFile = newText;
        }
        public void LoadThoughtFile(ParsedText newText)
        {
            currentMainDialogueFile = newText;
        }
    }
}

///proposed changes:
///handle next auto line and handle next manual line
///in the text printer. once a manual line has completed printing. the player can click to go to the next line, but NOT before (last year people skipped through my dialogue because i was kind enough to let them click to skip the dialogue printing. NOT THIS TIME, i say!)

