using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : BehaviourPattern
{
    Rigidbody2D rb;
    TimerObject patternTimer;
    Vector2 moveDirection;
    float speed;

    public Vector2 speedRange = new Vector2(1, 3);
    public Vector2 timeRange = new Vector2(1, 4);

    public override void StartBehaviour()
    {
        rb = GetComponent<Rigidbody2D>();
        patternTimer = new TimerObject(GenerateTime());
    }

    public override void UpdateBehaviour()
    {
        WalkRandomly();
        UpdateRB();
    }

    public override void StopBehaviour()
    {
        moveDirection = Vector2.zero;
        UpdateRB();
    }

    void UpdateRB()
    {
        rb.velocity = moveDirection * speed * 500 * Time.deltaTime;
    }

    void WalkRandomly()
    {
        if (patternTimer.IsTimerDone())
        {
            moveDirection.x = GenerateDirection();
            moveDirection.y = GenerateDirection();
            moveDirection.Normalize();
            speed = GenerateSpeed();
            patternTimer.SetNewTime(GenerateTime());
            patternTimer.ResetTimer();
        }
        else
        {
            patternTimer.StepTimer();
        }
    }

    float GenerateTime()
    {
        float rndTime = Random.Range(timeRange.x, timeRange.y);
        //Debug.Log(rndTime);
        return rndTime;
    }

    float GenerateDirection()
    {
        float rndDir = Random.Range(-1, 2);
        //Debug.Log(rndDir);
        return rndDir;
    }

    float GenerateSpeed()
    {
        float rndDir = Random.Range(speedRange.x, speedRange.y);
        //Debug.Log(rndDir);
        return rndDir;
    }
}
