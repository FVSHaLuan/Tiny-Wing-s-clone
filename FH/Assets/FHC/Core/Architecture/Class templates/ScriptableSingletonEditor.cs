using UnityEngine;
using System.Collections;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FH.DevTool
{
    public sealed class ScriptableSingletonEditor : ScriptableObject
    {
        #region Singleton
        const string assetName = "ScriptableSingletonEditor_ASSET";
        static ScriptableSingletonEditor instance = null;
        public static ScriptableSingletonEditor Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = LoadAsset();
                    if (instance == null)
                    {
                        instance = CreateAsset();
                    }

                    (instance as ScriptableSingletonEditor).Initialize();
                }

                return instance;
            }
        }

        private void Initialize()
        {
            throw new NotImplementedException();
        }

        static ScriptableSingletonEditor CreateAsset()
        {
#if UNITY_EDITOR
            ScriptableSingletonEditor scriptableSingletonEditor = CreateInstance<ScriptableSingletonEditor>();
            string assetPath = DevToolConfiguration.ResourcesPath + "/" + assetName + ".asset";
            UnityEditor.AssetDatabase.CreateAsset(scriptableSingletonEditor, assetPath);
            return scriptableSingletonEditor as ScriptableSingletonEditor;
#else
            return null;
#endif
        }

        static ScriptableSingletonEditor LoadAsset()
        {
#if UNITY_EDITOR
            string assetPath = DevToolConfiguration.ResourcesPath + "/" + assetName + ".asset";
            return AssetDatabase.LoadAssetAtPath<ScriptableSingletonEditor>(assetPath) as ScriptableSingletonEditor;
#else
            return null;
#endif
        }        

        #endregion

        #region Editor menu
#if UNITY_EDITOR
        //[UnityEditor.MenuItem("FH/<UNNAMED EDITOR>/<UNNAMED EDITOR>")]
        static void ShowInInspector()
        {
            UnityEditor.Selection.activeObject = Instance as ScriptableSingletonEditor;
        }
#endif
        #endregion

    }

}