using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace CombatSystem
{
    public class PlayerCombatController : MonoBehaviour
    {
        public static PlayerCombatController instance;
        private void Awake()
        {
            instance = this;
        }

        [SerializeField] GameObject cardPrefabBase;
        [SerializeField] Transform cardHolder;

        [SerializeField] TMP_Text manaField;

        [SerializeField] private PlayerStats playerStats;

        private List<Card> deck;
        private List<Card> discardPile;
        private float health;
        public float Health { get { return health; } }

        private int _currentMana;
        int currentMana
        {
            get
            {
                return _currentMana;
            }
            set
            {

                _currentMana = value;
                manaField.text = _currentMana.ToString();
            }
        }

        int handLimit = 5;

        public void beginBattle()
        {
            deck = new List<Card>();
            discardPile = new List<Card>();
            deck.AddRange(playerStats.deck);
            health = playerStats.maxHealth;
            cardsInHand = 0;

            while (cardsInHand < handLimit)
                DrawCard();


            //testing
            startPlayerTurn();
        }

        int cardsInHand;
        void DrawCard()
        {
            if (handLimit >= cardsInHand)
            {
                cardsInHand++;
                if (deck.Count == 0)
                {
                    ShuffleDiscardPileIntoDeck();
                }

                int pullFromDeckPosition = Random.Range(0, deck.Count);


                GameObject drawnCard = Instantiate(cardPrefabBase, cardHolder);
                drawnCard.GetComponent<CardButtons>().SetMyCard(deck[pullFromDeckPosition]);

                deck.RemoveAt(pullFromDeckPosition);


            }
        }

        void ShuffleDiscardPileIntoDeck()
        {
            Debug.Log("Need to make this bit lol");
        }


        bool isMyTurn;
        public void startPlayerTurn()
        {
            currentMana = Random.Range(1, 5) + Random.Range(1, 5);

            isMyTurn = true;
        }

        private CardButtons currentlySelectedCard;
        public void SelectCard(CardButtons card)
        {
            if (card.myCard.manaCost > currentMana)
            {
                card.UnhighlightMe();
                Debug.Log("Not enough mana. Display a warning msg to the player");
                return;
            }
            currentlySelectedCard = card;
        }
        
        void ActivateCard(Enemy target = null)
        {
            Debug.Log(currentlySelectedCard.myCard.abilityName);

            currentMana -= currentlySelectedCard.myCard.manaCost;

            if (target != null)
            {
                target.AttackMe();
            }

            discardPile.Add(currentlySelectedCard.myCard);
            Destroy(currentlySelectedCard.gameObject);
            currentlySelectedCard = null;

            cardsInHand--;
        }

        void RedrawCard()
        {
            
        }

        void RemoveCurrentlySelectedCardFromHand()
        {

        }

        //[SerializeField] GameObject targettingReticle;
        private void Update()
        {
            if (!isMyTurn)
                return;

            /*if (currentlySelectedCard != null)
            {
                targettingReticle.transform.localScale = new Vector3(1f, currentlySelectedCard.myCard.abilityDistance, 1f);

                //check the mouse is in bounds

                //mouse pos
                Vector3 targetPos = new Vector3(0f, 0f, 0f);
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    targetPos = hit.point;
                }
                Debug.Log(targetPos);
                targetPos.y = targettingReticle.transform.position.y;

                targettingReticle.transform.LookAt(targetPos, Vector3.forward);
            }*/

            if (currentlySelectedCard != null)
            {
                //if the player has selected a card, they can start targetting enemies
                Vector3 mousePosition = new Vector3(0f, 0f, 0f);
                Enemy targettedEnemy = null;
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    mousePosition = hit.point;
                    if (hit.collider.GetComponent<Enemy>())
                    {
                        targettedEnemy = hit.collider.GetComponent<Enemy>();
                    }
                }

                switch (currentlySelectedCard.myCard.targettingStyle)
                {
                    case Card.TargettingStyle.singleTarget:
                        if (Input.GetMouseButtonDown(0) && targettedEnemy != null)
                        {
                            ActivateCard(targettedEnemy);
                        }
                        break;
                }
            }

            /*if (Input.GetMouseButtonDown(0) && currentlySelectedCard != null)
            {
                //oaef check if the player is targetting a valid position first
                ActivateCard();
            }*/

            if (Input.GetMouseButtonDown(1))
            {
                currentlySelectedCard.UnhighlightMe();
                currentlySelectedCard = null;
            }
        }
    }
}

