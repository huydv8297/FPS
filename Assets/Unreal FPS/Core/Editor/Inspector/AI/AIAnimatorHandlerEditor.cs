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
using UnrealFPS.AI;

namespace UnrealFPS.Editor
{
	[CustomEditor(typeof(AIAnimatorHandler))]
	[CanEditMultipleObjects]
	public class AIAnimatorHandlerEditor : UEditor
	{
		/// <summary>
		/// Custom Inspector GUI
		/// </summary>
		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			BeginBackground();
			Title("AI Animator Handler");
			BeginBox();
			base.OnInspectorGUI();
			EndBox();
			EndBackground();
			serializedObject.ApplyModifiedProperties();
		}
	}
}