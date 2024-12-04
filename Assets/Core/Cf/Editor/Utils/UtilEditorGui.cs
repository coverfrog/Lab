#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Cf
{
    public static class UtilEditorGui 
    {
        public static void OpenFolder()
        {
            string selectedPath = EditorUtility.OpenFilePanel("Select Folder", "", "");
        }
    }
}

#endif