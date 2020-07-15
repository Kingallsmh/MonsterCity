using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public List<GameObject> UIObjects;

    private void Awake()
    {
        Instance = this;
    }

    public void AddNewUIObj(GameObject _uiObj)
    {

    }
}
