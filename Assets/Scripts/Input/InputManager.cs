using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    public const string InputActionAssetResourcesPath = "InputSystem_Actions";

    [SerializeField] private InputData inputData = new InputData();
    
    private PlayerInput _mPlayerInput;
    private Dictionary<string, InputAction> _mInputActionDict;
    private Dictionary<InputEventName, InputAction> _mInputEventNameDict;
    
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
        SetInputEventAction(ref _mInputEventNameDict);
        
        _mPlayerInput.actions = inputActionAsset;
    }

    private void GetComponents()
    {
        if (!_mPlayerInput) _mPlayerInput = gameObject.GetComponent<PlayerInput>();
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
                    
                }
            }
        }

        if (inputActionDict.Count > 0)
        {
            return;
        }

        inputActionDict.Clear();
        inputActionDict.Add("Error", null);
    }

    private static void GetInputEventNameDict(ref Dictionary<string, InputAction> inputActionDict, out Dictionary<InputEventName, InputAction> inputEventNameDict)
    {
        inputEventNameDict = new Dictionary<InputEventName, InputAction>();

        int missingCount = 0;
        
        foreach (KeyValuePair<string, InputAction> pair in inputActionDict)
        {
            if (Enum.TryParse(pair.Key, false, out InputEventName eventName))
            {
                inputEventNameDict.TryAdd(eventName, pair.Value);
                
                continue;
            }

            ++missingCount;
        }

        Debug.Assert( missingCount <= 0 ,$"missing count : {missingCount}");
    }

    private void SetInputEventAction(ref Dictionary<InputEventName, InputAction> inputEventNameDict)
    {
        if (inputEventNameDict.TryGetValue(InputEventName.Click, out InputAction leftClickInputAction))
        {
            leftClickInputAction.performed += context => inputData.isMouseLeftClick = context.ReadValue<float>() > 0;
        }

        if (inputEventNameDict.TryGetValue(InputEventName.RightClick, out InputAction rightClickInputAction))
        {
            rightClickInputAction.performed += context => inputData.isMouseRightClick = context.ReadValue<float>() > 0;
        }
    }

    private void SetReBind(InputEventName inputEventName)
    {
        if (_mInputEventNameDict.TryGetValue(inputEventName, out InputAction inputAction))
        {
            
        }
    }
}
