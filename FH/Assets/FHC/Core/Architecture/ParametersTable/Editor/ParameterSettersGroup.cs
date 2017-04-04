using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace FH.Core.Architecture.ParametersTable
{
    class ParameterSettersGroup
    {
        List<ParameterSetter> setters = new List<ParameterSetter>();
        bool haveSameValue = true;

        public bool TryAdd(ParameterSetter setter)
        {
            if (setters.Count == 0)
            {
                setters.Add(setter);
                return true;
            }
            else
            {
                if (ParametersTableHelper.IdenticalSetter(setters[0], setter))
                {
                    Add(setter);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        void Add(ParameterSetter setter)
        {
            if (haveSameValue)
            {
                haveSameValue = ParametersTableHelper.SettersHaveSameValue(setters[0], setter);
            }

            setters.Add(setter);
        }

        void SyncValue()
        {
            if (setters.Count < 2)
            {
                return;
            }

            var orginalSetter = setters[0];

            for (int i = 1; i < setters.Count; i++)
            {
                ParametersTableHelper.CopyValue(orginalSetter, setters[i]);
            }
        }

        #region Draw
        public void DrawField()
        {
            if (setters.Count == 0)
            {
                return;
            }

            EditorGUI.showMixedValue = !haveSameValue;

            EditorGUI.BeginChangeCheck();

            DrawParameter(setters[0], setters.Count > 1 ? string.Format("({0}) ", setters.Count) : "");

            if (EditorGUI.EndChangeCheck())
            {
                SyncValue();
                haveSameValue = true;
            }

            EditorGUI.showMixedValue = false;
        }

        private void DrawParameter(ParameterSetter parameter, string labelPrefix)
        {
            switch (parameter.PrimitiveValueType)
            {
                case PrimitiveValueType.Integer:
                    DrawInt(parameter, labelPrefix);
                    break;
                case PrimitiveValueType.Float:
                    DrawFloat(parameter, labelPrefix);
                    break;
                case PrimitiveValueType.Bool:
                    DrawBool(parameter, labelPrefix);
                    break;
                case PrimitiveValueType.String:
                    DrawString(parameter, labelPrefix);
                    break;
                default:
                    break;
            }
        }

        void DrawInt(ParameterSetter parameter, string labelPrefix)
        {
            var value = parameter.IntValue;
            if (parameter.MaxIntValue == parameter.MinIntValue)
            {
                value = EditorGUILayout.IntField(labelPrefix + parameter.Key, value);
            }
            else
            {
                value = EditorGUILayout.IntSlider(labelPrefix + parameter.Key, value, parameter.MinIntValue, parameter.MaxIntValue);
            }
            parameter.IntValue = value;
        }
        void DrawFloat(ParameterSetter parameter, string labelPrefix)
        {
            var value = parameter.FloatValue;
            if (parameter.MaxFloatValue == parameter.MinFloatValue)
            {
                value = EditorGUILayout.FloatField(labelPrefix + parameter.Key, value);
            }
            else
            {
                value = EditorGUILayout.Slider(labelPrefix + parameter.Key, value, parameter.MinFloatValue, parameter.MaxFloatValue);
            }
            parameter.FloatValue = value;
        }
        void DrawBool(ParameterSetter parameter, string labelPrefix)
        {
            var value = parameter.BoolValue;
            value = EditorGUILayout.Toggle(labelPrefix + parameter.Key, value);
            parameter.BoolValue = value;
        }
        void DrawString(ParameterSetter parameter, string labelPrefix)
        {
            var value = parameter.StringValue;
            value = EditorGUILayout.TextField(labelPrefix + parameter.Key, value);
            parameter.StringValue = value;
        }
        #endregion
    }

}