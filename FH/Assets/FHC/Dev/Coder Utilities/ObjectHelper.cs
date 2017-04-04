using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace FH.DevTool
{
    public static class ObjectHelper
    {
        public static void SafeDestroy<T>(T targetObject) where T : Object
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                Object.Destroy(targetObject);
            }
            else
            {
                Object.DestroyImmediate(targetObject);
            }
#endif
        }

        public static void SafeDestroy<T>(this Object currentObject, T targetObject) where T : Object
        {
            SafeDestroy(targetObject);
        }
        public static bool IsChildOrSelf(Transform parentTransform, Transform testTransform)
        {
            if (testTransform == null)
            {
                return false;
            }

            if (parentTransform == testTransform)
            {
                return true;
            }
            else
            {
                return IsChildOrSelf(parentTransform, testTransform.parent);
            }
        }

        public static T[] GetAllObjectsInCurrentActiveScene<T>(bool includeInactive)
        {
            GameObject[] allRootGameObject = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
            List<T> allObjects = new List<T>();
            foreach (var item in allRootGameObject)
            {
                allObjects.AddRange(item.GetComponentsInChildren<T>(includeInactive));
            }
            return allObjects.ToArray();
        }
    }

}