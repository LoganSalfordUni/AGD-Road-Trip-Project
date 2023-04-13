using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    [CreateAssetMenu]
    public class PlayerStats : ScriptableObject
    {
        //only one of these needs to exist

        [SerializeField] private List<Card> _deck;
        public List<Card> deck { get { return _deck; } }

        [SerializeField] private int _maxHealth;
        public int maxHealth { get { return _maxHealth; } }

        [SerializeField] private float _speed;
        public float tempSpeed;
        public float speed { get { return _speed + tempSpeed; } }

        [SerializeField] private float _attackPower;
        public float tempAttackPower;
        public float attackPower { get { return _attackPower + tempAttackPower; } }

        //knockback distance is calculated by 3 + 1/10th of your knockback power. This can be found in player combat controller inside the activate card function
        [SerializeField] private float _knockbackPower;
        public float tempKnockbackPower;
        public float knockbackPower { get { return _knockbackPower + tempKnockbackPower; } }

        void StartCombat()
        {
            //reset all temporary stats

            tempSpeed = 0f;
            tempAttackPower = 0f;
            tempKnockbackPower = 0f;
        }

        /*void ResetStats()
        {
            //set stats to their starting value. Do this when the game begins and the player is choosing their stats in the skill tree

            _maxHealth = 30;
            _speed = 0f;
            _attackPower = 5f;
            _knockbackPower = 3f;
        }*/

        public void SkillTreeSetStats(int newHealth, float newSpeed, float newAttack, float newKnockback)
        {
            _maxHealth = newHealth;
            _speed = newSpeed;
            _attackPower = newAttack;
            _knockbackPower = newKnockback;
        }

        public void SkillTreeSetDeck(List<Card> newDeck)
        {
            _deck = new List<Card>();
            _deck.AddRange(newDeck);
        }

    } 
}

