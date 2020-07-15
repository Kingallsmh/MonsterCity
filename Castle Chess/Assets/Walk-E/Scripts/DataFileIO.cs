using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace DataRecording
{    public class DataFileIO<T>
    {
        public static string fileName;
        public static string extension;
        public static string fileLocation;

        public static void WriteData(T sceneContainer)
        {
            Debug.Log("Path to file: " + fileLocation);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            //Make sure Directory exists
            if (!Directory.Exists(fileLocation))
            {
                Directory.CreateDirectory(fileLocation);
            }                
            //Create the whole path with filename and extension
            string file = Path.Combine(fileLocation, fileName + "." + extension);
            if (File.Exists(file))
            {
                //Debug.LogError("File already exists!");
                File.Delete(file);
            }
            Debug.Log(sceneContainer);
            //Write the file
            using (FileStream fileStream = File.Open(file, FileMode.CreateNew))
            {
                binaryFormatter.Serialize(fileStream, sceneContainer);
            }
        }

        public static void WriteData(string sceneContainer)
        {
            Debug.Log("Path to file: " + fileLocation);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            //Make sure Directory exists
            if (!Directory.Exists(fileLocation))
            {
                Directory.CreateDirectory(fileLocation);
            }
            //Create the whole path with filename and extension
            string file = Path.Combine(fileLocation, fileName + "." + extension + "read");
            if (File.Exists(file))
            {
                //Debug.LogError("File already exists!");
                File.Delete(file);
            }
            //Write the file
            File.WriteAllText(file, sceneContainer);
        }

        public static T LoadData()
        {
            if (!Directory.Exists(fileLocation))
            {
                return default;
            }
            //Create the whole path with filename and extension
            string file = Path.Combine(fileLocation, fileName + "." + extension);
            if (!File.Exists(file))
            {
                //Debug.LogError("File already exists!");
                return default;
            }

            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using (FileStream fileStream = File.Open(file, FileMode.Open))
            {
                return (T)binaryFormatter.Deserialize(fileStream);
            }
        }
    }
}

