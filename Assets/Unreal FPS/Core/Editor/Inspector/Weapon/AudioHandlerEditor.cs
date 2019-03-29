/* ================================================================
   ---------------------------------------------------
   Project   :    Unreal FPS
   Publisher :    Infinite Dawn
   Author    :    Tamerlan Favilevich
   ---------------------------------------------------
   Copyright © Tamerlan Favilevich 2017 - 2018 All rights reserved.
   ================================================================ */

using UnityEngine;
using UnityEditor;

namespace UnrealFPS.Editor
{
	[CustomEditor(typeof(AudioHandler))]
	public class AudioHandlerEditor : UEditor
	{
		public override void OnInspectorGUI()
		{
			BeginBackground();
			Title("Audio Handler");
			BeginBox();
			EditorGUILayout.HelpBox("Handling animation sounds", MessageType.Info);
			EndBox();
			EndBackground();
		}
	}
}