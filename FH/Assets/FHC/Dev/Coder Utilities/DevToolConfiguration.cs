using UnityEngine;
using System.Collections;

namespace FH.DevTool
{
    public static class DevToolConfiguration
    {
        const string resourcesPath = "Assets/FH/Dev/EditorResources";        

        public static string ResourcesPath
        {
            get
            {
                return resourcesPath;
            }
        }
    }

}