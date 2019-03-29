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
    /// Custom editor for Weapon Reload System
    /// </summary>
    [CustomEditor(typeof(WeaponReloadSystem))]
    [CanEditMultipleObjects]
    public class WeaponReloadSystemEditor : UEditor
    {
        private WeaponReloadSystem instance;
        private SerializedProperty e_BulletCount;
        private SerializedProperty e_ClipCount;
        private SerializedProperty e_MaxBulletCount;
        private SerializedProperty e_MaxClipCount;
        private SerializedProperty e_ReloadTime;
        private SerializedProperty e_EmptyReloadTime;
        private SerializedProperty e_StartTime;
        private SerializedProperty e_IterationTime;

        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        protected virtual void OnEnable()
        {
            instance = (WeaponReloadSystem) target;
            e_BulletCount = serializedObject.FindProperty("bulletCount");
            e_ClipCount = serializedObject.FindProperty("clipCount");
            e_MaxBulletCount = serializedObject.FindProperty("maxBulletCount");
            e_MaxClipCount = serializedObject.FindProperty("maxClipCount");
            e_ReloadTime = serializedObject.FindProperty("reloadTime");
            e_EmptyReloadTime = serializedObject.FindProperty("emptyReloadTime");
            e_StartTime = serializedObject.FindProperty("startTime");
            e_IterationTime = serializedObject.FindProperty("iterationTime");
        }

        /// <summary>
        /// Custom Inspector GUI
        /// </summary>
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            BeginBackground();
            Title("Weapon Reload System");
            BeginBox();
            instance.W_ReloadType = (ReloadType) EditorGUILayout.EnumPopup("Reload Type", instance.W_ReloadType);
            EditorGUILayout.PropertyField(e_BulletCount, new GUIContent("Bullet Count"));
            EditorGUILayout.PropertyField(e_ClipCount, new GUIContent("Clip Count"));
            EditorGUILayout.PropertyField(e_MaxBulletCount, new GUIContent("Max Bullet Count"));
            EditorGUILayout.PropertyField(e_MaxClipCount, new GUIContent("Max Clip Count"));
            switch (instance.W_ReloadType)
            {
                case ReloadType.Default:
                    EditorGUILayout.PropertyField(e_ReloadTime, new GUIContent("Reload Time"));
                    EditorGUILayout.PropertyField(e_EmptyReloadTime, new GUIContent("Empty Reload Time"));
                    break;
                case ReloadType.Sequential:
                    EditorGUILayout.PropertyField(e_StartTime, new GUIContent("Start Reload Time"));
                    EditorGUILayout.PropertyField(e_IterationTime, new GUIContent("Iteration Reload Time"));
                    break;

            }
            EndBox();
            EndBackground();
            serializedObject.ApplyModifiedProperties();
        }
    }
}