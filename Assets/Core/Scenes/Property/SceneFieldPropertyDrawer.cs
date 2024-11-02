using UnityEditor;
using UnityEngine;

namespace Cf.Scenes.Property
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
            
            // target [ obj ] when null
            if (sceneAsset == null)
            {
                EditorGUI.EndProperty();
                return;
            }

            // !!! value is true !!!
            sceneAsset.objectReferenceValue =
                EditorGUI.ObjectField(position, sceneAsset.objectReferenceValue, typeof(SceneAsset), false);
            
            // reference [ obj ] null
            if (sceneAsset.objectReferenceValue == null)
            {
                EditorGUI.EndProperty();
                return;
            }
            
            // !!! value is true !!!
            sceneName.stringValue = sceneAsset.objectReferenceValue.name;

            // end
            EditorGUI.EndProperty();
        }
    }
}