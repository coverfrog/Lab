#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Cf.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = System.Diagnostics.Debug;

[CustomEditor(typeof(InputManager))]
public class InputManagerEditor : Editor
{
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern bool SetForegroundWindow(IntPtr hWnd);
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();

        GUIStyle headStyle = new GUIStyle(GUI.skin.label)
        {
            fontStyle = FontStyle.Bold,
        };
        
        GUIStyle btnStyle = new GUIStyle(GUI.skin.button)
        {
            alignment = TextAnchor.MiddleLeft,
            fixedHeight = 26,
        };
        
        EditorGUILayout.LabelField("Editor", headStyle);
        
        if (GUILayout.Button("Open Enum Txt", btnStyle))
        {
            OpenEnumTxt();
        }

        if (GUILayout.Button("Open Data Txt", btnStyle))
        {
            OpenDataTxt();
        }
    }

    private static bool GetInputActionAsset(out InputActionAsset inputActionAsset)
    {
        inputActionAsset = Resources.Load<InputActionAsset>(InputManager.InputActionAssetResourcesPath);
        
        return inputActionAsset;
    }

    private static void OpenEnumTxt()
    {
        if (!GetInputActionAsset(out InputActionAsset inputActionAsset))
        {
            return;
        }

        InputManager.GetInputActionDict(ref inputActionAsset, out Dictionary<string, InputAction> inputActionDict);

        const string fileName = "InputEventName";
        const string structName = "InputEventName";

        string enumNames = "";
        
        foreach (string key in inputActionDict.Keys)
        {
            string enumName = CfUtil.String.ToPascal(key);

            enumNames += 
                "\t" + enumName + "," + "\n";
        }
        
        string enumString =
            $"public enum {structName}" + "\n" +
            $"{{"                       + "\n" +
            $"{enumNames}"              +             
            $"}}";

        OpenNotepad(fileName, enumString);
    }

    private static void OpenDataTxt()
    {
        if (!GetInputActionAsset(out InputActionAsset inputActionAsset))
        {
            return;
        }
        
        InputManager.GetInputActionDict(ref inputActionAsset, out Dictionary<string, InputAction> inputActionDict);
        
        const string fileName = "InputData";
        const string className = "InputData";
        
        string memberNames = "";
        
        foreach (var pair in inputActionDict)
        {
            string[] frontStrArr = pair.Value.expectedControlType.ToLower() switch
            {
                "button" => new string[2]{ "bool", "is"},
                _ => null,
            };

            if (frontStrArr == null)
            {
                continue;
            }

            string frontStr = $"public {frontStrArr[0]} {frontStrArr[1]}";
            string memberName = $"{frontStr}{CfUtil.String.ToPascal(pair.Key)}";

            memberNames += 
                "\t" + memberName + ";" + "\n";
        }
        
        string classString =
            $"using System;"            + "\n" +
            $"using UnityEngine;"       + "\n" +
            $""                         + "\n" +
            $"[Serializable]"           + "\n" +
            $"public class {className}" + "\n" +
            $"{{"                       + "\n" +
            $"{memberNames}"            +             
            $"}}";

        OpenNotepad(fileName, classString);
    }

    private static void OpenNotepad(string fileName, string data, int openTimeOut = 10)
    {
        string persistentPath = Path.Combine(Application.persistentDataPath, "Cf");

        if (!Directory.Exists(persistentPath))
        {
            Directory.CreateDirectory(persistentPath);
        }

        string path = Path.Combine(Application.persistentDataPath, $"{fileName}.txt");

        if (File.Exists(path))
        {
            File.Delete(path);
        }
        
        File.WriteAllText(path, data);

        _ = Process.Start("notepad.exe", $"{path}");
    }
}

#endif