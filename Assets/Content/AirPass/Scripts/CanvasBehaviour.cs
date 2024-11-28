using System;
using UnityEngine;
using UnityEngine.UI;

namespace AirPass
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasScaler))]
    public class CanvasBehaviour : MonoBehaviour
    {
        private void OnEnable()
        {
            GameManager.OnCanvasBehaviour(this, this, true);
        }

        private void OnDisable()
        {
            GameManager.OnCanvasBehaviour(this, this, false);
        }
    }
}
