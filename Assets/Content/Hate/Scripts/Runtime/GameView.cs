using System;
using UnityEngine;
using TMPro;

namespace Hate
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private UICtrl mUiCtrl;

        public UICtrl UICtrl => mUiCtrl;
    }
}
