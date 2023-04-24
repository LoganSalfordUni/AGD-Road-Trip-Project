using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressTracker : MonoBehaviour
{
    //make sure this is attatched to a game object w/ no other components
    //This script tracks the players progress between scenes. This way, the game can remember what choices you've made, and what conversations you've had
    public static ProgressTracker instance;
    public void Awake()
    {
        if (instance != null)
            return;

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private HashSet<string> _progressMarkers;
    [HideInInspector]
    public HashSet<string> ProgressMarkers
    {
        get
        {
            return _progressMarkers;
        }
    }

    private void Start()
    {
        _progressMarkers = new HashSet<string>();
    }
    public void AddProgressMarker(string add)//use this if you want to remember decisions the player has made 
    {
        _progressMarkers.Add(add.ToLower());
    }
}
