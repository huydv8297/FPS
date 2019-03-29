/* ================================================================
   ---------------------------------------------------
   Project   :    Unreal FPS
   Publisher :    Infinite Dawn
   Author    :    Tamerlan Favilevich
   ---------------------------------------------------
   Copyright © Tamerlan Favilevich 2017 - 2018 All rights reserved.
   ================================================================ */

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnrealFPS.Editor
{
    /// <summary>
    /// Custom editor for Weapon Animation System
    /// </summary>
    [CustomEditor(typeof(WeaponAnimationSystem))]
    [CanEditMultipleObjects]
    public class WeaponAnimationSystemEditor : UEditor
    {
        /// <summary>
        /// Custom Inspector GUI
        /// </summary>
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            BeginBackground();
            Title("Weapon Animation System");
            BeginBox();
            base.OnInspectorGUI();
            EndBox();
            EndBackground();
            serializedObject.ApplyModifiedProperties();
        }
    }
}