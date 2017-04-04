using UnityEngine;
using System.Collections;
using UnityEditor;

namespace FH.DevTool.SceneViewUtility
{
    [CustomEditor(typeof(LocalTransformPinner))]
    public class LocalTransformPinnerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            LocalTransformPinner t = target as LocalTransformPinner;

            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField(string.Format("Position: {0} x: {1} y: {2} z: {3}", t.position, t.pinX, t.pinY, t.pinZ));
            EditorGUILayout.LabelField(string.Format("Rotation: {0} x: {1} y: {2} z: {3}", t.rotation, t.pinRotationX, t.pinRotationY, t.pinRotationZ));
            EditorGUILayout.LabelField(string.Format("Scale: {0} x: {1} y: {2} z: {3}", t.scale, t.pinScaleX, t.pinScaleY, t.pinScaleZ));
            EditorGUILayout.EndVertical();
        }

    }

}