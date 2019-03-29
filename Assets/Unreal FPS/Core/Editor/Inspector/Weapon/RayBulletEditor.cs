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
    [CustomEditor(typeof(RayBullet))]
    [CanEditMultipleObjects]
    public class RayBulletEditor : UEditor
    {
        private SerializedProperty e_Model;
        private SerializedProperty e_Caliber;
        private SerializedProperty e_Type;
        private SerializedProperty e_Damage;
        private SerializedProperty e_Variance;
        private SerializedProperty e_Numberbullet;
        private SerializedProperty e_BulletHitEffects;
        private BulletType bulletType;
        private bool folduotHitEffect;

        private void OnEnable()
        {
            e_Model = serializedObject.FindProperty("model");
            e_Caliber = serializedObject.FindProperty("caliber");
            e_Type = serializedObject.FindProperty("type");
            e_Damage = serializedObject.FindProperty("damage");
            e_Variance = serializedObject.FindProperty("variance");
            e_Numberbullet = serializedObject.FindProperty("numberbullet");
            e_BulletHitEffects = serializedObject.FindProperty("bulletHitEffects");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            GUILayout.BeginVertical("HelpBox");
            GUILayout.Space(7);
            GUILayout.Label("Ray Bullet", TitleLabel);
            GUILayout.Space(7);
            bulletType = (BulletType)EditorGUILayout.EnumPopup("Type", bulletType);
            EditorGUILayout.PropertyField(e_Model, new GUIContent("Model"));
            EditorGUILayout.PropertyField(e_Caliber, new GUIContent("Caliber"));
            EditorGUILayout.PropertyField(e_Type, new GUIContent("Type"));
            EditorGUILayout.PropertyField(e_Damage, new GUIContent("Damage"));
            if (bulletType == BulletType.Fractional)
            {
                EditorGUILayout.PropertyField(e_Variance, new GUIContent("Variance"));
                e_Numberbullet.intValue = EditorGUILayout.IntSlider("Number bullet", e_Numberbullet.intValue, 1, 1000);
            }
            folduotHitEffect = EditorGUILayout.Foldout(folduotHitEffect, new GUIContent("Hit Effect"));
            if (folduotHitEffect)
            {
                for (int i = 0; i < e_BulletHitEffects.arraySize; i++)
                {
                    EditorGUILayout.BeginVertical("HelpBox");
                    EditorGUILayout.PropertyField(e_BulletHitEffects.GetArrayElementAtIndex(i), true);
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Edit Surface");
                    if (GUILayout.Button("Remove", GUILayout.Width(70)))
                    {
                        e_BulletHitEffects.DeleteArrayElementAtIndex(i);
                    }
                    EditorGUILayout.EndHorizontal();
                    GUILayout.Space(3);
                    EditorGUILayout.EndVertical();
                }
                GUILayout.Space(3);
                if (GUILayout.Button("Add New Surface"))
                    e_BulletHitEffects.arraySize++;
            }
            GUILayout.Space(7);
            GUILayout.EndVertical();
            serializedObject.ApplyModifiedProperties();
        }
    }
}