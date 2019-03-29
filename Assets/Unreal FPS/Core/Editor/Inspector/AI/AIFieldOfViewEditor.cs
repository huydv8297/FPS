/* ================================================================
   ---------------------------------------------------
   Project   :    Unreal FPS
   Publisher :    Infinite Dawn
   Author    :    Tamerlan Favilevich
   ---------------------------------------------------
   Copyright © Tamerlan Favilevich 2017 - 2018 All rights reserved.
   ================================================================ */

using UnrealFPS.AI;
using UnityEditor;
using UnityEngine;

namespace UnrealFPS.Editor
{
    [CustomEditor(typeof(AIFieldOfView))]
    [CanEditMultipleObjects]
    public class AIFieldOfViewEditor : UEditor
    {
        private SerializedProperty e_ViewRadius;
        private SerializedProperty e_ViewAngle;
        private SerializedProperty e_TargetMask;
        private SerializedProperty e_ObstacleMask;


        /// <summary>
        /// 
        /// </summary>
        private void OnEnable()
        {
            e_ViewRadius = serializedObject.FindProperty("viewRadius");
            e_ViewAngle = serializedObject.FindProperty("viewAngle");
            e_TargetMask = serializedObject.FindProperty("targetMask");
            e_ObstacleMask = serializedObject.FindProperty("obstacleMask");
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnSceneGUI()
        {
            AIFieldOfView fovAI = (AIFieldOfView)target;
            Handles.color = Color.white;
            Handles.DrawWireArc(fovAI.transform.position, Vector3.up, Vector3.forward, 360, fovAI.ViewRadius);
            Vector3 viewAngleA = fovAI.DirFromAngle(-fovAI.ViewAngle / 2, false);
            Vector3 viewAngleB = fovAI.DirFromAngle(fovAI.ViewAngle / 2, false);

            Handles.DrawLine(fovAI.transform.position, fovAI.transform.position + viewAngleA * fovAI.ViewRadius);
            Handles.DrawLine(fovAI.transform.position, fovAI.transform.position + viewAngleB * fovAI.ViewRadius);

            Handles.color = Color.red;
            foreach (Transform visibleTarget in fovAI.VisibleTargets)
            {
                Handles.DrawLine(fovAI.transform.position, visibleTarget.position);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            BeginBackground();
            Title("AI Field of View");
            BeginBox();
            e_ViewRadius.floatValue = EditorGUILayout.Slider("View Radius", e_ViewRadius.floatValue, 0, 360);
            e_ViewAngle.floatValue = EditorGUILayout.Slider("View Angle", e_ViewAngle.floatValue, 0, 360);
            EditorGUILayout.PropertyField(e_TargetMask, new GUIContent("Target Mask"), true);
            EditorGUILayout.PropertyField(e_ObstacleMask, new GUIContent("Obstacle Mask"));
            EndBox();
            EndBackground();
            serializedObject.ApplyModifiedProperties();
        }
    }
}