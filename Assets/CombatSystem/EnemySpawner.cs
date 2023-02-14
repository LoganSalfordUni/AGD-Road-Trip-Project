using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefabTest;

        void SpawnEnemy()
        {
            float xPos = Random.Range(-21f, 21f);
            float zPos = -0.03f * (xPos * xPos) + 24;//to figure out what the graph should be. i used this website: https://www.desmos.com/calculator and plugged in numbers till it looked right and then tested them here, and plugged in numbers again

            Instantiate(enemyPrefabTest, new Vector3(xPos, 0f, zPos), Quaternion.identity);
        }

        private void Start()
        {
            SpawnEnemyTesting();
        }

        void SpawnEnemyTesting()
        {
            for (int i = -21; i <= 21; i += 2)
            {
                float xPos = i;
                float zPos = -0.03f * (xPos * xPos) + 24;

                Instantiate(enemyPrefabTest, new Vector3(xPos, 0f, zPos), Quaternion.identity);
            }

        }
    }
}

