using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

[Serializable]
public class VWorldCursorPointConst
{
    protected const float InitLatitude = 37.5202991f;
    protected const float InitLongitude = 127.1214261f;
}

[Serializable]
public class VWorldCursorPoint : VWorldCursorPointConst
{
    public float longitude = InitLongitude;
    public float latitude = InitLatitude;
}

public class VWorldCursor : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler, IPointerUpHandler, IScrollHandler
{
    [Header("Option")] 
    [SerializeField] private float mMoveUpdateDistance = 100.0f;
    
    [Header("Debug")]
    [SerializeField] private VWorldCursorPoint mCenterPoint = new VWorldCursorPoint();
    
    public event Action OnZoomInAction; 
    public event Action OnZoomOutAction;
    public event Action<Vector2, Vector2> OnMoveAction;

    private bool _mIsLeftClick;
    private Vector2 _mMousePointPrev;
    
    private void Awake()
    {
        mCenterPoint = new VWorldCursorPoint();
    }

    private static string ToKey(VWorldCursorPoint point) => $"{point.longitude}_{point.latitude}";

    public void GetCenterKey(out string key)
    {
        key = ToKey(mCenterPoint);
    }

    public void GetCenterPoint(out VWorldCursorPoint point)
    {
        point = mCenterPoint;
    }

    public void SetCenterPoint(Vector2 newPoint)
    {
        mCenterPoint.latitude = newPoint.x;
        mCenterPoint.longitude = newPoint.y;
    }

    #region :: Switch

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
    
    public void OnPointerUp(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                OnUpLeft(eventData);
                break;
            case PointerEventData.InputButton.Right:
                OnUpRight(eventData);
                break;
            case PointerEventData.InputButton.Middle:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    


    #endregion

    public void OnPointerMove(PointerEventData eventData)
    {
        if (!_mIsLeftClick)
        {
            return;
        }

        if (Vector2.Distance(eventData.position, _mMousePointPrev) < mMoveUpdateDistance)
        {
            return;
        }
        
        OnMoveAction?.Invoke(_mMousePointPrev, eventData.position);

        _mMousePointPrev = eventData.position;
    }
    
    #region :: Left

    private void OnDownLeft(PointerEventData eventData)
    {
        _mIsLeftClick = true;
        _mMousePointPrev = eventData.position;
    }
    
    private void OnUpLeft(PointerEventData eventData)
    {
        _mIsLeftClick = false;
        if (_mMousePointPrev == eventData.position)
        {
            return;
        }

        OnMoveAction?.Invoke(_mMousePointPrev, eventData.position);
    }

    #endregion

    #region :: Right

    private void OnDownRight(PointerEventData eventData)
    {
        
    }

    private void OnUpRight(PointerEventData eventData)
    {
        
    }

    #endregion
    
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
