using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    [CreateAssetMenu]
    public class Card : ScriptableObject
    {
        [Header("Card Presentation")]
        [SerializeField] private string _abilityName;
        public string abilityName { get { return _abilityName; } }

        [Header("Card Attributes")]
        [SerializeField] private int _manaCost;
        public int manaCost { get { return _manaCost; } }

        [SerializeField] private float _abilityDistance;
        public float abilityDistance { get { return _abilityDistance; } }//30 is high, 10 is basically melee

        [SerializeField] private int _damageValue;
        public int damageValue { get { return _damageValue; } }

        [SerializeField] private float _knockbackValue;
        public float knockbackValue { get { return _knockbackValue; } }


        public enum TargettingStyle
        {
            singleTarget,
            Cone
        }
        [SerializeField] private TargettingStyle _targettingStyle;
        public TargettingStyle targettingStyle { get { return _targettingStyle; } }

        [SerializeField] private bool _canBeRerolled = true;
        public bool canBeRerolled { get { return _canBeRerolled; } }
    }
}


