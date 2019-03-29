/* ================================================================
   ---------------------------------------------------
   Project   :    Unreal FPS
   Publisher :    Infinite Dawn
   Author    :    Tamerlan Favilevich
   ---------------------------------------------------
   Copyright © Tamerlan Favilevich 2017 - 2018 All rights reserved.
   ================================================================ */

using System.Text;
using UnityEditor;
using UnityEngine;
using UnrealFPS.Utility;

namespace UnrealFPS.Editor
{
	[CustomEditor(typeof(InputController), true)]
	[CanEditMultipleObjects]
	public class InputControllerEditor : UEditor
	{
		private string title;

		/// <summary>
		/// This function is called when the object becomes enabled and active.
		/// </summary>
		protected virtual void OnEnable()
		{
			InputController instance = target as InputController;
			title = instance.GetType().Name.AddSpaces();
		}

		/// <summary>
		/// Custom Inspector GUI
		/// </summary>
		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			BeginBackground();
			Title(title);
			BeginBox();
			EditorGUILayout.HelpBox("Handling Unreal FPS Input By " + title, MessageType.Info);
			EndBox();
			EndBackground();
			serializedObject.ApplyModifiedProperties();
		}
	}
}