using System;
using System.Collections.Generic;
using UnityEngine;

public enum InputCursorType
{
    Normal,
    Active,
}

[RequireComponent(typeof(InputCursorHelper))]
public class InputCursorHelper : MonoBehaviour
{
    [Header("Debug")] 
    [SerializeField] private InputCursorType mCursorType;
    
    [Header("Data")] 
    [SerializeField] private Texture2D mNormalTexture2D;
    [SerializeField] private Texture2D mActiveTexture2D;

    private void Start()
    {
        if (SetCursor(InputCursorType.Normal, true))
        {
            
        }
    }
    
    public bool SetCursor(InputCursorType cursorType, bool isHard = false)
    {
        if (!isHard && mCursorType == cursorType)
        {
            return false;
        }

        Texture2D texture2D = cursorType switch
        {
            InputCursorType.Normal => mNormalTexture2D,
            InputCursorType.Active => mActiveTexture2D,
            _ => throw new ArgumentOutOfRangeException(nameof(cursorType), cursorType, null)
        };
        
        if (!texture2D)
        {
            return false;
        }
        
        Cursor.SetCursor(texture2D, Vector2.zero, CursorMode.Auto);

        mCursorType = cursorType;
        
        return true;
    }
}
