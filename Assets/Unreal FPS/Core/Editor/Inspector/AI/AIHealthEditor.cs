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
    [CustomEditor(typeof(AIHealth))]
    [CanEditMultipleObjects]
    public class AIHealthEditor : UEditor
    {
        private SerializedProperty e_Health;
        private SerializedProperty e_MaxHealth;
        private bool foldout;

        private void OnEnable()
        {
            e_Health = serializedObject.FindProperty("health");
            e_MaxHealth = serializedObject.FindProperty("maxHealth");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            BeginBackground();
            Title("AI Health");
            BeginBox();
            EditorGUILayout.PropertyField(e_Health, new GUIContent("Health"));
            EditorGUILayout.PropertyField(e_MaxHealth, new GUIContent("Max Health"));
            EndBox();
            EndBackground();
            serializedObject.ApplyModifiedProperties();
        }
    }
}