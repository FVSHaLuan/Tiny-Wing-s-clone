using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

namespace FH.DevTool.LevelDesign
{
    public class PrefabTool
    {
        #region Menu items
        [MenuItem("GameObject/FH/Select Disconnected prefabs", false, 0)]
        public static void SelectDisconnectedPrefab()
        {
            List<GameObject> disconnectedObjects = new List<GameObject>();
            Transform rootTransform = Selection.transforms[0];
            Transform[] allTransform = rootTransform.GetComponentsInChildren<Transform>();
            EditorUtility.DisplayProgressBar("SelectDisconnectedPrefab", "Wait...", 0);
            for (int i = 0; i < allTransform.Length; i++)
            {
                EditorUtility.DisplayProgressBar("Selecting disconnected prefabs", string.Format("{0}/{1}", i, allTransform.Length), (float)i / (float)allTransform.Length);

                GameObject gameObject = allTransform[i].gameObject;
                if (IsRootPrefabInstance(gameObject))
                {
                    if (PrefabUtility.GetPrefabType(gameObject) == PrefabType.DisconnectedPrefabInstance)
                    {
                        disconnectedObjects.Add(gameObject);
                    }
                }
            }

            EditorUtility.ClearProgressBar();
            EditorUtility.ClearProgressBar();
            EditorUtility.DisplayDialog("SelectDisconnectedPrefab", "Done", "OK");
            Selection.objects = disconnectedObjects.ToArray();
        }

        [MenuItem("FH/Prefab/Disconnect from prefabs", false, 0)]
        public static void DisconnectFromPrefab()
        {
            EditorUtility.DisplayProgressBar("DisconnectFromPrefab", "Wait...", 0);
            for (int i = 0; i < Selection.transforms.Length; i++)
            {
                GameObject gameObject = Selection.transforms[i].gameObject;
                PrefabUtility.DisconnectPrefabInstance(gameObject);
            }
            EditorUtility.ClearProgressBar();
            EditorUtility.DisplayDialog("DisconnectFromPrefab", "Done", "OK");
        }

        [MenuItem("FH/Prefab/Reconnect to prefabs", false, 0)]
        public static void ReconnectToPrefab()
        {
            EditorUtility.DisplayProgressBar("ReconnectToPrefab", "Wait...", 0);
            for (int i = 0; i < Selection.transforms.Length; i++)
            {
                GameObject gameObject = Selection.transforms[i].gameObject;
                PrefabUtility.ReconnectToLastPrefab(gameObject);
            }
            EditorUtility.ClearProgressBar();
            EditorUtility.DisplayDialog("ReconnectToPrefab", "Done", "OK");
        }

        [MenuItem("FH/Prefab/Revert prefabs", false, 0)]
        public static void RevertPrefab()
        {
            EditorUtility.DisplayProgressBar("RevertPrefab", "Wait...", 0);

            for (int i = 0; i < Selection.transforms.Length; i++)
            {
                GameObject gameObject = Selection.transforms[i].gameObject;
                PrefabUtility.ReconnectToLastPrefab(gameObject);
                PrefabUtility.RevertPrefabInstance(gameObject);
            }

            EditorUtility.ClearProgressBar();
            EditorUtility.DisplayDialog("RevertPrefab", "Done", "OK");
        }

        [MenuItem("FH/Prefab/Revert prefabs keeping scales", false, 0)]
        public static void RevertPrefabKeepingScale()
        {
            EditorUtility.DisplayProgressBar("RevertPrefabKeepingScale", "Wait...", 0);

            for (int i = 0; i < Selection.transforms.Length; i++)
            {
                GameObject gameObject = PrefabUtility.FindRootGameObjectWithSameParentPrefab(Selection.transforms[i].gameObject);
                Vector3 savedLocalScale = gameObject.transform.localScale;
                PrefabUtility.ReconnectToLastPrefab(gameObject);
                PrefabUtility.RevertPrefabInstance(gameObject);
                gameObject.transform.localScale = savedLocalScale;
            }

            EditorUtility.ClearProgressBar();
            EditorUtility.DisplayDialog("RevertPrefabKeepingScale", "Done", "OK");
        }

        #endregion

        static bool IsRootPrefabInstance(GameObject gameObject)
        {
            return PrefabUtility.FindRootGameObjectWithSameParentPrefab(gameObject) == gameObject;
        }
    }

}