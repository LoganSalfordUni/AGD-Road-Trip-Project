using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    public class TurnManager : MonoBehaviour
    {
        public static TurnManager instance;
        private void Awake()
        {
            instance = this;
        }

        public enum GameStates
        {
            playersTurn,//enemies move at 10% speed
            waitingForTurn,//enemies move at 100% speed
        }
        private GameStates _currentGameState;
        public GameStates currentGameState { get { return _currentGameState; } }

        private void Start()
        {
            //testing
            _currentGameState = GameStates.playersTurn;
            PlayerCombatController.instance.beginBattle() ;
        }

        public void EndTurn()
        {
            _currentGameState = GameStates.waitingForTurn;
        }
    }
}

