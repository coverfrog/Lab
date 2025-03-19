using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public enum VWorldCursorState
{
    Idle,
    MoveMode,
}

[Serializable]
public class VWorldCursorPointConst
{
    protected const string InitLatitude = "37.5202991";
    protected const string InitLongitude = "127.1214261";
}

[Serializable]
public class VWorldCursorPoint : VWorldCursorPointConst
{
    public string latitude = InitLatitude;
    public string longitude = InitLongitude;
}

public class VWorldCursor : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler, IScrollHandler
{
    [SerializeField] private VWorldCursorPoint mVWorldCursorPoint;

    public VWorldCursorPoint GetPoint() => mVWorldCursorPoint;
    
    public event Action OnZoomInAction; 
    public event Action OnZoomOutAction;

    private void Awake()
    {
        mVWorldCursorPoint = new VWorldCursorPoint();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                OnDownLeft(eventData);
                break;
            case PointerEventData.InputButton.Right:
                OnDownRight(eventData);
                break;
            case PointerEventData.InputButton.Middle:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    public void OnPointerMove(PointerEventData eventData)
    {
 
    }

    private void OnDownLeft(PointerEventData eventData)
    {

    }

    private void OnDownRight(PointerEventData eventData)
    {
        
    }

    #region :: Scroll

    public void OnScroll(PointerEventData eventData)
    {

        if (eventData.scrollDelta.y > 0)
        {
            OnZoomInAction?.Invoke();
        }

        else
        {
            OnZoomOutAction?.Invoke();
        }
    }

    #endregion
}
