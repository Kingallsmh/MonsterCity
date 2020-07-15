//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class FoodComponent : StatEffectComponent
//{
//    [Header("Food Specifics")]
//    public float foodRequirement;

//    public override void EffectCreature(CreatureMain _creature)
//    {        
//        //These are the effects from the food
//        _creature.GetCreatureStats().AddToStatsData(GetStatsFromList());
//        //Intended as the creature needs to be a certain amount of hungry before it'll eat
//        //This is the food value it'ss get once eating
//        _creature.GetCreatureStats().AddToStat(StatType.Food, foodRequirement);
//        PenManager.Instance.SetFoodIntoPool(this);
//    }

//    public Collider2D GetTriggerCollider()
//    {
//        Collider2D[] colliderList = GetComponentsInChildren<Collider2D>();
//        for(int i = 0; i < colliderList.Length; i++)
//        {
//            if (colliderList[i].isTrigger)
//            {
//                return colliderList[i];
//            }
//        }
//        return null;
//    }

//    public Collider2D GetPhysicalCollider()
//    {
//        Collider2D[] colliderList = GetComponentsInChildren<Collider2D>();
//        for (int i = 0; i < colliderList.Length; i++)
//        {
//            if (!colliderList[i].isTrigger)
//            {
//                return colliderList[i];
//            }
//        }
//        return null;
//    }
//}
