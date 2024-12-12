#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace ParkGolf.Editors
{
    [CustomEditor(typeof(Score))]
    public class ScoreEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            SetNameByScore();
        }

        private void SetNameByScore()
        {
            GUILayout.Space(20);
            
            if (!GUILayout.Button("Set Name By Score", GUILayout.Height(30))) return;

            var score = target as Score;
            
            if (!score) return;
        }
    }
}

#endif