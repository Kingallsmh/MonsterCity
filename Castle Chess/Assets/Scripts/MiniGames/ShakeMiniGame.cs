using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeMiniGame : MonoBehaviour
{
    //Ref to a shaker or shake manager
    public float totalPlayTime = 5;
    float elapsedTime = 0;
    public float maxEnergy = 20;
    float energyCollected = 0;
    float absolueMaxEnergy = 0;

    public void InitGame()
    {
        //Start EnergyManager to collect
        EnergyManager.SetCollectingEnergy(true);
        absolueMaxEnergy = maxEnergy + Mathf.Floor(maxEnergy / 5);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(elapsedTime < totalPlayTime)
        {
            energyCollected = Mathf.Clamp(energyCollected + EnergyManager.GetCurrentBuilt(), 0, absolueMaxEnergy);
            elapsedTime = Time.deltaTime;
        }
    }

    public bool GameIsFinished()
    {
        return elapsedTime > totalPlayTime;
    }

    public void EndGame()
    {
        //Stop EnergyManager from collecting, let next operation decide
        EnergyManager.SetCollectingEnergy(false);
    }

    public float GetResult()
    {
        return energyCollected / maxEnergy;
    }
}
