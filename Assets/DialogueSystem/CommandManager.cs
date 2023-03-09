using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public class CommandManager : MonoBehaviour
    {
        public void HandleCommand(string commandLine)
        {
            //get the command line. Split it into parts. Figure out what the command is, and then call a function to handle that command
            //Essentially this function sorts what command its been given, and then distributes the job of executing it elsewhere. 

            if (commandLine.StartsWith('!'))
                commandLine.Remove(0);
            commandLine = commandLine.ToLower().Trim();

            if (commandLine == "nextstorybeat")
                nextStoryBeat();
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
    }
}

