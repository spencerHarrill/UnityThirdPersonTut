using UnityEngine;
using System.Collections;
using UnityEditor;
[CustomPropertyDrawer (typeof(Combo))]
public class ComboDrawer : PropertyDrawer {
	/// <summary>
	/// Make Sure what you're Drawering does not extend MonoDevelop.
	/// </summary>
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		SerializedProperty comboInput = property.FindPropertyRelative ("comboInput");
		//SerializedProperty accuracy = property.FindPropertyRelative ("accuracy");
		EditorGUI.BeginProperty (position, label, property);
		Rect contentPosition = EditorGUI.PrefixLabel (position, label);
		int indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;
		
		float fullWidth = contentPosition.width;
		// Number here changes Input box size
		contentPosition.width = fullWidth * 0.38f;
		float x = contentPosition.x;
		
		contentPosition.x = x;
		//Changes name width
		EditorGUIUtility.labelWidth = 50f;
		EditorGUI.PropertyField (contentPosition, comboInput, new GUIContent ("Combo"));
		
		x += contentPosition.width;
// adds another serialized property
//		contentPosition.width = fullWidth * .4f;
//		contentPosition.x = x;
//		EditorGUIUtility.labelWidth = 50f;
//		EditorGUI.PropertyField (contentPosition, accuracy);
//		
		EditorGUI.indentLevel = indent;
		EditorGUI.EndProperty ();
	}
}
