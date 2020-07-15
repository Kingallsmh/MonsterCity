using DataRecording;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using WalkEMons;

namespace WalkEMons
{
    public class DataManagement
    {
        public static string dataLocation = Application.persistentDataPath;
        public static string extension = "sav";
        public static string filename = "WALKE";

        public static void SavePlayer(PlayerFile playerFile)
        {
            DataFileIO<PlayerFile>.extension = extension;
            DataFileIO<PlayerFile>.fileName = filename;
            DataFileIO<PlayerFile>.fileLocation = dataLocation;
            DataFileIO<PlayerFile>.WriteData(playerFile);
            DataFileIO<PlayerFile>.WriteData(playerFile.ToString());
        }

        public static PlayerFile LoadPlayer()
        {
            DataFileIO<PlayerFile>.extension = extension;
            DataFileIO<PlayerFile>.fileName = filename;
            DataFileIO<PlayerFile>.fileLocation = dataLocation;
            return DataFileIO<PlayerFile>.LoadData();
        }

        public static bool FileExists()
        {
            if (!Directory.Exists(dataLocation))
            {
                return false;
            }
            //Create the whole path with filename and extension
            string file = Path.Combine(dataLocation, filename + "." + extension);
            if (!File.Exists(file))
            {
                //Debug.LogError("File already exists!");
                return false;
            }
            return true;
        }
    }
}

