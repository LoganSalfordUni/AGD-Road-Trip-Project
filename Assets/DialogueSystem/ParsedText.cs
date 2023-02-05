using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem 
{
    [CreateAssetMenu]
    public class ParsedText : ScriptableObject
    {
        [SerializeField, Tooltip("to load this. click on the three dots in the top right corner")] private TextAsset textFileOrigin;

        [System.Serializable]
        public struct section
        {
            public string sectionName;
            public string[] sectionLines;
        }
        [SerializeField]
        private section[] sections;
        
        [HideInInspector]
        public section[] sectionsRuntime
        {
            get
            {
                return sections;
            }
        }

        [ContextMenu("LoadTextFile")]//to use this. click on the three dots in the top right corner
        public void LoadTextFile()
        {
            if (textFileOrigin == null)
            {
                Debug.Log("ARGH. WHAT KIND OF SHENNANIGAN IS THIS?! YOU DIDN'T INPUT A TEXT FILE TO SCAN. Add a text asset to the text file origin");
                return;
            }

            string[] fileLines = textFileOrigin.ToString().Split('\n');
            
            //figure out how many sections are in the text file
            int numberOfSections = 0;
            foreach (string line in fileLines)
            {
                if (line.Trim().StartsWith('$'))
                {
                    numberOfSections++;
                }
            }

            sections = new section[numberOfSections];//now we can edit sections directly. I think I could have used a list and then converted it but creating a new entry in a struct was giving me errors

            List<string> newSectionLines = new List<string>();
            int currentSectionNumber = -1;
            for (int i = 0; i < fileLines.Length; i++)
            {
                Debug.Log(fileLines[i]);
                //the process here is to save each line (that isnt a comment or section) to a string list. then when we get to a new section, we save that list inside of the sections array

                if (fileLines[i].Trim().StartsWith('$'))//trim all the time. this happens outside of runtime so its not rly costing anything. sometimes theres a random character at the start not perceptable to the human eyeball and you end up confused
                {
                    //new section
                    if (currentSectionNumber > -1)//we start at -1 because when we find the first section, we havn't saved any lines inside current section lines yet. 
                    {
                        //save all currently stored lines inside of newSectionLines to the previous section, before we start counting for the new one
                        sections[currentSectionNumber].sectionLines = newSectionLines.ToArray();
                    }

                    currentSectionNumber++;
                    sections[currentSectionNumber].sectionName = fileLines[i].Trim().ToLower();//all sections should be in lower case to prevent capitalisation errors. however, in the text document. it acn be nicer for them to be capitalised for readability. so we change that here 
                    newSectionLines.Clear();//reset the newSectionLines because we're recording a new section now

                    continue;
                }
                if (fileLines[i].Trim().StartsWith('/'))
                {
                    //skip this line. im pretty sure thats what break does. remember to test this. (NOTE: DONT USE BREAK, THAT BREAKS THE ENTIRE LOOP)
                    continue;
                }
                else
                {
                    newSectionLines.Add(fileLines[i]);
                }
            }
            sections[currentSectionNumber].sectionLines = newSectionLines.ToArray();//save the final section
        }
    }
}

