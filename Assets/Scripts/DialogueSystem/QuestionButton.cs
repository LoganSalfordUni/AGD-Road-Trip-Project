using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using DialogueSystem;

public class QuestionButton : MonoBehaviour, IPointerDownHandler
{
    //idk how to edit a unity button through script, and researching it got kinda complicated. so instead of breaking my flow im doing it in a messier way

    public string mySectionName;//when this object is clicked. this is what section it goes too. Set by the question handler
    public QuestionHandler questionHandler;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("click");
        questionHandler.MakeChoice(mySectionName);
    }
}
