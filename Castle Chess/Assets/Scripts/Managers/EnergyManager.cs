using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{
    public IEnergyCollect currentCollector;
    public StepCounterWrapper bgStepCount;
    bool isBGInit = false;
    public static bool watchCollection = true;
    public static int collectedEnergy = 0;

    public static Action<int> OnEnergyCollected = delegate { };

    private void Awake()
    {
        bgStepCount = new StepCounterWrapper();
        currentCollector = GetComponent<IEnergyCollect>();
    }

    private void FixedUpdate()
    {
        if (watchCollection)
        {
            collectedEnergy += currentCollector.GetEnergyScore();
            OnEnergyCollected(collectedEnergy);
        }
    }

    public static void SetCollectingEnergy(bool _collect)
    {
        watchCollection = _collect;
    }

    public static int GetCurrentBuilt()
    {
        int tempEnergy = collectedEnergy;
        collectedEnergy = 0;
        return tempEnergy;
    }

    private void OnApplicationPause(bool pause)
    {
#if UNITY_ANDROID
        if (pause)
        {
            StartBGEnergyCollection();
            isBGInit = true;
        }
        else
        {
            if (isBGInit)
            {
                GetBGBuiltEnergy();
                isBGInit = false;
            }

        }
#endif
    }

    void StartBGEnergyCollection()
    {
        bgStepCount.InitStepCounter();
    }

    void GetBGBuiltEnergy()
    {
        collectedEnergy += bgStepCount.FinishStepCounter();
    }
}
