using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataRecording;
using System;
using UnityEngine.Events;

[RequireComponent(typeof(DataRecordingHandler))]
public class DataRecManager : MonoBehaviour
{
    public static DataRecManager Instance;
    DataRecordingHandler recHandler;
    DataFileIO<DataSceneCollection> sceneIO;
    DataFileIO<string> writableIO;

    public string fileName;
    public string fileExtension;
    public string fileLocation;

    bool isInitialized = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitDataRecManager();
    }

    public void InitDataRecManager()
    {
        if (!isInitialized)
        {
            recHandler = GetComponent<DataRecordingHandler>();
            recHandler.InitDataRecHandler();
            //sceneIO = new DataFileIO<DataSceneCollection>(fileName, fileExtension);
            
            //writableIO = new DataFileIO<string>(fileName + "Read", "read");
            isInitialized = true;
        }
    }

    public void StartRecording()
    {
        recHandler.StartRecording();
    }

    public void StopRecording()
    {
        recHandler.StopRecording();
    }

    public void ResetToBeginning()
    {
        recHandler.SkipToFrame(0);
    }

    public void StartPlayback()
    {
        recHandler.StartPlayback();
    }

    public void StopPlayback()
    {
        recHandler.StopPlayback();
    }

    public void SetFileName(string _fileName, string _extension)
    {
        //sceneIO.fileName = _fileName;
        //sceneIO.extension = _extension;
    }

    public void WriteToFile(string _location)
    {
        //DataSceneCollection sceneCollect = recHandler.CreateDataSceneCollection();
        //sceneIO.fileName += sceneCollect.recordDate;
        //sceneIO.SetFileLocation(_location);
        //sceneIO.WriteData(sceneCollect);

        //writableIO.SetFileLocation(_location);
        //writableIO.WriteData(recHandler.GetAllWriteable());
    }

    public void WriteToFile()
    {
        WriteToFile(fileLocation);
    }

    public void LoadFromFile(string _location)
    {
        //sceneIO.SetFileLocation(_location);
        //recHandler.LoadDataSceneCollection(sceneIO.LoadData());
    }

    public void AddToRecordingList(IDataRec _rec)
    {
        recHandler.AddToDataRecList(_rec);
    }

    public void RegisterToRecordStateChange(Action<RecordingState> _method)
    {
        recHandler.OnRecordStateChanged += _method;
    }

    public void UnregisterFromRecordStateChange(Action<RecordingState> _method)
    {
        recHandler.OnRecordStateChanged -= _method;
    }

    public void RegisterToTimeChanged(Action<float> _method)
    {
        recHandler.OnTimeChanged += _method;
    }

    public void UnregisterFromTimeChanged(Action<float> _method)
    {
        recHandler.OnTimeChanged -= _method;
    }

    public void RegisterToFrameChanged(Action<int> _method)
    {
        recHandler.OnFrameChanged += _method;
    }

    public void UnregisterFromFrameChanged(Action<int> _method)
    {
        recHandler.OnFrameChanged -= _method;
    }

    public void SaveRecBeforeExit()
    {
        StopRecording();
        WriteToFile();
    }

    private void OnApplicationQuit()
    {
        if(recHandler.recState == RecordingState.Recording)
        {
            SaveRecBeforeExit();
        }        
    }
}
