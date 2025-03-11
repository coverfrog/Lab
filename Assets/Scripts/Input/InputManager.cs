using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    public const string InputActionAssetResourcesPath = "InputSystem_Actions";
    
    private PlayerInput _mPlayerInput;
    private Dictionary<string, InputAction> _mInputActionDict;
    private Dictionary<InputEventName, string> _mInputEventNameDict;
    
    private void Awake()
    {
        InputActionAsset inputActionAsset = Resources.Load<InputActionAsset>(InputActionAssetResourcesPath);

        Debug.Assert(inputActionAsset, "Load Fail");
        if (!inputActionAsset)
        {
            return;
        }

        GetComponents();
        GetInputActionDict(ref inputActionAsset, out _mInputActionDict);
        GetInputEventNameDict(ref _mInputActionDict, out _mInputEventNameDict);
        
        _mPlayerInput.actions = inputActionAsset;
    }

    private void GetComponents()
    {
        if (!_mPlayerInput) _mPlayerInput = gameObject.GetComponent<PlayerInput>();
    }

    public static void GetInputActionDict(ref InputActionAsset inputActionAsset, out Dictionary<string, InputAction> inputActionDict)
    {
        inputActionDict = new Dictionary<string, InputAction>();

        int missingCount = 0;
        
        foreach (InputActionMap inputActionMap in inputActionAsset.actionMaps)
        {
            foreach (InputAction inputAction in inputActionMap.actions)
            {
                if (inputActionDict.TryAdd(inputAction.name, inputAction))
                {
                    continue;   
                }

                ++missingCount;
            }
        }

        Debug.Assert(missingCount <= 0,$"missing count : {missingCount}");

        if (inputActionDict.Count > 0)
        {
            return;
        }

        inputActionDict.Clear();
        inputActionDict.Add("Error", null);
    }

    private void GetInputEventNameDict(ref Dictionary<string, InputAction> inputActionDict, out Dictionary<InputEventName, string> inputEventNameDict)
    {
        inputEventNameDict = new Dictionary<InputEventName, string>();

        int missingCount = 0;
        
        foreach (KeyValuePair<string, InputAction> pair in inputActionDict)
        {
            if (Enum.TryParse(pair.Key, false, out InputEventName eventName))
            {
                inputEventNameDict.TryAdd(eventName, pair.Key);
                
                continue;
            }

            ++missingCount;
        }

        Debug.Assert( missingCount <= 0 ,$"missing count : {missingCount}");
    }
}
