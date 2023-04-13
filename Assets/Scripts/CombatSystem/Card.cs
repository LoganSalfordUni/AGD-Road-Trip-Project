using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//notes about the cards. cards with knockback should be high mana cost 
//set up cards should also have a high mana cost 
//most generic damage cards should be a low mana cost 

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


        public enum TargettingStyle
        {
            singleTarget,
            Cone,
            clickToSpawnExplosive
        }
        [SerializeField] private TargettingStyle _targettingStyle;
        public TargettingStyle targettingStyle { get { return _targettingStyle; } }


        public enum AbilityEffects
        {
            knockback,
            cantBeRerolled
        }
        [SerializeField] private AbilityEffects[] _abilityEffects;
        public AbilityEffects[] abilityEffects { get { return _abilityEffects; } }
    }
}


