using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

[Serializable]
public class VWorldCursorPointConst
{
    protected const double InitLatitude = 37.5202991;
    protected const double InitLongitude = 127.1214261;
}

[Serializable]
public class VWorldCursorPoint : VWorldCursorPointConst
{
    public double latitude = InitLatitude;
    public double longitude = InitLongitude;
}

public class VWorldCursor : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler, IPointerUpHandler, IScrollHandler
{
    [SerializeField] private VWorldCursorPoint mCenterPoint = new VWorldCursorPoint();
    [SerializeField] private VWorldCursorPoint mLeftBottomPoint = new VWorldCursorPoint();
    [SerializeField] private VWorldCursorPoint mRightTopPoint = new VWorldCursorPoint();

    public VWorldCursorPoint GetCenterPoint() => mCenterPoint;
    
    public event Action OnZoomInAction; 
    public event Action OnZoomOutAction;

    private void Awake()
    {
        mCenterPoint = new VWorldCursorPoint();
    }

    #region :: Point Update

    public void OnCursorUpdate(VWorldMapSetting setting)
    {
        var zoomLevel = setting.mapZoomLevel;
        
        var pixelToLongitudeRatio = 360.0f / (Mathf.Pow(2, zoomLevel) * 256.0f);
        var pixelToLatitudeRatio = 180.0f / (Mathf.Pow(2, zoomLevel) * 256.0f);

        var halfWidthInPixels = setting.mapWidth / 2.0f;
        var halfHeightInPixels = setting.mapHeight / 2.0f;

        var spanLongitude = halfWidthInPixels * pixelToLongitudeRatio;
        var spanLatitude = halfHeightInPixels * pixelToLatitudeRatio;

        mLeftBottomPoint.longitude = mCenterPoint.longitude - spanLongitude;
        mLeftBottomPoint.latitude = mCenterPoint.latitude - spanLatitude;

        mRightTopPoint.longitude = mCenterPoint.longitude + spanLongitude;
        mRightTopPoint.latitude = mCenterPoint.latitude + spanLatitude;
    }

    #endregion

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

    #region :: Move

    public void OnPointerMove(PointerEventData eventData)
    {
 
    }

    #endregion

    #region :: Left

    private void OnDownLeft(PointerEventData eventData)
    {

    }
    
    private void OnUpLeft(PointerEventData eventData)
    {

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
