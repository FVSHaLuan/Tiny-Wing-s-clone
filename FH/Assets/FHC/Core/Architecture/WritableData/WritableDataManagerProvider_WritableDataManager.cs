using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace FH.Core.Architecture.WritableData
{
    public static partial class WritableDataManagerProvider
    {
        private class WritableDataManager : IWritableDataManager
        {
            const string KeysListKey = "DatakeyList";

            List<string> keys;

            #region IWritableDataManager
            public bool ContainsKey(string key)
            {
                return keys.Contains(key);
            }

            public T LoadData<T>(string key)
            {
                Assert.IsTrue(keys.Contains(key));

                byte[] bytesData = PersistentFileHelper.LoadFile(GetFileName(key));
                return BinarySerializationHelper.Deserialize<T>(bytesData);
            }

            public void SaveData<T>(string key, T data)
            {
                byte[] bytesData = BinarySerializationHelper.Serialize(data);
                PersistentFileHelper.SaveFile(bytesData, GetFileName(key));

                if (!keys.Contains(key))
                {
                    keys.Add(key);
                    SavekeysList();
                }
            }

            public void DeleteData(string key)
            {
                keys.Remove(key);
                SavekeysList();
            }

            public void DeleteAllData()
            {
                keys = new List<string>();
                SavekeysList();
            }
            #endregion

            public WritableDataManager()
            {
                LoadKeysList();
            }

            void LoadKeysList()
            {
                string fileName = GetFileName(KeysListKey);
                if (PersistentFileHelper.FileExist(fileName))
                {
                    byte[] keysData = PersistentFileHelper.LoadFile(fileName);
                    keys = BinarySerializationHelper.Deserialize<List<string>>(keysData);
                }
                else
                {
                    keys = new List<string>();
                }
            }

            void SavekeysList()
            {
                byte[] keysData = BinarySerializationHelper.Serialize(keys);
                PersistentFileHelper.SaveFile(keysData, GetFileName(KeysListKey));
            }

            string GetFileName(string key)
            {
                return string.Format("WritableData_{0}.FH", key);
            }
        }
    }
}