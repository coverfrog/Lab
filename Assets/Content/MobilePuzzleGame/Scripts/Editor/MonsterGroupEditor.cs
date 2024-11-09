#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;

namespace Pu
{
    [CustomEditor(typeof(MonsterGroup))]
    public class MonsterGroupEditor : Editor
    {
        private MonsterGroup _monsterGroup;
        
        private void OnEnable()
        {
            _monsterGroup = target as MonsterGroup;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(20);
            
            if (GUILayout.Button("Find All"))
            {
                _monsterGroup.SpawnDataListUpdate(this, null);
            }
        }
    }
}

#endif