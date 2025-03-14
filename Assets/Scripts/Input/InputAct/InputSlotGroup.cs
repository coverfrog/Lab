using System;
using System.Collections;
using System.Collections.Generic;
using Cf.Yield;
using UnityEngine;

public class InputSlotGroup : MonoBehaviour
{
    private static readonly InputEventName[] SlotNames = new[]
    {
        InputEventName.A,
        InputEventName.S,
        InputEventName.D,
        InputEventName.F,
        
        InputEventName.Q,
        InputEventName.W,
        InputEventName.E,
        InputEventName.R,
    };

    private static readonly bool[] SlotDowns = new bool[8];
    private static readonly bool[] SlotPrevDowns = new bool[8];

    [SerializeField] private InputSkillRequest mSkillRequest;
    
    private InputManager _mInputManager;

    private void Awake()
    {
        if (!mSkillRequest) mSkillRequest = gameObject.GetComponent<InputSkillRequest>();
        if (!mSkillRequest) mSkillRequest = gameObject.AddComponent<InputSkillRequest>();
    }

    private IEnumerator Start()
    {
        while (!InputManager.Instance)
        {
            yield return YieldCache.WaitForEndOfFrame;
        }
        
        _mInputManager = InputManager.Instance;
    }

    private void Update()
    {
        if (!_mInputManager)
        {
            return;
        }
        
        if (_mInputManager.Data == null)
        {
            return;
        }

        if (SlotDowns == null || SlotPrevDowns == null)
        {
            return;
        }

        if (!mSkillRequest)
        {
            return;
        }

        for (int slotIdx = 0; slotIdx < SlotDowns.Length; ++slotIdx)
        {
            if (SlotPrevDowns[slotIdx] == SlotDowns[slotIdx])
            {
                continue;
            }

            mSkillRequest.Request(slotIdx);

            SlotPrevDowns[slotIdx] = SlotDowns[slotIdx];
        }
    }
}
