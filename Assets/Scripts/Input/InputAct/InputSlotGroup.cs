using System;
using System.Collections;
using System.Collections.Generic;
using Cf.Yield;
using UnityEngine;

public class InputSlotGroup : MonoBehaviour
{
    private static readonly bool[] SlotDowns = new bool[8];
    private static readonly bool[] SlotPrevDowns = new bool[8];

    public event Action<int> OnSlotInput;
    
    private InputManager _mInputManager;

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

        SlotDowns[0] = _mInputManager.Data.isA;
        SlotDowns[1] = _mInputManager.Data.isS;
        SlotDowns[2] = _mInputManager.Data.isD;
        SlotDowns[3] = _mInputManager.Data.isF;
        
        SlotDowns[4] = _mInputManager.Data.isQ;
        SlotDowns[5] = _mInputManager.Data.isW;
        SlotDowns[6] = _mInputManager.Data.isE;
        SlotDowns[7] = _mInputManager.Data.isR;
        
        for (int slotIdx = 0; slotIdx < SlotDowns.Length; ++slotIdx)
        {
            if (SlotPrevDowns[slotIdx] == SlotDowns[slotIdx])
            {
                continue;
            }

            OnSlotInput?.Invoke(slotIdx);

            SlotPrevDowns[slotIdx] = SlotDowns[slotIdx];
        }
    }
}
