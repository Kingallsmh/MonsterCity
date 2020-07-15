using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUGAccelerameter : MonoBehaviour, IEnergyCollect
{
    public bool watchCollection = true;
    public int score = 0;

    public bool runAutoScore = false;
    public float timeTillNextPoint = 1;
    float cTime = 0;

    // Update is called once per frame
    void Update()
    {
        if (watchCollection)
        {
            if (runAutoScore)
            {
                HandleAutoPoint();
            }
        }        
    }

    void HandleAutoPoint()
    {
        if(cTime > timeTillNextPoint)
        {
            score++;
            cTime = 0;
        }
        cTime += Time.deltaTime;
    }

    public int GetEnergyScore()
    {
        int tempScore = score;
        score = 0;
        return tempScore;
    }

    public void WatchForCollection(bool _collect)
    {
        watchCollection = _collect;
    }
}
