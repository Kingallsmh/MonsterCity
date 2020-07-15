//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CreatureEgg : MonoBehaviour, ILifeSpan
//{
//    public float currentTime = 0;
//    public float hatchTime = 120;

//    public void CreateEgg()
//    {
//        TimeManager.Instance.AddToLifeSpanList(this);
//    }

//    public void TimeTick(float _timePassed)
//    {
//        currentTime += _timePassed;
//        if(currentTime > hatchTime)
//        {
//            HatchEgg();
//        }
//    }

//    public void HatchEgg()
//    {
//        TimeManager.Instance.RemoveFromLifeSpanList(this);
//        CreatureMain newPet = PetGameManger.Instance.GetCreatureBank().GetRandomNewCreatureOfSpecies();
//        newPet.InitCreature();
//        newPet.transform.position = transform.position;
//        Destroy(gameObject);
//    }
//}
