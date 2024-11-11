#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;

namespace Cf
{
    [CustomPropertyDrawer(typeof(PoolHelperField<>))]
    public class PoolHelperFieldPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // begin
            EditorGUI.BeginProperty(position, label, property);
            
            // get
            SerializedProperty prefabProperty = property.FindPropertyRelative("prefab");

            // position from label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // check null
            if (prefabProperty == null)
            {
                return;
            }

            // value field 
            // allow false why ? must be use orgin source
            Type fieldType = fieldInfo.FieldType.GenericTypeArguments[0];
            
            prefabProperty.objectReferenceValue = 
                EditorGUI.ObjectField(
                    position, 
                    new GUIContent(fieldType.ToString()),
                    prefabProperty.objectReferenceValue,
                    typeof(PoolHelperField<>),
                    false);
            
            // end
            EditorGUI.EndProperty();
        }
    }
}

#endif