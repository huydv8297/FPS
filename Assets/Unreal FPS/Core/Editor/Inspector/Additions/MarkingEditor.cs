/* ================================================================
   ---------------------------------------------------
   Project   :    Unreal FPS
   Publisher :    Infinite Dawn
   Author    :    Tamerlan Favilevich
   ---------------------------------------------------
   Copyright © Tamerlan Favilevich 2017 - 2018 All rights reserved.
   ================================================================ */

using UnityEditor;
using UnityEngine;

namespace UnrealFPS.Editor
{
	[CustomEditor (typeof (Marking))]
	public class MarkingEditor : UEditor
	{
		private SerializedProperty e_MarkItem;

		private void OnEnable ()
		{
			e_MarkItem = serializedObject.FindProperty ("markItem");
		}

		public override void OnInspectorGUI ()
		{
			serializedObject.Update();
			BeginBackground();
			Title("Marking Object");
			BeginBox();
			GUILayout.Space(3);
			EditorGUILayout.PropertyField(e_MarkItem, new GUIContent("Mark Item"));
			GUILayout.Space(3);
			EndBox();
			EndBackground();
			serializedObject.ApplyModifiedProperties();
		}
	}
}