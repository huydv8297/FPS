/* ================================================================
   ---------------------------------------------------
   Project   :    Unreal FPS
   Publisher :    Infinite Dawn
   Author    :    Tamerlan Favilevich
   ---------------------------------------------------
   Copyright © Tamerlan Favilevich 2017 - 2018 All rights reserved.
   ================================================================ */

using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace UnrealFPS.Editor
{
    [CustomEditor(typeof(PlayerHealth), true)]
    [CanEditMultipleObjects]
    public class PlayerHealthEditor : UEditor
    {

        private SerializedProperty e_PlayerHealth;
        private SerializedProperty e_MaxPlayerHealth;
        private SerializedProperty e_StartPlayerHealth;
        private ReorderableList e_FallDamages;
        private SerializedProperty e_KillCam;
        private SerializedProperty e_OnDeadEvent;
        private SerializedProperty e_DamageImage;
        private SerializedProperty e_UseRegeniration;
        private SerializedProperty e_RegenerationSettings;
        private bool foldout_Health;
        private bool foldout_OnDeadEvent;
        private bool foldout_FallDamage;
        private int currentID;
        private bool foldout;

        void OnEnable()
        {
            e_PlayerHealth = serializedObject.FindProperty("health");
            e_MaxPlayerHealth = serializedObject.FindProperty("maxHealth");
            e_StartPlayerHealth = serializedObject.FindProperty("startHealth");
            e_KillCam = serializedObject.FindProperty("killCam");
            e_OnDeadEvent = serializedObject.FindProperty("onDeadEvent");
            e_DamageImage = serializedObject.FindProperty("damageImage");
            e_UseRegeniration = serializedObject.FindProperty("useRegeniration");
            e_RegenerationSettings = serializedObject.FindProperty("regenerationSettings");
            e_FallDamages = new ReorderableList(serializedObject, serializedObject.FindProperty("fallDamages"), true, true, true, true)
            {
                drawHeaderCallback = (rect) =>
                    {
                        EditorGUI.LabelField(rect, "Fall Damage");
                    },

                    drawElementCallback = (rect, index, isActive, isFocused) =>
                    {
                        SerializedProperty fallDamage = e_FallDamages.serializedProperty.GetArrayElementAtIndex(index);
                        EditorGUI.LabelField(new Rect(rect.x, rect.y, 70, EditorGUIUtility.singleLineHeight), "Min Height:");
                        fallDamage.FindPropertyRelative("minHeight").floatValue = EditorGUI.FloatField(new Rect(rect.x + 70, rect.y, 30, EditorGUIUtility.singleLineHeight), fallDamage.FindPropertyRelative("minHeight").floatValue);

                        EditorGUI.LabelField(new Rect(rect.x + 105, rect.y, 70, EditorGUIUtility.singleLineHeight), "Max Height:");
                        fallDamage.FindPropertyRelative("maxHeight").floatValue = EditorGUI.FloatField(new Rect(rect.x + 180, rect.y, 30, EditorGUIUtility.singleLineHeight), fallDamage.FindPropertyRelative("maxHeight").floatValue);

                        EditorGUI.LabelField(new Rect(rect.x + 215, rect.y, 70, EditorGUIUtility.singleLineHeight), "Damage:");
                        fallDamage.FindPropertyRelative("damage").intValue = EditorGUI.IntField(new Rect(rect.x + 275, rect.y, 30, EditorGUIUtility.singleLineHeight), fallDamage.FindPropertyRelative("damage").intValue);
                    }
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            BeginBackground();
            Title("Player Health");

            //Foldout Health
            GUILayout.BeginVertical("Button", GUILayout.Height(50));
            EditorGUILayout.Space();
            foldout_Health = EditorGUILayout.Foldout(foldout_Health, new GUIContent("Health"));
            if (foldout_Health)
            {
                GUILayout.BeginVertical(EditorStyles.helpBox);
                GUILayout.Label("Health Parameters", EditorStyles.boldLabel);
                EditorGUILayout.IntSlider(e_PlayerHealth, 0, 1000, new GUIContent("Player Health", "Health player at the moment"));
                EditorGUILayout.IntSlider(e_StartPlayerHealth, 0, 1000, new GUIContent("Start Player Health", "Health player in the start game"));
                EditorGUILayout.IntSlider(e_MaxPlayerHealth, 0, 1000, new GUIContent("Max Player Health", "Maximum value health"));
                EditorGUILayout.PropertyField(e_DamageImage, new GUIContent("Damage Image"));
                EditorGUILayout.PropertyField(e_UseRegeniration, new GUIContent("Use Regeneration"));
                if (e_UseRegeniration.boolValue)
                {
                    EditorGUILayout.PropertyField(e_RegenerationSettings, new GUIContent("Regeneration Settings"), true);
                }
                GUILayout.EndVertical();

                GUILayout.BeginVertical(EditorStyles.helpBox);
                GUILayout.Label("Kill Camera", EditorStyles.boldLabel);
                foldout = EditorGUILayout.Foldout(foldout, new GUIContent("Edit Kill Camera"), true);
                if (foldout)
                {
                    e_KillCam.FindPropertyRelative("killCamera").objectReferenceValue = (Transform) EditorGUILayout.ObjectField("Kill Camera", e_KillCam.FindPropertyRelative("killCamera").objectReferenceValue, typeof(Transform), true);
                    e_KillCam.FindPropertyRelative("lookAt").objectReferenceValue = (Transform) EditorGUILayout.ObjectField("Look At", e_KillCam.FindPropertyRelative("lookAt").objectReferenceValue, typeof(Transform), true);
                    e_KillCam.FindPropertyRelative("playerModel").objectReferenceValue = (GameObject) EditorGUILayout.ObjectField("Full Body", e_KillCam.FindPropertyRelative("playerModel").objectReferenceValue, typeof(GameObject), true);
                    e_KillCam.FindPropertyRelative("_FPSBody").objectReferenceValue = (GameObject) EditorGUILayout.ObjectField("FPS Body", e_KillCam.FindPropertyRelative("_FPSBody").objectReferenceValue, typeof(GameObject), true);
                    e_KillCam.FindPropertyRelative("radius").floatValue = EditorGUILayout.FloatField("Radius", e_KillCam.FindPropertyRelative("radius").floatValue);
                    e_KillCam.FindPropertyRelative("minDistance").floatValue = EditorGUILayout.FloatField("Min Distance", e_KillCam.FindPropertyRelative("minDistance").floatValue);
                }
                GUILayout.EndVertical();

                GUILayout.BeginVertical(EditorStyles.helpBox);
                GUILayout.Label("Fall Damage", EditorStyles.boldLabel);
                foldout_FallDamage = EditorGUILayout.Foldout(foldout_FallDamage, new GUIContent("Edit Fall Damage"), true);
                if (foldout_FallDamage)
                {
                    e_FallDamages.DoLayoutList();
                }
                GUILayout.EndVertical();

                GUILayout.BeginVertical(EditorStyles.helpBox);
                GUILayout.Label("On Dead Event", EditorStyles.boldLabel);
                foldout_OnDeadEvent = EditorGUILayout.Foldout(foldout_OnDeadEvent, new GUIContent("Edit Event"), true);
                if (foldout_OnDeadEvent)
                {
                    EditorGUILayout.PropertyField(e_OnDeadEvent);
                }
                GUILayout.EndVertical();
            }
            if (!foldout_Health) { EditorGUILayout.LabelField("Edit Health"); }
            EditorGUILayout.Space();
            GUILayout.EndVertical();

            EndBackground();
            serializedObject.ApplyModifiedProperties();
        }
    }
}