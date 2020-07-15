using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DataRecording
{
    public class DataRecordingHandler : MonoBehaviour
    {
        DataSceneCollection dataScene;
        List<IDataRec> dataRecList;
        List<DataObjRef> dataObjRefs;

        int frame;
        float time;
        public RecordingState recState = RecordingState.Idle;
        public bool playbackLooping = false;

        public Action<RecordingState> OnRecordStateChanged = delegate { };
        public Action<float> OnTimeChanged = delegate { };
        public Action<int> OnFrameChanged = delegate { };

        public void InitDataRecHandler()
        {
            if (dataRecList == null)
            {
                dataScene = new DataSceneCollection();
                dataRecList = new List<IDataRec>();
                dataObjRefs = new List<DataObjRef>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            switch (recState)
            {
                case RecordingState.Idle:
                    break;
                case RecordingState.Recording:
                    HandleRecording();
                    break;
                case RecordingState.Playing:
                    HandlePlayback();
                    break;
            }
        }

        private void OnDestroy()
        {
            OnRecordStateChanged = null;
            OnFrameChanged = null;
            OnTimeChanged = null;
        }

        public void SetTime(float _time)
        {
            time = _time;
            frame = Mathf.RoundToInt(time / dataScene.totalTime * dataScene.totalFrames);
        }

        #region Recording

        void HandleRecording()
        {
            for(int i = 0; i < dataRecList.Count; i++)
            {
                dataRecList[i].RecordData(frame, time);
            }
            frame++;
            time += Time.deltaTime;
        }        

        public void StartRecording()
        {
            ChangeRecState(RecordingState.Recording);
        }

        public void StopRecording()
        {
            ChangeRecState(RecordingState.Idle);
            for (int i = 0; i < dataRecList.Count; i++)
            {
                dataRecList[i].FinishRecordData(frame, time);
            }
        }

        #endregion

        #region Playback        

        void HandlePlayback()
        {
            for (int i = 0; i < dataRecList.Count; i++)
            {
                dataRecList[i].GoToTime(time);
            }
            time += Time.deltaTime;
            if (time >= dataScene.totalTime)
            {
                HandleEndOfPlayback();
            }
        }

        public void SetLooping(bool _isLooping)
        {
            playbackLooping = _isLooping;
        }

        public void StartPlayback(bool _isLooping = false)
        {
            SetLooping(_isLooping);
            ChangeRecState(RecordingState.Playing);
        }

        public void SkipToFrame(int _frame)
        {
            float estTime = _frame / dataScene.totalFrames * dataScene.totalTime;
            SkipToFrame(estTime);
        }

        public void SkipToFrame(float _time)
        {
            SetTime(_time);
            for (int i = 0; i < dataRecList.Count; i++)
            {
                dataRecList[i].GoToTime(_time);
            }
        }

        public void StopPlayback()
        {
            ChangeRecState(RecordingState.Idle);
        }

        void HandleEndOfPlayback()
        {
            if (playbackLooping)
            {
                time = 0;
            }
            else
            {
                StopPlayback();
            }
        }        

        #endregion

        public DataSceneCollection CreateDataSceneCollection()
        {
            dataScene = new DataSceneCollection();
            dataScene.SetTimeFrames(frame, time);
            for (int i = 0; i < dataRecList.Count; i++)
            {
                dataScene.AddToDataRecList(dataRecList[i].GetDataObjCollection());
            }
            return dataScene;
        }

        public void LoadDataSceneCollection(DataSceneCollection _sceneCollection)
        {
            dataScene = _sceneCollection;
            for(int i = 0; i < dataScene.GetDataRecList().Count; i++)
            {
                LoadToDataRec(dataScene.GetDataRecList()[i]);
            }
        }

        void LoadToDataRec(DataObjCollection _collection)
        {
            for(int i = 0; i < dataObjRefs.Count; i++)
            {
                if(dataObjRefs[i].objName == _collection.GetName())
                {
                    SetMatchingCollections(dataObjRefs[i], _collection);
                }
            }
        }

        void SetMatchingCollections(DataObjRef _collectionsToTry, DataObjCollection _collectionToMatch)
        {
            for(int i = 0; i < _collectionsToTry.objComponents.Count; i++)
            {
                if (_collectionsToTry.objComponents[i].CompareClassTypeByString(_collectionToMatch.GetClassName()))
                {
                    _collectionsToTry.objComponents[i].GiveDataFromObjCollection(_collectionToMatch);
                }
            }            
        }

        public void AddToDataRecList(IDataRec _dataRec)
        {
            dataRecList.Add(_dataRec);
            AddToObjRefs(_dataRec.GetDataObjCollection());
        }

        void AddToObjRefs(DataObjCollection _collection)
        {
            for (int i = 0; i < dataObjRefs.Count; i++)
            {
                if(_collection.GetName() == dataObjRefs[i].objName)
                {
                    dataObjRefs[i].objComponents.Add(_collection);
                }
            }
            dataObjRefs.Add(new DataObjRef(_collection.GetName(), _collection));
        }

        public void ClearDataRecList()
        {
            dataRecList = new List<IDataRec>();
            dataObjRefs = new List<DataObjRef>();
        }

        public string GetAllWriteable()
        {
            StringBuilder build = new StringBuilder();
            build.Append("Date: ").Append(dataScene.recordDate).AppendLine();
            build.Append("Time Elapsed: ").Append(dataScene.totalTime).AppendLine();
            build.Append("Total Frames: ").Append(dataScene.totalFrames).AppendLine();
            build.AppendLine();
            for(int i = 0; i < dataRecList.Count; i++)
            {
                build.Append(dataRecList[i].GetWritableData()).AppendLine();
            }
            return build.ToString();
        }

        public void ChangeRecState(RecordingState _state)
        {
            recState = _state;
            OnRecordStateChanged(recState);
        }

        struct DataObjRef
        {
            public string objName;
            public List<DataObjCollection> objComponents;

            public DataObjRef(string _name, DataObjCollection _collection)
            {
                objName = _name;
                objComponents = new List<DataObjCollection>();
                objComponents.Add(_collection);
            }
        }

    }

    

    public enum RecordingState
    {
        Idle, Recording, Playing
    }
}

