using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DialogueSystem
{
    public class CommandManager : MonoBehaviour
    {
        public static CommandManager instance;
        private void Awake()
        {
            instance = this;
        }


        public void HandleCommand(string commandLine)
        {
            //get the command line. Split it into parts. Figure out what the command is, and then call a function to handle that command
            //Essentially this function sorts what command its been given, and then distributes the job of executing it elsewhere. 

            if (commandLine.StartsWith('!'))
                commandLine = commandLine.Remove(0,1);
            commandLine = commandLine.ToLower().Trim();
            //Debug.Log(commandLine);

            string[] lineParts = commandLine.Split('/');
            for (int i = 0; i < lineParts.Length; i++)
                lineParts[i] = lineParts[i].Trim();

            if (lineParts[0] == "nextstorybeat")
                nextStoryBeat();
            if (lineParts[0] == "gotoauto" || lineParts[0] == "autogoto")
                GoToSectionAutomatic(lineParts[1]);
            if (lineParts[0] == "goto" || lineParts[0] == "gotomanual" || lineParts[0] == "manualgoto")
                GoToSectionManual(lineParts[1]);

            if (lineParts[0] == "nextbathroomevent")
                BathroomEvent();

            if (lineParts[0] == "addprogress")
                AddProgress(lineParts[1]);

            if (lineParts[0] == "startcarone")
                StartCarOne();
            if (lineParts[0] == "startdiner")
                StartDiner();
            if (lineParts[0] == "startcartwo")
                StartCarTwo();

            if (lineParts[0] == "playradioone")
                PlaySound(0);

            if (lineParts[0] == "vanish")
                Vanish();
        }

        void AddProgress(string progressMarker)
        {
            LineReader.instance.ProgressMarkers.Add(progressMarker);
        }

        void nextStoryBeat()
        {
            //!NextStoryBeat
            //when this command is played. It (usually) means the current story beat is over, so the next one should start
            //Story beats can also be viewed as scenes (tho since unity already has a meaning for "scene" i felt "Beat" was a better term
            //Story beats are either a section of dialogue. or a new scene. Sometimes both!
            //To edit the order of story beats. Create a story manager in the scene and edit it
            StoryManager.instance.NextStoryBeat();
        }

        void GoToSectionAutomatic(string sectionName)
        {
            //gets the automatic dialogue system (used for the arguments) to skip to a specific place
            Debug.Log("sending the automatic dialogue system to: " + sectionName);
            LineReader.instance.ArgumentJumpToSection(sectionName);
        }

        void GoToSectionManual(string sectionName)
        {
            Debug.Log("sending the manual dialogue system to: " + sectionName);
            LineReader.instance.MainJumpToSection(sectionName);
        }

        void BathroomEvent()
        {
            BathroomDirector.instance.NextEvent();
        }

        void StartCarOne()
        {
            SceneManager.LoadScene("CarOne");
        }

        void StartDiner()
        {
            Debug.Log("starting the diner scene");
            SceneManager.LoadScene("MontyHallDiner");  
        }

        void StartCarTwo()
        {
            SceneManager.LoadScene("CarTwo");
        }

        void Ending()
        {
            SceneManager.LoadScene("Ending");
        }

        
        void PlaySound(int soundNumber) 
        { 

        }

        [SerializeField, Tooltip("If you use the command !vanish in this scene, this is the game object that'll disappear")] GameObject disappearGameObject;
        void Vanish()
        {
            disappearGameObject.SetActive(false);
        }
    }
}

