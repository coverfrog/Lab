using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pu
{
    public class Game : MonoBehaviour
    {
        [Title("Info")]
        [SerializeField] [ReadOnly] [InlineEditor(InlineEditorModes.FullEditor)] private GameInfo selectedInfo;
        [SerializeField] private List<GameInfo> infoList;

        private IEnumerator Start()
        {
            selectedInfo = infoList[0].Clone();
            
            yield return null;
        }
    }
}
