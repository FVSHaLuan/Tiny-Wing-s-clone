using UnityEngine;
using System.Collections;
using System;

namespace FH.Core.Architecture.ParametersTable
{
    public static class ParametersTableExtension
    {
        #region IParametersTable
        public static T GetValueOfType<T>(this IParametersTable parametersTable, string key, T defaultValue)
        {
            var t = typeof(T);

            if (t == typeof(int))
            {
                return (T)GetValueOfType(parametersTable, key, PrimitiveValueType.Integer, defaultValue);
            }
            if (t == typeof(float))
            {
                return (T)GetValueOfType(parametersTable, key, PrimitiveValueType.Float, defaultValue);
            }
            if (t == typeof(bool))
            {
                return (T)GetValueOfType(parametersTable, key, PrimitiveValueType.Bool, defaultValue);
            }
            if (t == typeof(string))
            {
                return (T)GetValueOfType(parametersTable, key, PrimitiveValueType.String, defaultValue);
            }

            throw new NotSupportedException();
        }
        public static object GetValueOfType(this IParametersTable parametersTable, string key, PrimitiveValueType primeValueType, object defaultValue)
        {
            switch (primeValueType)
            {
                case PrimitiveValueType.Integer:
                    return parametersTable.GetInt(key, (int)defaultValue);
                case PrimitiveValueType.Float:
                    return parametersTable.GetFloat(key, (float)defaultValue);
                case PrimitiveValueType.Bool:
                    return parametersTable.GetBool(key, (bool)defaultValue);
                case PrimitiveValueType.String:
                    return parametersTable.GetString(key, (string)defaultValue);
                default:
                    throw new NotSupportedException();
            }
        }

        #endregion

        #region IParametersTableListableKeys
        public static string[] GetKeysOfType<T>(this IParametersTableListableKeys parametersTable)
        {
            var t = typeof(T);

            if (t == typeof(int))
            {
                return GetKeysOfType(parametersTable, PrimitiveValueType.Integer);
            }
            if (t == typeof(float))
            {
                return GetKeysOfType(parametersTable, PrimitiveValueType.Float);
            }
            if (t == typeof(bool))
            {
                return GetKeysOfType(parametersTable, PrimitiveValueType.Bool);
            }
            if (t == typeof(string))
            {
                return GetKeysOfType(parametersTable, PrimitiveValueType.String);
            }

            throw new NotSupportedException();
        }

        public static string[] GetKeysOfType(this IParametersTableListableKeys parametersTable, PrimitiveValueType primitiveValueType)
        {
            switch (primitiveValueType)
            {
                case PrimitiveValueType.Integer:
                    return parametersTable.GetIntKeys();
                case PrimitiveValueType.Float:
                    return parametersTable.GetFloatKeys();
                case PrimitiveValueType.Bool:
                    return parametersTable.GetBoolKeys();
                case PrimitiveValueType.String:
                    return parametersTable.GetStringKeys();
                default:
                    throw new NotSupportedException();
            }
        }
        #endregion

        #region IParametersTableSetable
        public static void CopyFromOtherTable(this IParametersTableSetable parameterTable, IParametersTableListableKeys otherTable)
        {
            // Bool
            var boolKeys = otherTable.GetBoolKeys();
            for (int i = 0; i < boolKeys.Length; i++)
            {
                string key = boolKeys[i];
                parameterTable.SetBool(key, otherTable.GetBool(key, default(bool)));
            }

            // int
            var intKeys = otherTable.GetIntKeys();
            for (int i = 0; i < intKeys.Length; i++)
            {
                string key = intKeys[i];
                parameterTable.SetInt(key, otherTable.GetInt(key, default(int)));
            }

            // float
            var floatKeys = otherTable.GetFloatKeys();
            for (int i = 0; i < floatKeys.Length; i++)
            {
                string key = floatKeys[i];
                parameterTable.SetFloat(key, otherTable.GetFloat(key, default(float)));
            }

            // string
            var stringKeys = otherTable.GetStringKeys();
            for (int i = 0; i < stringKeys.Length; i++)
            {
                string key = stringKeys[i];
                parameterTable.SetString(key, otherTable.GetString(key, default(string)));
            }
        }
        #endregion
    }

}