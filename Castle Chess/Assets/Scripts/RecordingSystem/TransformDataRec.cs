//using DataRecording;
//using System.Collections;
//using System.Collections.Generic;
//using System.Text;
//using UnityEngine;

//public class TransformDataRec : MonoBehaviour, IDataRec
//{
//    DataObjCollection objCollection;
//    DataAssign<SizedVector> positionTrack;
//    DataAssign<SizedVector> rotationTrack;
//    DataAssign<SizedVector> sizeTrack;
//    DataAssign<bool> activeTrack;

//    public bool trackSize = false;
//    public bool trackActive = false;

//    bool isInitialized = false;

//    // Start is called before the first frame update
//    void Start()
//    {
//        InitTranformRec();
//    }

//    public void InitTranformRec()
//    {
//        if (!isInitialized)
//        {
//            objCollection = new DataObjCollection(gameObject.name, this.GetType());
//            positionTrack = new DataAssign<SizedVector>("positionTrack");
//            objCollection.AddDataStorage(positionTrack);
//            rotationTrack = new DataAssign<SizedVector>("rotationTrack");
//            objCollection.AddDataStorage(rotationTrack);
//            if (trackSize)
//            {
//                sizeTrack = new DataAssign<SizedVector>("sizeTrack");
//                objCollection.AddDataStorage(sizeTrack);
//            }
//            if (trackActive)
//            {
//                activeTrack = new DataAssign<bool>("activeTrack");
//                objCollection.AddDataStorage(activeTrack);
//            }

//            DataRecManager.Instance.AddToRecordingList(this);
//            isInitialized = true;
//        }        
//    }

//    public void FinishRecordData(int _frame, float _time)
//    {
//        positionTrack.AddLastFrame(_frame, _time, new SizedVector(transform.position));
//        rotationTrack.AddLastFrame(_frame, _time, new SizedVector(transform.rotation));
//        if (trackSize) sizeTrack.AddLastFrame(_frame, _time, new SizedVector(transform.localScale));
//        if (trackActive) activeTrack.AddLastFrame(_frame, _time, gameObject.activeSelf);        
//    }

//    public DataObjCollection GetDataObjCollection()
//    {
//        return objCollection;
//    }

//    public string GetWritableData()
//    {
//        StringBuilder builder = new StringBuilder();
//        builder.Append("Object: ").Append(objCollection.GetName());
//        builder.Append("Component: ").Append(objCollection.GetClassName()).AppendLine();
//        builder.Append(positionTrack.GetWritableFromVariable()).AppendLine();
//        builder.Append(rotationTrack.GetWritableFromVariable()).AppendLine();
//        if (trackSize)  builder.Append(sizeTrack.GetWritableFromVariable()).AppendLine();
//        if(trackActive) builder.Append(activeTrack.GetWritableFromVariable()).AppendLine();
//        return builder.ToString();
//    }

//    public void GoToFrame(int _frame)
//    {
//        transform.position = positionTrack.GetFromStorage(_frame).ReturnVector3();
//        transform.rotation = rotationTrack.GetFromStorage(_frame).ReturnQuaternion();
//        if (trackSize) transform.localScale = sizeTrack.GetFromStorage(_frame).ReturnVector3();
//        if (trackActive) gameObject.SetActive(activeTrack.GetFromStorage(_frame));
//    }

//    public void GoToTime(float _time)
//    {
//        transform.position = positionTrack.GetFromStorage(_time).ReturnVector3();
//        transform.rotation = rotationTrack.GetFromStorage(_time).ReturnQuaternion();
//        if (trackSize) transform.localScale = sizeTrack.GetFromStorage(_time).ReturnVector3();
//        if (trackActive) gameObject.SetActive(activeTrack.GetFromStorage(_time));
//    }

//    public void RecordData(int _frame, float _time)
//    {
//        positionTrack.AddToStorage(_frame, _time, new SizedVector(transform.position));
//        rotationTrack.AddToStorage(_frame, _time, new SizedVector(transform.rotation));
//        if (trackSize) sizeTrack?.AddToStorage(_frame, _time, new SizedVector(transform.localScale));
//        if (trackActive) activeTrack?.AddToStorage(_frame, _time, gameObject.activeSelf);
//    }

//    public void RemoveCurrentData()
//    {
//        objCollection.ClearStorage();
//    }

    
//}
