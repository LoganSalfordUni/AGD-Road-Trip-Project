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
            if (TESTLoadTextFileOnPlay)
                currentFile.LoadTextFile();

            JumpToSection("start");
        }

        [SerializeField]
        ParsedText currentFile;
        [SerializeField, Tooltip("Load the text file on play. should be used for testing purposes only, not in the build version. will destroy edits made outside the text file")]
        bool TESTLoadTextFileOnPlay;


        int currentLineNumber;
        string[] lines;

        //saved parts
        //the dialogue may be interupted and forced to skip to a new section briefly (say if the player does something that interupts. so we save where we're at, so we can return to it later
        Queue<int> savedLineNumbers;
        Queue<string[]> savedSections;
        
        //how to handle each line
        //dialogue lines should have a f, m or n (father, mother, neutral) on them to decide if the father says it, the mother says it or if it should be a generic text box. This will decide what text box the dialogue goes too
        //in the text printer. when a | is found. jump to the next line early
        public void HandleNextLine()
        {
            currentLineNumber++;
            if (currentLineNumber >= lines.Length)
            {
                Debug.Log("Reached the end of the section");
                return;
            }

            string currentLine = lines[currentLineNumber].Trim();
            Debug.Log(currentLine);

            if (currentLine.StartsWith('!'))
            {
                //its a command

                HandleNextLine();//for now just skip to the next line. DELETE THIS LATER
            }
            else
            {
                string[] parts = currentLine.Split(':');
                if (parts.Length == 0)
                    return;//go to the next printer
                else
                    TextPrinter.instance.DisplayText(parts[1], parts[0]);
            }

        }

        public void JumpToSection(string desiredSection, bool saveCurrentPlace = false)
        {
            desiredSection = desiredSection.Trim().ToLower();
            if (!desiredSection.StartsWith('$'))
                desiredSection = "$" + desiredSection;

            if (saveCurrentPlace)
            {
                savedLineNumbers.Enqueue(currentLineNumber);
                savedSections.Enqueue(lines);
            }

            foreach(ParsedText.section section in currentFile.sectionsRuntime)
            {
                if (section.sectionName == desiredSection)
                {
                    lines = section.sectionLines;
                    currentLineNumber = -1;
                    HandleNextLine();
                    break;
                }
            }
        }
        public void ReturnToLastSection()
        {
            lines = savedSections.Dequeue();
            currentLineNumber = savedLineNumbers.Dequeue();
            HandleNextLine();
        }
    }
}

