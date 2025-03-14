using System;
using System.Collections.Generic;
using Cf.Pattern;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InputManager : Singleton<InputManager>
{
    public const string InputActionAssetResourcesPath = "InputSystem_Actions";

    [Header("Data")]
    [SerializeField] private InputData mInputData = new InputData();
    
    private PlayerInput _mPlayerInput;
    private Dictionary<string, InputAction> _mInputActionDict;
    private Dictionary<InputEventName, InputAction> _mInputEventNameDict;

    public InputData Data => mInputData;
    
    protected override void Awake()
    {
        base.Awake();
        
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
        if (!_mPlayerInput) _mPlayerInput = gameObject.AddComponent<PlayerInput>();
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
        Type inputDataType = typeof(InputData);

        _ = SetInputEventActionFloatToBool(ref inputEventNameDict, ref inputDataType, InputEventName.Click);
        _ = SetInputEventActionFloatToBool(ref inputEventNameDict, ref inputDataType, InputEventName.RightClick);

        _ = SetInputEventActionFloatToBool(ref inputEventNameDict, ref inputDataType, InputEventName.A);
        _ = SetInputEventActionFloatToBool(ref inputEventNameDict, ref inputDataType, InputEventName.S);
        _ = SetInputEventActionFloatToBool(ref inputEventNameDict, ref inputDataType, InputEventName.D);
        _ = SetInputEventActionFloatToBool(ref inputEventNameDict, ref inputDataType, InputEventName.F);

        _ = SetInputEventActionFloatToBool(ref inputEventNameDict, ref inputDataType, InputEventName.Q);
        _ = SetInputEventActionFloatToBool(ref inputEventNameDict, ref inputDataType, InputEventName.W);
        _ = SetInputEventActionFloatToBool(ref inputEventNameDict, ref inputDataType, InputEventName.E);
        _ = SetInputEventActionFloatToBool(ref inputEventNameDict, ref inputDataType, InputEventName.R);
    }

    private bool SetInputEventActionFloatToBool(ref Dictionary<InputEventName, InputAction> inputEventNameDict, ref Type inputDataType, InputEventName inputEventName)
    {
        if (!inputEventNameDict.TryGetValue(inputEventName, out InputAction inputAction))
        {
            return false;
        }
        
        var field = inputDataType.GetField($"is{inputEventName}");

        if (field == null)
        {
            return false;
        }

        inputAction.performed += context =>
        {
            bool b = context.ReadValue<float>() > 0;
            
            field.SetValue(mInputData, b);
        };
        
        inputAction.canceled += context =>
        {
            bool b = context.ReadValue<float>() > 0;
            
            field.SetValue(mInputData, b);
        };

        return true;
    }
}
