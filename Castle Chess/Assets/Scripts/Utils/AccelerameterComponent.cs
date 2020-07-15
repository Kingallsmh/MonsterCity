using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccelerameterComponent : MonoBehaviour, IEnergyCollect
{
    Vector3 lastaccel;
    float wantedDif = 1.0f;

    public float timeTillNextScore = 0.01f;
    float cTime = 0;

    int HandleCollectEnergy()
    {
        Vector3 accel = Input.acceleration;
        float dif = (lastaccel - accel).magnitude;
        if (dif > wantedDif && cTime > timeTillNextScore)
        {            
            cTime = 0;
            return 1;
        }

        cTime += Time.deltaTime;
        lastaccel = accel;
        return 0;
    }

    public int GetEnergyScore()
    {        
        return HandleCollectEnergy();
    }
}
