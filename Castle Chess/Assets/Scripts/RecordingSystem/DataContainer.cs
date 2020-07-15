using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataRecording
{
    [Serializable]
    public struct DataContainer
    {
        public object dataRec;

        public DataContainer(object _data)
        {
            dataRec = _data;            
        }

        public DataContainer(DataContainer _toCopy)
        {
            dataRec = _toCopy.dataRec;
        }

        public bool CompareNewValue(object _newData)
        {
            return Equals(dataRec, _newData);
        }
    }
}

