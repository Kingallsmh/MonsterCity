using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataRecording
{
    [Serializable]
    public class DataSceneCollection
    {
        List<DataObjCollection> dataRecList;

        public int totalFrames;
        public float totalTime;
        public string recordDate;

        public DataSceneCollection()
        {
            dataRecList = new List<DataObjCollection>();
            recordDate = DateTime.Now.ToLocalTime().ToString();
        }

        public void SetTimeFrames(int _frames, float _time)
        {
            totalFrames = _frames;
            totalTime = _time;
        }

        public void AddToDataRecList(DataObjCollection _dataCollection)
        {
            dataRecList.Add(_dataCollection);
        }

        public List<DataObjCollection> GetDataRecList()
        {
            return dataRecList;
        }

        public void ClearRecordingInDataList()
        {
            for(int i = 0; i < dataRecList.Count; i++)
            {
                dataRecList[i].ClearStorage();
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

