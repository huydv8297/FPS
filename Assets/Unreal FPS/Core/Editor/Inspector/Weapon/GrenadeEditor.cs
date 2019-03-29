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
    [CustomEditor(typeof(Grenade))]
    [CanEditMultipleObjects]
    public class GrenadeEditor : UEditor
    {
        /// <summary>
        /// Custom Inspector GUI
        /// </summary>
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            BeginBackground();
            Title("Grenade");
            BeginBox();
            base.OnInspectorGUI();
            EndBox();
            EndBackground();
            serializedObject.ApplyModifiedProperties();
        }
    }
}