using DataRecording;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataRecording
{
    /// <summary>
    /// Class that contains the list of DataContainers and used as a
    /// timeline for a single variable.
    /// </summary>
    [Serializable]
    public class DataStorage
    {
        string variableName;
        object data;

        public DataStorage(string _variableName)
        {
            variableName = _variableName;
        }

        public DataStorage(DataStorage toCopy)
        {
            variableName = toCopy.variableName;
            data = toCopy.data;
        }

        public void SetObject(object _data)
        {
            data = _data;
        }

        public object GetData()
        {
            return data;
        }

        public bool ValueDifferentFromPreviousData(object _data)
        {
            if (data != null)
            {
                return !Equals(data, _data);
            }
            return true;
        }        

        public string GetVariableName()
        {
            return variableName;
        }
    }
}

