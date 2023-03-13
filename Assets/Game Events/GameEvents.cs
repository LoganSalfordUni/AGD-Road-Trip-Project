using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvents : ScriptableObject
{
    //when this event is called, it will signal each of its listeners

    HashSet<GameEventListener> myListeners = new HashSet<GameEventListener>();//A hashset is a list but restricted to one of each kind

}
