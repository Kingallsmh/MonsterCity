using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour
{
    public static DebugManager Instance;

    public Text debugText;
    public bool allowDebug;
    StringBuilder builder;
    List<DebugSlot> debugList;

    private void Awake()
    {
        Instance = this;
        builder = new StringBuilder();
        debugList = new List<DebugSlot>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (allowDebug)
        {
            debugText.gameObject.SetActive(true);

            BuildTotalMessage();
            debugText.text = builder.ToString();
            builder = new StringBuilder();
        }
        else
        {
            debugText.gameObject.SetActive(false);
        }
    }

    public void BuildTotalMessage()
    {
        for(int i = 0; i < debugList.Count; i++)
        {
            AddToDebugString(debugList[i].title, debugList[i].message);
        }
    }

    public void AddToDebugString(string _debug)
    {
        builder.Append(_debug).AppendLine();
    }

    public void AddToDebugString(string _debugTitle, string _debugString)
    {
        builder.Append(_debugTitle).Append(": ").Append(_debugString).AppendLine();
    }

    public void AddDebugSlot(DebugSlot _slot)
    {
        debugList.Add(_slot);
    }
}

public class DebugSlot
{
    public string title;
    public string message;
}

