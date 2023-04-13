using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    public class SkillTreeButton : MonoBehaviour
    {
        //plan: you click a button on the skill tree, and it enables other buttons, and gives you stats
        //the stats the button gives you is randomised from an array.
        //when you scroll your mouse over 

        [SerializeField] string description;


        [SerializeField, Tooltip("The cost of choosing this skill")] int cost;
        enum stats
        {
            health,
            power,
            knockback
        }

        /*[System.Serializable]
        struct statAdjustment
        {
            [SerializeField] stats statToAdjust;
            [SerializeField, Tooltip("Note: some stats will be rounded down as they need to be integers")] float statChange;//might be better to change this to like, a number 1-5 for a rating for how high the stat change is. and adjust the exact shift in the manager
            [SerializeField, Range(1, 5), Tooltip("1-5, what is the rating of this stat improvement")] int statChange;
        }
        [SerializeField, Tooltip("I'll choose a random one of these stats to change")] statAdjustment[] possibleStatChanges;*/

        [SerializeField] stats[] possibleStatsToAdjust;
        [SerializeField, Range(1, 5), Tooltip("1-5, what is the rating of this stat improvement")] int statChangeRating;

        [SerializeField, Tooltip("I'll choose a random card from this list to add to the players deck")] Card[] possibleCardGains;

        [SerializeField, Tooltip("Make these game objects active, when I'm clicked")] SkillTreeButton[] connections;

        bool hasBeenEnabledAlready;
        bool canClick;

        public void EnableMe()
        {
            if (hasBeenEnabledAlready)//dont let this be enabled multiple times 
                return;

            hasBeenEnabledAlready = true;
            canClick = true;
        }
        private void OnMouseEnter()
        {
            SkillTreeManager.instance.ShowChoiceDescription(description);
        }

        private void OnMouseExit()
        {
            SkillTreeManager.instance.HideDescription();
        }

        private void OnMouseDown()
        {
            if (!canClick)
            {
                SkillTreeManager.instance.ShowChoiceDescription("You've already chosen this ability");
                return;
            }

            canClick = false;
            foreach (SkillTreeButton connect in connections)
            {
                connect.EnableMe();
            }

            //choose what stats to increase
            int randomStat = Random.Range(0, possibleStatsToAdjust.Length);
            int randomCard = Random.Range(0, possibleCardGains.Length);



        }
    }

}
