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

        [SerializeField] private float _maxHealth;
        public float maxHealth { get { return _maxHealth; } }
    }
}

