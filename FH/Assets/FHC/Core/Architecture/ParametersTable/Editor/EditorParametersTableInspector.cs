using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using FH.DevTool;
using FH.DevTool.LevelDesign;

namespace FH.Core.Architecture.ParametersTable
{
    [CustomEditor(typeof(EditorParametersTable)), CanEditMultipleObjects]
    public class EditorParametersTableInspector : Editor
    {
        List<EditorParametersTable> editorParametersTables;
        List<ParameterSettersGroup> parameterSettersGroups;

        void OnEnable()
        {
            BuildTablesList();
            BuildParameterSettersGroups();
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical();

            if (parameterSettersGroups.Count > 0)
            {
                for (int i = 0; i < parameterSettersGroups.Count; i++)
                {
                    EditorGUI.BeginChangeCheck();

                    parameterSettersGroups[i].DrawField();

                    if (EditorGUI.EndChangeCheck())
                    {
                        SetDirtyAll();
                        DispatchChangeEventAll();
                    }
                }
            }
            else
            {
                DrawInfo();
            }
            
            EditorGUILayout.EndVertical();
        }

        void DrawInfo()
        {
            EditorGUILayout.HelpBox("Switch to debug mode to add fields", MessageType.Info, true);
        }
        
        #region Group operation
        void BuildTablesList()
        {
            editorParametersTables = new List<EditorParametersTable>();
            for (int i = 0; i < targets.Length; i++)
            {
                editorParametersTables.Add(targets[i] as EditorParametersTable);
            }
        }

        void BuildParameterSettersGroups()
        {
            parameterSettersGroups = new List<ParameterSettersGroup>();
            var originalSetters = editorParametersTables[0].ParameterSetters;

            // Initialize
            for (int i = 0; i < originalSetters.Count; i++)
            {
                ParameterSettersGroup parameterSettersGroup = new ParameterSettersGroup();
                parameterSettersGroup.TryAdd(originalSetters[i]);
                parameterSettersGroups.Add(parameterSettersGroup);
            }

            // Add other table
            for (int i = 1; i < editorParametersTables.Count; i++)
            {
                AddSettersFromTable(editorParametersTables[i]);
            }
        }

        void AddSettersFromTable(EditorParametersTable editorParametersTable)
        {
            var setters = editorParametersTable.ParameterSetters;
            for (int i = 0; i < setters.Count; i++)
            {
                if (i < parameterSettersGroups.Count)
                {
                    parameterSettersGroups[i].TryAdd(setters[i]);
                }
            }
        }

        void SetDirtyAll()
        {
            for (int i = 0; i < editorParametersTables.Count; i++)
            {
                EditorUtility.SetDirty(editorParametersTables[i]);
            }
        }

        void DispatchChangeEventAll()
        {
            for (int i = 0; i < editorParametersTables.Count; i++)
            {
                editorParametersTables[i].DispatchChangeEvent();
            }
        }
        #endregion

    }
}