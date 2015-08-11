using UnityEngine;
using System.Collections;
using UnityEditor;
[CustomPropertyDrawer (typeof(JoyStickInput))]
public class AttackInputDrawer : PropertyDrawer  {
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		SerializedProperty direction = property.FindPropertyRelative ("direction");
		SerializedProperty accuracy = property.FindPropertyRelative ("accuracy");
		EditorGUI.BeginProperty (position, label, property);
		Rect contentPosition = EditorGUI.PrefixLabel (position, label);
		int indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		float fullWidth = contentPosition.width;

		contentPosition.width = fullWidth * 0.2f;
		float x = contentPosition.x;

		contentPosition.x = x;
		EditorGUIUtility.labelWidth = 20f;
		EditorGUI.PropertyField (contentPosition, direction, new GUIContent ("Dir"));

		x += contentPosition.width;
		contentPosition.width = fullWidth * .4f;
		contentPosition.x = x;
		EditorGUIUtility.labelWidth = 50f;
		EditorGUI.PropertyField (contentPosition, accuracy);

		EditorGUI.indentLevel = indent;
		EditorGUI.EndProperty ();
	}

}
