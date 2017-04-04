using UnityEngine;
using System.Collections;
using UnityEditor;

namespace FH.DevTool.SceneViewUtility
{
    [CustomEditor(typeof(PositionMark))]
    public class PositionMarkEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField(string.Format("Mark when not selected: {0}", (target as PositionMark).markWhenNotSelected));
            EditorGUILayout.EndVertical();
        }

        void OnSceneGUI()
        {
            PositionMark positionMark = target as PositionMark;
            GUI.contentColor = Color.blue;
            DrawLabel(positionMark);
        }

        static void DrawLabel(PositionMark positionMark)
        {
            string labelContent;
            if (string.IsNullOrEmpty(positionMark.customLabel))
            {
                labelContent = positionMark.name;
            }
            else
            {
                labelContent = positionMark.customLabel;
            }

            Handles.Label(positionMark.transform.position, labelContent, new GUIStyle()
            {
                fontStyle = FontStyle.Bold
            });
        }
    }

}