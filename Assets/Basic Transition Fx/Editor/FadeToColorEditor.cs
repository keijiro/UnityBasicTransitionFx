using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(FadeToColor))]
public class FadeToColorEditor : Editor
{
	SerializedProperty propColor;
	SerializedProperty propFadeIn;
	SerializedProperty propDuration;

	void OnEnable ()
	{
		propColor = serializedObject.FindProperty ("color");
		propFadeIn = serializedObject.FindProperty ("fadeIn");
		propDuration = serializedObject.FindProperty ("duration");
	}

	public override void OnInspectorGUI ()
	{
		serializedObject.Update ();

		EditorGUILayout.PropertyField (propColor);
		EditorGUILayout.PropertyField (propFadeIn);
        EditorGUILayout.Slider (propDuration, 0.01f, 5.0f);

        if (EditorApplication.isPlaying)
        {
            var component = (target as FadeToColor);
            EditorGUILayout.BeginHorizontal ();

            if (GUILayout.Button ("Fade Out"))
                component.BeginFadeOut ();

            if (GUILayout.Button ("Fade In"))
                component.BeginFadeIn ();

            if (GUILayout.Button ("White Out"))
            {
                component.color = Color.white;
                component.BeginFadeOut ();
            }

            if (GUILayout.Button ("Black Out"))
            {
                component.color = Color.black;
                component.BeginFadeOut ();
            }

            EditorGUILayout.EndHorizontal ();
        }

        serializedObject.ApplyModifiedProperties ();
	}
}
