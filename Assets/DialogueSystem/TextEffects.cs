using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Dialoguesystem
{
    public class TextEffects : MonoBehaviour
    {
        //i want to animate text on the screen, have it move around a bit. partially for "game feel" reasons, partially for conveying emotion
        //the plan is to attatch this script to the desired text fields. use a switch statement inside of an update loop to play different effects
        //ive not done this before so below i will write out some tutorials and resources:
        ///you need to use ForceMeshUpdate every frame you need to mesh to update (if im understanding things right)
        ///tutorial 1 https://youtu.be/ZHU3AcyDKik tutorial 2 https://youtu.be/FgWVW2PL1bQ tutorial 3 https://youtu.be/FXMqUdP3XcE (I couldnt find any non youtube tutorials for this sorta thing)
        ///Both tutorial 1 and 2 store a list of verts inside a vector 3 array
        ///each character is built from 4 vertices. This is well shown in tutorial 3. (start from the bottom left corner and go clockwise for the specific order)
        ///To end. Tutorial 2 and Tutorial 3 have different answers. One uses text.UpdateGeometry the other uses text.canvasrenderer.setmesh either way you feed the new mesh into it. I think UpdateGeometry is for doing it PER character because it asks for the index and thats the only difference between these two methods
        ///This is not easy to understand but heres the documentation:
        ///https://docs.unity3d.com/Packages/com.unity.textmeshpro@1.0/api/TMPro.TMP_Text.html
        public enum effectNames
        {
            jitter, 
            growth
        }
        public effectNames currentEffect;

        TMP_Text myTextField;
        Vector3[] vertices;

        private void Start()
        {
            myTextField = GetComponent<TMP_Text>();
        }


        void Update()
        {
            myTextField.ForceMeshUpdate();
            vertices = myTextField.mesh.vertices;

            switch (currentEffect)
            {
                case effectNames.jitter:
                    Jitter();
                    break;
                case effectNames.growth:
                    RandomGrowth();
                    break;
            }

            myTextField.mesh.vertices = vertices;
            myTextField.canvasRenderer.SetMesh(myTextField.mesh);
        }

        void Jitter(float jitterStrength = 1.5f)
        {
            Vector3 offsetAmount;
            for (int i = 0; i < vertices.Length; i+= 4)//we move 4 verts at a time because each character has 4 verts (This for loop counts characters)
            {
                offsetAmount = new Vector3(Random.Range(-1, 1f), Random.Range(-1, 1f), 0f);

                for (int x = 0; x < 4; x++)//we want to apply the same offset to each vertex in a character. again, theres 4 verts. so we apply it to all 4
                {
                    vertices[i + x] += offsetAmount * jitterStrength;
                }
            }
        }

        void RandomGrowth(float maxVariance = 3f)
        {
            float growth;

            for (int i = 0; i < vertices.Length; i += 4)
            {
                growth = Random.Range(0.5f, maxVariance);

                vertices[i] += new Vector3(-growth, -growth, 0f);
                vertices[i + 1] += new Vector3(-growth, growth, 0f);
                vertices[i + 2] += new Vector3(growth, growth, 0f);
                vertices[i + 3] += new Vector3(growth, -growth, 0f);
            }
        }

    }
}

