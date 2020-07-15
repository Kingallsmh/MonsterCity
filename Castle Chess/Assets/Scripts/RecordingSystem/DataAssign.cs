using DataRecording;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace DataRecording
{
    /// <summary>
    /// Helper class for simplifying the DataStorage class and keeping type safety.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataAssign<T>
    {
        public DataStorage storage;

        public DataAssign(string _varName)
        {
            storage = new DataStorage(_varName);
        }

        public bool SetData(T _value)
        {
            if (storage.ValueDifferentFromPreviousData(_value))
            {
                storage.SetObject(_value);
                return true;
            }
            else
            {
                storage.SetObject(_value);
                return false;
            }
        }

        public T GetFromStorage()
        {
            return (T)storage.GetData();
        }

        public string GetWritableFromVariable()
        {
            StringBuilder build = new StringBuilder();
            build.Append(storage.GetVariableName());
            build.Append(" - ").Append(typeof(T)).Append(" ").Append((T)storage.GetData()).AppendLine();
            return build.ToString();
        }
    }
}

