using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace CombatSystem
{
    public class SkillTreeManager : MonoBehaviour
    {
        public static SkillTreeManager instance;
        private void Awake()
        {
            instance = this;
        }


        [SerializeField] GameObject textFieldPanel;
        [SerializeField] TMP_Text textField;

        public void ShowChoiceDescription(string newText)
        {
            textFieldPanel.SetActive(true);
            textField.text = newText;
        }
        public void HideDescription()
        {
            textFieldPanel.SetActive(false);
        }

        [SerializeField] SkillTreeButton[] startingButtons;
        [SerializeField] PlayerStats playerStats;

        [HideInInspector] public List<Card> deck;
        [HideInInspector] private int health;
        [HideInInspector] private float speed;
        [HideInInspector] private float attackPower;
        [HideInInspector] private float knockbackPower;
        public void beginSkillTree()
        {
            health = 30;
            speed = 0f;
            attackPower = 5f;
            knockbackPower = 3f;

            foreach (SkillTreeButton treeNode in startingButtons)
            {
                treeNode.EnableMe();
            }
        }

        public void CompleteSkillTree()
        {
            playerStats.SkillTreeSetStats(health, speed, attackPower, knockbackPower);
            playerStats.SkillTreeSetDeck(deck);
        }
    }

}
