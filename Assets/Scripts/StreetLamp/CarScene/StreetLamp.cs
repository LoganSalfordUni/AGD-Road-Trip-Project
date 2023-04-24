using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StreetLamp : MonoBehaviour
{
    public Transform MainCamera { get; private set; }
    //public Transform Lamp { get; private set; }
    [SerializeField]public float speed;
    [SerializeField]public int minDistance;
    [SerializeField]public int maxDistance;
    
    // Start is called before the first frame update
    private void Start()
    {
        if (Camera.main != null) MainCamera = Camera.main.transform;
        //Lamp = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 newPos = Lamp.get - speed;
        //Lamp.SetPositionAndRotation(newPos);
        var transformPosition = gameObject.transform.position;
        transformPosition.z = transformPosition.z -speed;
        gameObject.transform.position = transformPosition;
        if (gameObject.transform.position.z<-6)
        {
            float distanceFromCamera = Random.Range(minDistance, maxDistance);
            transformPosition.z = MainCamera.position.z + distanceFromCamera;
            gameObject.transform.position = transformPosition;
        }
    }
}
