using UnityEditor;
using System.Collections;

namespace FH.DevTool
{
    public class SaveAsset
    {
        [MenuItem("FH/Save all assets")]
        public static void SaveAllAssets()
        {
            AssetDatabase.SaveAssets();
        }
    }

}