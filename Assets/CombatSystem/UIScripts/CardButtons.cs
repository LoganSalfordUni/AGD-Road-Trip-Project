using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CombatSystem
{
    [RequireComponent(typeof(Image))]
    public class CardButtons : MonoBehaviour
    {
        private Card card;
        public Card myCard { get { return card; } }

        Image myImage;
        [SerializeField] TMP_Text myText;

        private void Start()
        {
            myImage = this.gameObject.GetComponent<Image>();
        }

        public void SetMyCard(Card newCard)
        {
            card = newCard;

            myText.text = card.abilityName;
        }

        /*private void OnMouseUp()
        {
            HighlightMe();
            PlayerCombatController.instance.SelectCard( this);
        }*/

        public void OnClick()
        {
            HighlightMe();
            PlayerCombatController.instance.SelectCard(this);
        }

        void HighlightMe()
        {
            myImage.color = Color.gray;
        }

        public void UnhighlightMe()
        {
            myImage.color = Color.white;
        }

    }
}

