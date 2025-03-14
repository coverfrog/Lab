using System;
using System.Collections.Generic;
using Cf.Pattern;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InputManager : Singleton<InputManager>
{
    public const string InputActionAssetResourcesPath = "InputSystem_Actions";

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
        if (inputEventNameDict.TryGetValue(InputEventName.Click, out InputAction iaMouseLeftClick))
        {
            iaMouseLeftClick.performed += context => mInputData.isMouseLeftClick = context.ReadValue<float>() > 0;
            iaMouseLeftClick.canceled += context => mInputData.isMouseLeftClick = context.ReadValue<float>() > 0;
        }

        if (inputEventNameDict.TryGetValue(InputEventName.RightClick, out InputAction iaMouseRightClick))
        {
            iaMouseRightClick.performed += context => mInputData.isMouseRightClick = context.ReadValue<float>() > 0;
            iaMouseRightClick.canceled += context => mInputData.isMouseRightClick = context.ReadValue<float>() > 0;
        }
        
        //
        
        if (inputEventNameDict.TryGetValue(InputEventName.A, out InputAction iaA))
        {
            iaA.performed += context => mInputData.isA = context.ReadValue<float>() > 0;
            iaA.canceled += context => mInputData.isA = context.ReadValue<float>() > 0;
        }
        
        if (inputEventNameDict.TryGetValue(InputEventName.S, out InputAction iaS))
        {
            iaS.performed += context => mInputData.isS = context.ReadValue<float>() > 0;
            iaS.canceled += context => mInputData.isS = context.ReadValue<float>() > 0;
        }
        
        if (inputEventNameDict.TryGetValue(InputEventName.D, out InputAction iaD))
        {
            iaD.performed += context => mInputData.isD = context.ReadValue<float>() > 0;
            iaD.canceled += context => mInputData.isD = context.ReadValue<float>() > 0;
        }
        
        if (inputEventNameDict.TryGetValue(InputEventName.F, out InputAction iaF))
        {
            iaF.performed += context => mInputData.isF = context.ReadValue<float>() > 0;
            iaF.canceled += context => mInputData.isF = context.ReadValue<float>() > 0;
        }
        
        //
        
        if (inputEventNameDict.TryGetValue(InputEventName.Q, out InputAction iaQ))
        {
            iaQ.performed += context => mInputData.isQ = context.ReadValue<float>() > 0;
            iaQ.canceled += context => mInputData.isQ = context.ReadValue<float>() > 0;
        }
        
        if (inputEventNameDict.TryGetValue(InputEventName.W, out InputAction iaW))
        {
            iaW.performed += context => mInputData.isW = context.ReadValue<float>() > 0;
            iaW.canceled += context => mInputData.isW = context.ReadValue<float>() > 0;
        }
        
        if (inputEventNameDict.TryGetValue(InputEventName.E, out InputAction iaE))
        {
            iaE.performed += context => mInputData.isE = context.ReadValue<float>() > 0;
            iaE.canceled += context => mInputData.isE = context.ReadValue<float>() > 0;
        }
        
        if (inputEventNameDict.TryGetValue(InputEventName.R, out InputAction iaR))
        {
            iaR.performed += context => mInputData.isR = context.ReadValue<float>() > 0;
            iaR.canceled += context => mInputData.isR = context.ReadValue<float>() > 0;
        }
        
    }
}
