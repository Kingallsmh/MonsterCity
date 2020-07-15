using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataRecording
{
    public interface IDataRec
    {        
        void RecordData(int _frame, float _time);
        void FinishRecordData(int _frame, float _time);
        void GoToFrame(int _frame);
        void GoToTime(float _time);
        void RemoveCurrentData();
        DataObjCollection GetDataObjCollection();
        string GetWritableData();
    }
}

