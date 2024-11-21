#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Cf.Scenes.Editor
{
    [CustomPropertyDrawer(typeof(SceneField))]
    public class SceneFieldPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // begin
            EditorGUI.BeginProperty(position, label, property);
            
            // find property
            SerializedProperty sceneAsset = property.FindPropertyRelative("sceneAsset");
            SerializedProperty sceneName = property.FindPropertyRelative("sceneName");
            
            // position from label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            
            // define rect by position
            Rect sceneFieldRect = new Rect(position.x, position.y, position.width - 50, position.height);
            
            // target [ obj ] when null
            if (sceneAsset == null || sceneName == null)
            {
                EditorGUI.EndProperty();
                return;
            }

            // value field
            sceneAsset.objectReferenceValue = EditorGUI.ObjectField(sceneFieldRect, sceneAsset.objectReferenceValue, typeof(SceneAsset), false);
            
            // reference [ obj type ] is not match 
            if (sceneAsset.objectReferenceValue == null)
            {
                EditorGUI.EndProperty();
                return;
            }
            
            // value paste
            sceneName.stringValue = sceneAsset.objectReferenceValue.name;

            // end
            EditorGUI.EndProperty();
        }
    }
}
#endif