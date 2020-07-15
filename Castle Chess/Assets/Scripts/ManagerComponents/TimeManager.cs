using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    public float gameTime = 0;
    public float timeTickAmount = 1; //Use this to increase how fast things "decay". Higher means food and time alive will be shorter.
    DateTime lastMinimize;
    bool runTickCount = false;

    List<ILifeSpan> lifeSpanList;

    public static Action<float> OnTimeTick = delegate { };

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        lifeSpanList = new List<ILifeSpan>();
        StartCoroutine(HandleTime());        
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            lastMinimize = DateTime.Now;
            runTickCount = true;
        }
        else
        {
            if (runTickCount)
            {
                CalculateTimePassed();
                runTickCount = false;
            }
        }
        
    }

    public void AddToLifeSpanList(ILifeSpan _life)
    {
        if(lifeSpanList == null)
        {
            lifeSpanList = new List<ILifeSpan>();
        }
        lifeSpanList.Add(_life);
    }

    public void RemoveFromLifeSpanList(ILifeSpan _life)
    {
        lifeSpanList.Remove(_life);
    }

    IEnumerator HandleTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            UpdateTick();
        }
    }

    void UpdateTick()
    {
        UpdateTick(timeTickAmount);
    }

    void CalculateTimePassed()
    {
        float totalSeconds = (float)(DateTime.Now - lastMinimize).TotalSeconds * timeTickAmount;
        UpdateTick(Mathf.Round(totalSeconds));
        Debug.Log("Time from pause added: " + totalSeconds);
    }

    void UpdateTick(float _timePassed)
    {
        gameTime += _timePassed;
        //for(int i = 0; i < lifeSpanList.Count; i++)
        //{
        //    lifeSpanList[i].TimeTick(_timePassed);
        //}
        OnTimeTick(_timePassed);
        //PenManager.Instance.UpdateTickTimerForCreatures(_timePassed);
    }

    public void AddTicksSinceLastOn(DateTime lastOn)
    {
        float timeSinceOn = (float)(DateTime.Now - lastOn).TotalSeconds;
        UpdateTick(timeSinceOn);
    }

    public static string GetTimeString(float secs)
    {
        StringBuilder build = new StringBuilder();

        //This will give you hours, mins that aren't enough to make a full hour, and seconds that aren't enough for a min
        //Debug.Log(TimeSpan.FromSeconds(secs).Hours + " hours"); //Outputs 3
        //Debug.Log(TimeSpan.FromSeconds(secs).Minutes + " minutes"); //Outputs 46
        //Debug.Log(TimeSpan.FromSeconds(secs).Seconds + " seconds"); //Outputs 40

        int days = TimeSpan.FromSeconds(secs).Days;
        int hours = TimeSpan.FromSeconds(secs).Hours;
        int minutes = TimeSpan.FromSeconds(secs).Minutes;
        int seconds = TimeSpan.FromSeconds(secs).Seconds;

        build.Append(days).Append(" : ").Append(hours).Append(" : ").Append(minutes).Append(" : ").Append(seconds);
        return build.ToString();
    }
}
