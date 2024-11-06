#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Cf.AirPass
{
    [CustomPropertyDrawer(typeof(GameField))]
    public class GameFieldPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // begin
            EditorGUI.BeginProperty(position, label, property);

            // find property
            SerializedProperty component = property.FindPropertyRelative("resources");
            SerializedProperty gameList = property.FindPropertyRelative("gameList");

            // position get
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Keyboard), label);

            // check property is null
            if (component == null || gameList == null)
            {
                EditorGUI.EndProperty();
                return;
            }

            // Set a consistent height increment for each field
            float fieldSpacing = EditorGUIUtility.singleLineHeight + 4;

            // Full Button Object field
            Rect componentRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            component.objectReferenceValue = EditorGUI.ObjectField(componentRect, "> Component", component.objectReferenceValue, typeof(GameResources), true);
            position.y += fieldSpacing;

            // Game List Size field
            Rect sizeRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            gameList.arraySize = EditorGUI.IntSlider(sizeRect, "> Game List Size", gameList.arraySize, 3, 4);
            position.y += fieldSpacing;

            // indent +
            EditorGUI.indentLevel++;

            // Draw game list elements
            for (int i = 0; i < gameList.arraySize; i++)
            {
                SerializedProperty element = gameList.GetArrayElementAtIndex(i);
                Rect elementRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
                string elementLabel = $"  Game Level {i}";
                element.objectReferenceValue = EditorGUI.ObjectField(elementRect, elementLabel, element.objectReferenceValue, typeof(Game), true);
                position.y += fieldSpacing;
            }

            // indent -
            EditorGUI.indentLevel--;

            // end
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // get property
            SerializedProperty gameList = property.FindPropertyRelative("gameList");

            // calc line count
            int lineCount = gameList != null ? gameList.arraySize + 4 : 4;

            // return height
            return lineCount * (EditorGUIUtility.singleLineHeight + 4);
        }
    }
}

#endif