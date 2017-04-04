using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace FH.DevTool
{
    public static class FileNameUtil
    {
        public static string GetFileName(string filePath)
        {
            var splitted = filePath.Split('/', '\\');
            return splitted[splitted.Length - 1];
        }

        public static string GetParentFolder(string filePath)
        {
            List<string> splitted = new List<string>(filePath.Split('/', '\\'));
            splitted.RemoveAt(splitted.Count - 1);
            return string.Join("/", splitted.ToArray());
        }

        public static List<string> GetAssetsFilePathFromFolder(string folderPath, string searchPattern)
        {
            List<string> filePaths = new List<string>();

            foreach (var filePath in Directory.GetFiles(folderPath, searchPattern))
            {
                var fileAssetPath = filePath.Replace(Application.dataPath, "").Replace('\\', '/');
                filePaths.Add(fileAssetPath);
            }

            return filePaths;
        }
    }

}