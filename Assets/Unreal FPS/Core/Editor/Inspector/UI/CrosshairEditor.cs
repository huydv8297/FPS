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
using UnrealFPS.UI;

namespace UnrealFPS.Editor
{
	[CustomEditor(typeof(Crosshair))]
	[CanEditMultipleObjects]
	public class CrosshairEditor : UEditor
	{
		/// <summary>
		/// Custom Inspector GUI
		/// </summary>
		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			BeginBackground();
			Title("Crosshair");
			BeginBox();
			base.OnInspectorGUI();
			EndBox();
			EndBackground();
			serializedObject.ApplyModifiedProperties();
		}
	}
}