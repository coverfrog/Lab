using System;
using UnityEngine;
using TMPro;

namespace Hate
{
    public class GameView : MonoBehaviour
    {
        public void OnDataLoadChanged(bool inIsLoad)
        {
            if (inIsLoad)
            {
                
            }

            else
            {
#if UNITY_EDITOR
                Debug.Log("Data Load Failed");
#endif
            }
        }
    }
}
