using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourPattern: MonoBehaviour
{
    public abstract void StartBehaviour();

    public abstract void UpdateBehaviour();

    public abstract void StopBehaviour();

    private void Awake()
    {
        StartBehaviour();
    }

    private void Update()
    {
        UpdateBehaviour();
    }

    private void OnDisable()
    {
        StopBehaviour();
    }    
}
