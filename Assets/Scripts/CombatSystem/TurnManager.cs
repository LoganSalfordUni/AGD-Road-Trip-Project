using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            playerTurnUI.SetActive(true);
            notPlayerTurnUI.SetActive(false);

            _currentGameState = GameStates.playersTurn;
            PlayerCombatController.instance.beginBattle() ;
        }

        [SerializeField, Tooltip("What is ONLY visible during the players turn")] GameObject playerTurnUI;
        [SerializeField, Tooltip("What is ONLY visible whilst its NOT the players turn")] GameObject notPlayerTurnUI;

        float turnTimerGoal;
        float turnTimer;
        [SerializeField] Image timerImage;
        public void EndTurn()
        {
            playerTurnUI.SetActive(false);
            notPlayerTurnUI.SetActive(true);

            _currentGameState = GameStates.waitingForTurn;
            turnTimer = 0f;
            turnTimerGoal = 3f - (0.1f * Mathf.Pow(PlayerCombatController.instance.playerStats.speed, 0.7f));//you'd need a speed of over 100 (128.9) to get near instant turns. using this: https://www.desmos.com/calculator to figure out the curve
        }

        public void StartTurn()
        {
            playerTurnUI.SetActive(true);
            notPlayerTurnUI.SetActive(false);

            _currentGameState = GameStates.playersTurn;
            PlayerCombatController.instance.startPlayerTurn();
        }



        private void Update()
        {
            if (_currentGameState == GameStates.waitingForTurn)
            {
                turnTimer += Time.deltaTime;
                timerImage.fillAmount = (turnTimer / turnTimerGoal);
                if (turnTimer >= turnTimerGoal)
                {
                    StartTurn();
                }
            }
        }
    }
}

