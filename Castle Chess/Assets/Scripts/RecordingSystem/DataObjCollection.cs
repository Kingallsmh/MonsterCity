using DataRecording;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataRecording
{
    [Serializable]
    public class DataObjCollection
    {
        string objName;
        string className;
        List<DataStorage> dataStorageList;

        public DataObjCollection(string _objName, Type _component)
        {
            objName = _objName;
            className = _component.FullName;
            dataStorageList = new List<DataStorage>();
        }

        public void GiveDataFromObjCollection(DataObjCollection _objCollection)
        {
            for(int i = 0; i < _objCollection.GetStorageList().Count; i++)
            {
                GiveDataFromStorage(_objCollection.GetStorageList()[i]);
            }
        }

        public void GiveDataFromStorage(DataStorage _newStorage)
        {
            DataStorage dataStore = GetMatchingStorage(_newStorage.GetVariableName());
            if (dataStore != null)
            {
                dataStore.SetObject(_newStorage.GetData());
            }            
        }

        DataStorage GetMatchingStorage(string _storageName)
        {
            for(int i = 0; i < dataStorageList.Count; i++)
            {
                if(dataStorageList[i].GetVariableName() == _storageName)
                {
                    return dataStorageList[i];
                }
            }
            return null;
        }

        public bool CompareClassTypeByType(string _compare)
        {
            return Type.GetType(className).Equals(Type.GetType(_compare));
        }

        public bool CompareClassTypeByString(string _compare)
        {
            return className == _compare;
        }

        public void ClearStorage()
        {
            for(int i = 0; i < dataStorageList.Count; i++)
            {
                dataStorageList[i] = null;
            }
        }        

        public void AddDataStorage(DataStorage _storage)
        {
            dataStorageList.Add(_storage);
        }

        public void AddDataStorage<T>(DataAssign<T> _assign)
        {
            dataStorageList.Add(_assign.storage);
        }

        public void RemoveDataStorage(DataStorage _storage)
        {
            dataStorageList.Remove(_storage);
        }

        public DataStorage GetDataStorage(string _storageName)
        {
            for(int i = 0; i < dataStorageList.Count; i++)
            {
                if (dataStorageList[i].GetVariableName().Equals(_storageName))
                {
                    return dataStorageList[i];
                }
            }
            return null;
        }

        public string GetName()
        {
            return objName;
        }

        public string GetClassName()
        {
            return className;
        }

        public List<DataStorage> GetStorageList()
        {
            return dataStorageList;
        }
    }
}

