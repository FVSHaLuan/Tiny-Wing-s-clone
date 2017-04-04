using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Assertions;

namespace FH.Core.Architecture.WritableData
{
    [ExecuteInEditMode]
    public class WritableScriptableObject<T> : ScriptableObject, IWritableData<T>, IWritableScriptableObjectHelper, IInspectorCommandObject where T : new()
    {
        [SerializeField, ReadOnly]
        string key;

        [Space]
        [SerializeField]
        protected T currentData;
        [SerializeField, InspectorCommand()]
        int saveCurrentData;

        [Space]
        [SerializeField]
        protected T defaultData = new T();

        [NonSerialized]
        bool loadedData = false;

        #region IWritableData<T>
        public T Data
        {
            get
            {
                if (!loadedData)
                {
                    LoadData();
                    loadedData = true;
                }

                return currentData;
            }

            set
            {
#if UNITY_EDITOR
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
#endif
                currentData = value;
            }
        }
        public void SaveData()
        {
            WritableDataManagerProvider.GetManager().SaveData(key, currentData);
        }
        #endregion

        void LoadData()
        {
            IWritableDataManager manager = WritableDataManagerProvider.GetManager();
            if (manager.ContainsKey(key))
            {
                currentData = manager.LoadData<T>(key);
            }
            else
            {
                currentData = BinarySerializationHelper.Clone(defaultData);
            }

            OnDataLoaded();
        }

        string GetAutoKey()
        {
#if UNITY_EDITOR
            return UnityEditor.AssetDatabase.AssetPathToGUID(UnityEditor.AssetDatabase.GetAssetPath(this));
#else
            throw new System.NotImplementedException();
#endif
        }

        [ContextMenu("SetAutoKey")]
        protected void SetAutoKey()
        {
#if UNITY_EDITOR
            key = GetAutoKey();
#endif
        }

        protected void CopyDefaultDataToCurrentData()
        {
            currentData = BinarySerializationHelper.Clone(defaultData);
        }

        protected virtual void OnDataLoaded() { }

        #region MonoB

        public virtual void OnValidate()
        {
            SetAutoKey();
            key = key.Trim();
            Assert.IsFalse(string.IsNullOrEmpty(key));
        }

        public void Reset()
        {
            SetAutoKey();
        }
        #endregion

        #region Context menu

        [ContextMenu("LoadCurrentData")]
        public void Editor_LoadCurrentData()
        {
            LoadData();
            FHLog.Log("Loaded from " + key);
        }

        [ContextMenu("SaveCurrentData")]
        protected void Editor_SaveCurrentData()
        {
            SaveData();
            FHLog.Log("Saved to " + key);
        }

        [ContextMenu("CopyDefaultDataToCurrentData")]
        protected void Editor_CopyDefaultDataToCurrentData()
        {
            currentData = BinarySerializationHelper.Clone(defaultData);
        }

        #endregion

        #region IInspectorCommandObject
        void IInspectorCommandObject.ExcuteCommand(int intPara, string stringPara)
        {
            Editor_SaveCurrentData();
        }
        #endregion


    }

}