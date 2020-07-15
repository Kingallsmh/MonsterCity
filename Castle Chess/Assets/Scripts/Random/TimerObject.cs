using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TimerObject
{
    public float totalTime;
    public float currentTime;

    public TimerObject(float _totalTime)
    {
        totalTime = _totalTime;
    }

    public bool StepTimer()
    {
        currentTime += Time.deltaTime;
        return IsTimerDone();
    }

    public bool IsTimerDone()
    {
        return currentTime >= totalTime;
    }

    public void ResetTimer()
    {
        currentTime = 0;
    }

    public void SetNewTime(float time)
    {
        totalTime = time;
    }
}
