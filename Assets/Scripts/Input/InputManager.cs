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
    
    private void Awake()
    {
        InputActionAsset inputActionAsset = Resources.Load<InputActionAsset>(InputActionAssetResourcesPath);

        if (!inputActionAsset)
        {
            Debug.LogError("Load Fail");
            return;
        }

        if (!_mPlayerInput)
        {
            _mPlayerInput = gameObject.GetComponent<PlayerInput>();
        }

        _mPlayerInput.actions = inputActionAsset;

        GetInputActionDict(ref inputActionAsset, out _mInputActionDict);
    }

    public static void GetInputActionDict(ref InputActionAsset inputActionAsset, out Dictionary<string, InputAction> inputActionDict)
    {
        inputActionDict = new Dictionary<string, InputAction>();
        
        foreach (InputActionMap inputActionMap in inputActionAsset.actionMaps)
        {
            foreach (InputAction inputAction in inputActionMap.actions)
            {
                if (inputActionDict.TryAdd(inputAction.name, inputAction))
                {
                    continue;   
                }
            }
        }

        if (inputActionDict.Count > 0)
        {
            return;
        }
        
        inputActionDict.Add("Error", null);
    }
}
