#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Cf
{
    [CustomPropertyDrawer(typeof(ValueDropField<>))]
    public class ValueDropFieldPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty t = property.FindPropertyRelative("t");

            if (t == null)
            {
                EditorGUI.LabelField(position, "Property Is Null");
            }

            else
            {
                Type objType = property.serializedObject.targetObject.GetType();
                FieldInfo field = null;

                string[] propertyPath = property.propertyPath.Split('.');
                
                foreach (string path in propertyPath)
                {
                    field = objType.GetField(path, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

                    if (field == null)
                    {
                        break;
                    }

                    objType = field.FieldType;
                }

                if (field != null)
                {
                    
                }
            }

            EditorGUI.EndProperty();
        }
    }
}

#endif