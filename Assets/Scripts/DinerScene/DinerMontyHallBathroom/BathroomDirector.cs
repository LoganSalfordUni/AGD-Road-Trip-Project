using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystem;

public class BathroomDirector : MonoBehaviour
{
    //This should be active in the bathroom scene. It'll handle playing the monty hall problem
    //I need to get the command manager to call functions here i think. Make a command called BathroomEvent that just skips to the next bathroom event, rather than making a bunch of voids 

    //section order (name the sections of the script for this scene these:)
    //$bathroomstart
    //$chosenfirstdoor
    //$revealedsecond
    //$finalrevealwin
    //$finalreveallose

    public static BathroomDirector instance;//gonna use a singleton to let the command manager call this. An event system might be better so the command manager cant ever attempt calling this but this is quicker + easier
    void Awake()
    {
        instance = this;
    }
            

    int selectedDoor;
    int revealedDoor;
    int goalDoor;//the door that you want to choose

    [SerializeField]
    GameObject[] doors;//for now im just deleting these doors. mby animate them later but theres no point in doing so whilst i dont have the art assets

    enum CurrentStoryState
    {
        startingDialogue,//happens at the start of the scene
        pickFirstDoor,//happens after the opening dialogue [no dialogue]
        chosenFirstDoor,//happens once you've chosen the first door 
        revealSecondDoor,//happens after the dialogue from the previous part
        waitingForSwap,//happens after the dialogue from reveal second door. Wait for the player to click an unrevealed door [no dialogue]
        finalReveal,//happens after the player chooses a door. Check if the door is the one you want or not 
        leave
    }
    CurrentStoryState currentStory;

    private void Start()
    {
        currentStory = CurrentStoryState.startingDialogue;
        LineReader.instance.MainJumpToSection("bathroomstart");
    }

    public void ClickDoor(int doorNumber)
    {
        if (currentStory == CurrentStoryState.pickFirstDoor)
        {
            selectedDoor = doorNumber;
            currentStory = CurrentStoryState.chosenFirstDoor;
            ChosenDoor();
        }
        else if (currentStory == CurrentStoryState.waitingForSwap)
        {
            selectedDoor = doorNumber;
            currentStory = CurrentStoryState.finalReveal;
            FinalReveal();
        }
    }

    public void NextEvent()
    {
        if (currentStory == CurrentStoryState.startingDialogue)
        {
            currentStory = CurrentStoryState.pickFirstDoor;//this allows the player to click the door
        }

        if (currentStory == CurrentStoryState.chosenFirstDoor)
        {
            currentStory = CurrentStoryState.revealSecondDoor;
            RevealSecondDoor();
        }

        if (currentStory == CurrentStoryState.revealSecondDoor)
        {
            currentStory = CurrentStoryState.waitingForSwap;
        }
    }

    void ChosenDoor()
    {
        //play dialogue related to when the player choses their first door

        LineReader.instance.MainJumpToSection("chosenfirstdoor");
    }

    void RevealSecondDoor()
    {
        //find a door that isnt the goal. And that hasnt been picked

        List<int> possibleDoors = new List<int> { 1, 2, 3 };//This is refering to doors as how they number themselves. When referring back to the list of doors, remember to lower the value by 1. (Door 1 is item 0 in the array of doors)

        possibleDoors.Remove(goalDoor);
        possibleDoors.Remove(selectedDoor);

        //revealedDoor = possibleDoors[0];//there are either 2 or 1 items left in this list. Either way, grabbing the earliest one is fine. The flaw with this is that, if a player knew it was coded this way, and the player chose door one, and door 3 gets revealed. then they know for certain that door 2 is correct. but that shouldnt really be a concern
        revealedDoor = Random.Range(0, possibleDoors.Count);

        Destroy(doors[revealedDoor]);

        LineReader.instance.MainJumpToSection("revealedsecond");

    }

    void FinalReveal()
    {
        //reveal selected door. Play dialogue

        if (selectedDoor == goalDoor)
        {
            LineReader.instance.MainJumpToSection("$finalrevealwin");
        }
        else
        {
            LineReader.instance.MainJumpToSection("$finalreveallose");
        }
    }
}
