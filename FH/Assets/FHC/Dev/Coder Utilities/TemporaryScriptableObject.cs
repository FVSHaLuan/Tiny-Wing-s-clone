using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FH.DevTool
{
    public static class TemporaryScriptableObject
    {
        public static T GetTemporaryScriptableObject<T>(string assetName) where T : ScriptableObject
        {
#if UNITY_EDITOR
            string path = string.Format("{0}/{1}/{2}.asset", DevToolConfiguration.ResourcesPath, "TemporaryScriptableObjects", assetName);

            T so = AssetDatabase.LoadAssetAtPath<T>(path);

            if (so == null)
            {
                so = ScriptableObject.CreateInstance<T>();
                AssetDatabase.CreateAsset(so, path);
            }

            return so;
#else
            return null;
#endif
        }

    }

}