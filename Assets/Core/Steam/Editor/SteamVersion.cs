#if UNITY_EDITOR
using System;
using System.Text;
using UnityEditor;
using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace Cf
{
    public class SteamVersion : EditorWindow
    {
        private static readonly string[] RequirePackageNameArr = new string[]
        {
            "com.unity.netcode.gameobjects",
            "com.coverfrog.steamworks"
        };

        /// <summary>
        /// Get Window
        /// </summary>
        [MenuItem("Cf/Steam/Package")]
        private static void GetWindow()
        {
            var window = GetWindow<SteamVersion>();
        }

        /// <summary>
        /// Get Package Info
        /// </summary>
        /// <param name="packageName"> package name </param>
        /// <param name="packageInfo"> package info </param>
        private static bool TryFindForPackageName(string packageName, out PackageInfo packageInfo)
        { 
            packageInfo = PackageInfo.FindForPackageName(packageName);

            return packageInfo != null;
        }

        /// <summary>
        /// Package Status To Gui From Names
        /// </summary>
        /// <param name="packageName"> saved name </param>
        private static void PackageStatusGui(string packageName)
        {
            // create data
            string message = "Status : ";
            string activeStr;
            Color addColor;
            GUIStyle style = new GUIStyle(GUI.skin.label)
            {
                richText = true
            };

            // try get status active
            bool isExist = TryFindForPackageName(packageName, out var packageInfo);
            if (isExist)
            {
                activeStr = "Active   ";
                addColor = Color.green;
            }
            else
            {
                activeStr = "InActive";
                addColor = Color.red;
            }

            message += $"<color=#{ColorUtility.ToHtmlStringRGB(addColor)}>{activeStr}</color> ";
            
            // package name
            message += $"Name : {packageName} ";
            
            // version
            if (isExist)
            {
                message += $" {packageInfo.version}";
            }

            // set
            GUILayout.Label(message, style);
        }

        private void OnGUI()
        {
            // packages on gui
            GUILayout.Label("[ Package Actives Check ]");
            
            foreach (var packageName in RequirePackageNameArr)
            {
                PackageStatusGui(packageName);
            }
        }
    }
}

#endif