using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Pool <T>
{
    List<T> activePool;
    List<T> unactivePool;

    bool isInitialized;

    public Pool()
    {
        activePool = new List<T>();
        unactivePool = new List<T>();
    }

    public Pool(List<T> _objList)
    {
        activePool = new List<T>();
        unactivePool = _objList;
    }

    public void AddToUnactive(T _obj)
    {
        unactivePool.Add(_obj);
    }

    public void AddToUnactive(List<T> _objList)
    {
        unactivePool.AddRange(_objList);
    }

    public void AddToActive(T _obj)
    {
        activePool.Add(_obj);
    }

    public void AddToActive(List<T> _objList)
    {
        activePool.AddRange(_objList);
    }

    public T GetNextObject()
    {
        if (UnactiveListEmpty())
        {
            Debug.LogWarning("Not enough objects in list");
            return default(T);
        }

        T obj = unactivePool[0];
        unactivePool.RemoveAt(0);
        activePool.Add(obj);
        return obj;
    }

    public void ReturnObject(T _obj)
    {
        activePool.Remove(_obj);
        unactivePool.Add(_obj);
    }

    public void SwitchObjectState(T _obj)
    {
        activePool.Add(_obj);
        unactivePool.Remove(_obj);
    }

    public void ReturnFirstActiveToUnactive()
    {
        ReturnObject(activePool[0]);
    }

    public void RemoveObjectFromPool(T _obj)
    {
        unactivePool.Remove(_obj);
        activePool.Remove(_obj);
    }

    public void ClearActive()
    {
        for (int i = 0; i < activePool.Count; i++)
        {
            unactivePool.Add(activePool[i]);
        }

        activePool = new List<T>();
    }

    public bool UnactiveListEmpty()
    {
        return unactivePool.Count == 0;
    }

    public int GetUnactiveAmount()
    {
        return unactivePool.Count;
    }

    public List<T> GetUnactiveList()
    {
        return unactivePool;
    }

    public T GetUnactiveObject(int _num)
    {
        return unactivePool[_num];
    }

    public int GetActiveAmount()
    {
        return activePool.Count;
    }

    public T GetActiveObject(int _num)
    {
        return activePool[_num];
    }

    public List<T> GetActiveList()
    {
        return activePool;
    }

    public List<T> GetCombinedList()
    {
        List<T> list = new List<T>();
        list.AddRange(activePool);
        list.AddRange(unactivePool);
        return list;
    }

    public bool ListsContainObject(T _obj)
    {        
        if (unactivePool.Contains(_obj))
        {
            return true;
        }
        else if (activePool.Contains(_obj))
        {
            return true;
        }
        return false;
    }
}
