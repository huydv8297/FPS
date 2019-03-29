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
using UnrealFPS.AI;
using System.Collections.Generic;

namespace UnrealFPS.Editor
{
    /// <summary>
    /// 
    /// </summary>
    [CustomEditor(typeof(AIBehaviour))]
    [CanEditMultipleObjects]
    public class AIBehaviourEditor : UEditor
    {
        /// <summary>
        /// Custom Inspector GUI
        /// </summary>
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            base.OnInspectorGUI();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
