using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    private PlayerInput _mPlayerInput;
    private Dictionary<string, InputAction> _mInputActionDict;
    
    private void Awake()
    {
        InputActionAsset inputActionAsset = Resources.Load<InputActionAsset>("InputSystem_Actions");

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

    private static void GetInputActionDict(ref InputActionAsset inputActionAsset, out Dictionary<string, InputAction> inputActionDict)
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
                
                Debug.LogError("Add Fail");
            }
        }
    }
    
    private bool GetInputActionDict(out Dictionary<string, InputAction> inputActionDict)
    {
        inputActionDict = null;

        if (!_mPlayerInput)
        {
            return false;
        }

        if (!_mPlayerInput.actions)
        {
            return false;
        }

        inputActionDict = new Dictionary<string, InputAction>();

        InputActionAsset inputActionAsset = _mPlayerInput.actions;
        
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

        return true;
    }
}
