using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Coord))]
class CoordDrawer: PropertyDrawer {

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label){
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        Rect xPosition = new Rect(position.x, position.y, position.width / 2 - 5, position.height);
        Rect yPosition = new Rect(position.x + position.width / 2, position.y, position.width / 2, position.height);
        EditorGUI.PropertyField(xPosition, property.FindPropertyRelative("x"), GUIContent.none);
        EditorGUI.PropertyField(yPosition, property.FindPropertyRelative("y"), GUIContent.none);

        EditorGUI.EndProperty();
    }

}
