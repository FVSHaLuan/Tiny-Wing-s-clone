using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using FH.Core.Architecture.Helper;

namespace FH.Core.Architecture.ParametersTable
{
    [Serializable]
    public class SerializableParametersTable : IParametersTableListableKeys
    {
        #region Serializable fields
        List<string> intKeys;
        List<string> floatKeys;
        List<string> boolKeys;
        List<string> stringKeys;

        List<int> intValues;
        List<float> floatValues;
        List<bool> boolValues;
        List<string> stringValues;
        #endregion

        Dictionary<string, int> intDictionary;
        Dictionary<string, float> floatDictionary;
        Dictionary<string, bool> boolDictionary;
        Dictionary<string, string> stringDictionary;

        #region Serialization
        [OnDeserializing]
        void SetValuesBeforeDeserialization(StreamingContext context)
        {

        }
        [OnDeserialized]
        void SetValuesAfterDeserialization(StreamingContext context)
        {
            InitialDictionaries();

            if (intKeys != null)
            {
                DictionaryListConverter.ListToDictionary(intKeys, intValues, intDictionary);
            }

            if (floatKeys != null)
            {
                DictionaryListConverter.ListToDictionary(floatKeys, floatValues, floatDictionary);
            }

            if (boolKeys != null)
            {
                DictionaryListConverter.ListToDictionary(boolKeys, boolValues, boolDictionary);
            }

            if (stringKeys != null)
            {
                DictionaryListConverter.ListToDictionary(stringKeys, stringValues, stringDictionary);
            }

            FreeLists();
        }




        [OnSerializing]
        void SetValuesBeforeSerialization(StreamingContext context)
        {
            InitialLists();
            DictionaryListConverter.DictionaryToList(intKeys, intValues, intDictionary);
            DictionaryListConverter.DictionaryToList(floatKeys, floatValues, floatDictionary);
            DictionaryListConverter.DictionaryToList(boolKeys, boolValues, boolDictionary);
            DictionaryListConverter.DictionaryToList(stringKeys, stringValues, stringDictionary);
        }
        [OnSerialized]
        void SetValuesAfterSerialization(StreamingContext context)
        {
            FreeLists();
        }


        #endregion

        #region IParametersTable
        bool IParametersTable.GetBool(string key, bool defaultValue)
        {
            bool value;
            if (boolDictionary.TryGetValue(key, out value))
            {
                return value;
            }
            else
            {
                return defaultValue;
            }
        }

        float IParametersTable.GetFloat(string key, float defaultValue)
        {
            float value;
            if (floatDictionary.TryGetValue(key, out value))
            {
                return value;
            }
            else
            {
                return defaultValue;
            }
        }

        int IParametersTable.GetInt(string key, int defaultValue)
        {
            int value;
            if (intDictionary.TryGetValue(key, out value))
            {
                return value;
            }
            else
            {
                return defaultValue;
            }
        }

        string IParametersTable.GetString(string key, string defaultValue)
        {
            string value;
            if (stringDictionary.TryGetValue(key, out value))
            {
                return value;
            }
            else
            {
                return defaultValue;
            }
        }

        #endregion

        #region IParametersTableListableKeys
        string[] IParametersTableListableKeys.GetIntKeys()
        {
            List<string> keys = new List<string>();
            foreach (var item in intDictionary.Keys)
            {
                keys.Add(item);
            }
            return keys.ToArray();
        }

        string[] IParametersTableListableKeys.GetFloatKeys()
        {
            List<string> keys = new List<string>();
            foreach (var item in floatDictionary.Keys)
            {
                keys.Add(item);
            }
            return keys.ToArray();
        }

        string[] IParametersTableListableKeys.GetBoolKeys()
        {
            List<string> keys = new List<string>();
            foreach (var item in boolDictionary.Keys)
            {
                keys.Add(item);
            }
            return keys.ToArray();
        }

        string[] IParametersTableListableKeys.GetStringKeys()
        {
            List<string> keys = new List<string>();
            foreach (var item in stringDictionary.Keys)
            {
                keys.Add(item);
            }
            return keys.ToArray();
        }
        #endregion

        public SerializableParametersTable(IParametersTableListableKeys parametersTable)
        {
            InitialDictionaries();
            FillDictionaryByOtherTable(parametersTable, intDictionary);
            FillDictionaryByOtherTable(parametersTable, floatDictionary);
            FillDictionaryByOtherTable(parametersTable, boolDictionary);
            FillDictionaryByOtherTable(parametersTable, stringDictionary);
        }

        public SerializableParametersTable()
        {
            InitialDictionaries();
        }

        #region Private methods
        void InitialDictionaries()
        {
            intDictionary = new Dictionary<string, int>();
            floatDictionary = new Dictionary<string, float>();
            boolDictionary = new Dictionary<string, bool>();
            stringDictionary = new Dictionary<string, string>();
        }

        void InitialLists()
        {
            intKeys = new List<string>();
            floatKeys = new List<string>();
            boolKeys = new List<string>();
            stringKeys = new List<string>();

            intValues = new List<int>();
            floatValues = new List<float>();
            boolValues = new List<bool>();
            stringValues = new List<string>();
        }

        void FreeLists()
        {
            intKeys = null;
            floatKeys = null;
            boolKeys = null;
            stringKeys = null;

            intValues = null;
            floatValues = null;
            boolValues = null;
            stringValues = null;
        }

        void FillDictionaryByOtherTable<T>(IParametersTableListableKeys parametersTable, Dictionary<string, T> dictionary)
        {
            dictionary.Clear();
            var keys = parametersTable.GetKeysOfType<T>();
            for (int i = 0; i < keys.Length; i++)
            {
                string key = keys[i];
                dictionary[key] = parametersTable.GetValueOfType<T>(key, default(T));
            }
        }


        #endregion
    }

}