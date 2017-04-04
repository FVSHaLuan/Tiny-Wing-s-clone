using UnityEngine;
using System.Collections;
using System.IO;
using FH.Core.Architecture.WritableData;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FH.DevTool
{
    public static class TextAssetManipulator
    {
        public static void ChangeBytesData(TextAsset textAsset, byte[] bytes, bool refresh)
        {
#if UNITY_EDITOR
            string path = AssetDatabase.GetAssetPath(textAsset);
            BinaryFileHelper.SaveFile(bytes, path);
            if (refresh)
            {
                AssetDatabase.Refresh();
            }
#else
            throw new System.NotSupportedException();
#endif

        }

        public static void ChangeTextData(TextAsset textAsset, string text, bool refresh)
        {
#if UNITY_EDITOR
            //string path = AssetDatabase.GenerateUniqueAssetPath(AssetDatabase.GetAssetPath(textAsset));
            string path = AssetDatabase.GetAssetPath(textAsset);
            SaveFile(text, path);
            if (refresh)
            {
                AssetDatabase.Refresh();
            }
#else
            throw new System.NotSupportedException();
#endif
        }

        public static TextAsset CreateTextAsset(string text, string path)
        {
#if UNITY_EDITOR
            SaveFile(text, path);
            AssetDatabase.Refresh();
            return AssetDatabase.LoadAssetAtPath<TextAsset>(path);
#else
             throw new System.NotSupportedException();
#endif
        }

        public static TextAsset CreateTextAsset(byte[] bytes, string path)
        {
#if UNITY_EDITOR
            BinaryFileHelper.SaveFile(bytes, path);
            AssetDatabase.Refresh();
            return AssetDatabase.LoadAssetAtPath<TextAsset>(path);
#else
             throw new System.NotSupportedException();
#endif
        }

#if UNITY_EDITOR
        [MenuItem("Assets/Create/TextAsset/Text")]
        static void CreateTextDataTextAsset()
        {
            CreateTextAssetGeneral("txt");
        }

        [MenuItem("Assets/Create/TextAsset/Bytes")]
        static void CreateBytesDataTextAsset()
        {
            CreateTextAssetGeneral("bytes");
        }

#endif

        #region Private        
        static void SaveFile(string text, string path)
        {
            using (StreamWriter streamWrite = File.CreateText(path))
            {
                streamWrite.Write(text);
            }
        }

        static void CreateTextAssetGeneral(string extension)
        {
#if UNITY_EDITOR
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (path == "")
            {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }

            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + extension + "." + extension);
            File.Create(assetPathAndName).Close();
            AssetDatabase.Refresh();
#endif
        }
        #endregion

    }

}