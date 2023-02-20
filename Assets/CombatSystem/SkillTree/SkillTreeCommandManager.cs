using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//based on this tutorial: https://learn.unity.com/tutorial/command-pattern
public class SkillTreeCommandManager : MonoBehaviour
{
    //scrap this code. Just realised. I dont want the player to be able to undo their decisions on the skill tree. that defeats the entire metaphor 

    public static SkillTreeCommandManager instance;
    private void Awake()
    {
        instance = this;
    }

    Stack<ICommand> commandStack;

    public void AddCommand(ICommand command)
    {
        command.Execute();//execute the command
        commandStack.Push(command);//add it to a stack so it can be undone later
    }

    public void UndoLastCommand()
    {
        if (commandStack.Count == 0)
            return;

        var cmd = commandStack.Pop();
        cmd.Undo();
    }
}
