//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SearchForBehaviour : BehaviourPattern
//{
//    CreatureAction action;
//    GameObject objectToReach;

//    public SearchForBehaviour(CreatureAction _action, GameObject _obj)
//    {
//        action = _action;
//        ChangeObjectToSearchFor(_obj);
//    }

//    public override void BehaviourUpdate()
//    {
//        WalkTowardsObject();
//    }

//    public override void StopBehaviour()
//    {
//        action.Move(Vector2.zero);
//    }

//    public void ChangeObjectToSearchFor(GameObject _obj)
//    {
//        objectToReach = _obj;
//    }

//    void WalkTowardsObject()
//    {
//        Vector2 difference = CreateDirectionNeeded();
//        if(difference.magnitude < 2)
//        {
//            action.Move(Vector2.zero);
//        }
//        else
//        {
//            action.Move(CreateDirectionNeeded().normalized);
//        }
        
//    }

//    Vector2 CreateDirectionNeeded()
//    {
//        return objectToReach.transform.position - action.transform.position;
//    }
//}
