using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DialogueSystem
{
    public class StoryManager : MonoBehaviour
    {
        //This is responsible for handing the different beats of the story. 
        //Plan: Make a list of "Stuff that happens" and order it. 
        //Have a function that goes to the next "stuff" once the previous one is done. 
        //Allow multiple stuff to be started at once. cus we might want multiple things to start at once 

        public static StoryManager instance;
        private void Awake()
        {
            if (instance != null)
            {
                Debug.Log("Too many story managers exist");
                Destroy(this);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
        }


        [System.Serializable]
        struct StoryBeat
        {
            public enum argumentOrThought {thought, argument}
            [Tooltip("This needs to be filled in for the textObject and sectionName to work")] public argumentOrThought whichDialogueSystem;
            [Tooltip("Leave blank if you dont need to load a new parsed text file")] public ParsedText textObject;
            [Tooltip("Which section should we go to in the current (or new) ParsedText file")] public string sectionName;
            [Tooltip("Leave blank if theres no scene to switch too")] public string newSceneName;
        }

        [SerializeField, Tooltip("Make sure these are in order")] List<StoryBeat> storyMoments;
        int currentMoment;

        private void Start()
        {
            currentMoment = 0;
            LoadMoment();
        }

        void LoadMoment()
        {
            StoryBeat thisBeat = storyMoments[currentMoment];
            if (thisBeat.newSceneName != null)
            {
                //add in some checks to make sure the scene exists and it isnt the currently loaded scene
                SceneManager.LoadScene(thisBeat.newSceneName, LoadSceneMode.Single);
            }

            if (thisBeat.textObject != null)
            {
                if (thisBeat.whichDialogueSystem == StoryBeat.argumentOrThought.thought)
                    LineReader.instance.LoadThoughtFile(thisBeat.textObject);
                else if (thisBeat.whichDialogueSystem == StoryBeat.argumentOrThought.argument)
                    LineReader.instance.LoadArgumentFile(thisBeat.textObject);
            }

            if (thisBeat.sectionName != null)
            {
                if (thisBeat.whichDialogueSystem == StoryBeat.argumentOrThought.thought)
                {
                    LineReader.instance.MainJumpToSection(thisBeat.sectionName);
                }
                else if (thisBeat.whichDialogueSystem == StoryBeat.argumentOrThought.argument)
                {
                    LineReader.instance.ArgumentJumpToSection(thisBeat.sectionName);
                }
            }
        }

        public void NextStoryBeat()
        {
            currentMoment += 1;
            LoadMoment();
        }
    }
}

