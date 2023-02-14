using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    public class Enemy : MonoBehaviour
    {
        float speed = 3f;

        private void Update()
        {
            float localTimeScale = TurnManager.instance.currentGameState == TurnManager.GameStates.playersTurn ? 0.1f : 1f;
            
            transform.LookAt(new Vector3(0, 0, 0));
            transform.position += transform.forward * speed * Time.deltaTime * localTimeScale;

        }


        public void AttackMe()
        {
            Destroy(this.gameObject);
        }
    }
}

