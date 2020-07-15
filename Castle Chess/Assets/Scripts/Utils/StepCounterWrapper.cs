using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepCounterWrapper
{
    AndroidJavaObject javaObj;
    AndroidJavaObject unityActivity;
    AndroidJavaObject unityContext;
    AndroidJavaClass unityClass;

    // Start is called before the first frame update
    public void InitStepCounter()
    {
        unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        unityActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
        unityContext = unityActivity.Call<AndroidJavaObject>("getApplicationContext");
        javaObj = new AndroidJavaObject("com.example.bgcountlibrary.ResponseBroadcastReceiver");
        javaObj.CallStatic("receiveActivityInstance", unityContext);
        startService();
    }

    // Update is called once per frame
    public int FinishStepCounter()
    {
        stopService();
        return javaObj.Call<int>("GetBuiltScore");
    }

    void startService()
    {
        javaObj.Call("StartCheckerService");
    }

    void stopService()
    {
        Debug.Log("CountStep App should be quitting...");
        javaObj.CallStatic("StopCheckerService");
    }
}
