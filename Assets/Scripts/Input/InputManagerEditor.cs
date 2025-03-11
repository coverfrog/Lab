#if UNITY_EDITOR
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Cf.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

[CustomEditor(typeof(InputManager))]
public class InputManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();
        
        EditorGUILayout.LabelField("Editor");
        
        if (GUILayout.Button("Open Enum Txt"))
        {
            OpenEnumTxt();
        }
    }

    private void OpenEnumTxt()
    {
        InputActionAsset inputActionAsset = Resources.Load<InputActionAsset>(InputManager.InputActionAssetResourcesPath);
        
        if (!inputActionAsset)
        {
            return;
        }

        InputManager.GetInputActionDict(ref inputActionAsset, out Dictionary<string, InputAction> inputActionDict);

        const string fileName = "Cf Input Event Name";
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

        string path = Path.Combine(Application.persistentDataPath, $"{fileName}.txt");

        if (File.Exists(path))
        {
            File.Delete(path);
        }
        
        File.WriteAllText(path, enumString);

        Process.Start("notepad.exe", path);
    }
}

#endif