using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace FH.DevTool
{
    public static class IssueAlert
    {

        public static void Alert(int repeatTimes, string consoleMesage)
        {
#if UNITY_EDITOR
            FHLog.LogError(consoleMesage);
            for (int i = 0; i < repeatTimes; i++)
            {
                EditorUtility.DisplayDialog("Có gì đó không ổn", "Có vấn đề với dữ liệu dự án. Gọi dev để giải quyết sự cố", "OK");
            }
#endif
        }
    }

}