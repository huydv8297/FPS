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
using UnityEditorInternal;
using UnityEngine;

namespace UnrealFPS.Editor
{
    /// <summary>
    /// Custom editor for Weapon Attack System
    /// </summary>
    [CustomEditor(typeof(WeaponAttackSystem), true)]
    [CanEditMultipleObjects]
    public class WeaponAttackSystemEditor : UEditor
    {
        private WeaponAttackSystem instance;
        private SerializedProperty e_AttackPoint;
        private SerializedProperty e_Bullet; //For Physics Attack
        private SerializedProperty e_RayBullet; //For RayCast Attack
        private SerializedProperty e_Delay;
        private SerializedProperty e_AttackImpulse;
        private SerializedProperty e_AttackRange;
        private SerializedProperty e_AttackSound;
        private SerializedProperty e_EmptySound;
        private SerializedProperty e_MuzzleFlash;
        private SerializedProperty e_CartridgeEjection;
        private SerializedProperty e_SpreadSystem;
        private ReorderableList e_BulletSpreadParams;

        private bool f_SpreadSystem;

        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        protected virtual void OnEnable()
        {
            instance = (WeaponAttackSystem) target;
            e_AttackPoint = serializedObject.FindProperty("attackPoint");
            e_Bullet = serializedObject.FindProperty("bullet");
            e_RayBullet = serializedObject.FindProperty("rayBullet");
            e_Delay = serializedObject.FindProperty("delay");
            e_AttackImpulse = serializedObject.FindProperty("attackImpulse");
            e_AttackRange = serializedObject.FindProperty("attackRange");
            e_AttackSound = serializedObject.FindProperty("attackSound");
            e_EmptySound = serializedObject.FindProperty("emptySound");
            e_MuzzleFlash = serializedObject.FindProperty("muzzleFlash");
            e_CartridgeEjection = serializedObject.FindProperty("cartridgeEjection");
            e_SpreadSystem = serializedObject.FindProperty("spreadSystem");
            e_BulletSpreadParams = new ReorderableList(e_SpreadSystem.serializedObject, e_SpreadSystem.FindPropertyRelative("bulletSpreadParams"), true, true, true, true)
            {
                drawHeaderCallback = (rect) =>
                    {
                        EditorGUI.LabelField(rect, "Spread Parameters");
                    },

                    drawElementCallback = (rect, index, active, focused) =>
                    {
                        SerializedProperty e_BulletSpreadParam = e_BulletSpreadParams.serializedProperty.GetArrayElementAtIndex(index);
                        EditorGUI.LabelField(new Rect(rect.x, rect.y + 15, 30, 15), "State");
                        e_BulletSpreadParam.FindPropertyRelative("state").stringValue = EditorGUI.TextField(new Rect(rect.x + 35, rect.y + 15, 50, 15), e_BulletSpreadParam.FindPropertyRelative("state").stringValue);

                        EditorGUI.LabelField(new Rect(rect.x + 110, rect.y + 1.5f, 37, 15), "Max X");
                        e_BulletSpreadParam.FindPropertyRelative("maxX").floatValue = EditorGUI.FloatField(new Rect(rect.x + 150, rect.y + 1.5f, 50, 15), e_BulletSpreadParam.FindPropertyRelative("maxX").floatValue);
                        EditorGUI.LabelField(new Rect(rect.x + 220, rect.y + 1.5f, 37, 15), "Max Y");
                        e_BulletSpreadParam.FindPropertyRelative("maxY").floatValue = EditorGUI.FloatField(new Rect(rect.x + 260, rect.y + 1.5f, 50, 15), e_BulletSpreadParam.FindPropertyRelative("maxY").floatValue);

                        EditorGUI.LabelField(new Rect(rect.x + 110, rect.y + 27, 37, 15), "Min  X");
                        e_BulletSpreadParam.FindPropertyRelative("minX").floatValue = EditorGUI.FloatField(new Rect(rect.x + 150, rect.y + 27, 50, 15), e_BulletSpreadParam.FindPropertyRelative("minX").floatValue);
                        EditorGUI.LabelField(new Rect(rect.x + 220, rect.y + 27, 37, 15), "Min  Y");
                        e_BulletSpreadParam.FindPropertyRelative("minY").floatValue = EditorGUI.FloatField(new Rect(rect.x + 260, rect.y + 27, 50, 15), e_BulletSpreadParam.FindPropertyRelative("minY").floatValue);
                    },

                    elementHeight = 50
            };
        }

        /// <summary>
        /// Custom Inspector GUI
        /// </summary>
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            BeginBackground();
            Title("Weapon Attack System");
            BeginBox();
            instance.W_AttackType = (AttackType) EditorGUILayout.EnumPopup("Attack Type", instance.W_AttackType);
            EditorGUILayout.PropertyField(e_AttackPoint, new GUIContent("Attack Point"));

            switch (instance.W_AttackType)
            {
                case AttackType.RayCast:
                    EditorGUILayout.PropertyField(e_RayBullet, new GUIContent("Ray Bullet"));
                    break;
                case AttackType.Physics:
                    EditorGUILayout.PropertyField(e_Bullet, new GUIContent("Bullet"));
                    break;
                case AttackType.Throw:
                    EditorGUILayout.PropertyField(e_Bullet, new GUIContent("Ammo"));
                    break;
            }

            EditorGUILayout.PropertyField(e_Delay, new GUIContent("Delay"));
            EditorGUILayout.PropertyField(e_AttackImpulse, new GUIContent("Attack Impulse"));

            switch (instance.W_AttackType)
            {
                case AttackType.RayCast:
                    EditorGUILayout.PropertyField(e_AttackRange, new GUIContent("Attack Range"));
                    break;
                case AttackType.Throw:
                    EditorGUILayout.PropertyField(e_AttackRange, new GUIContent("Throw Time"));
                    break;
            }

            EditorGUILayout.PropertyField(e_AttackSound, new GUIContent("Attack Sound"));
            EditorGUILayout.PropertyField(e_EmptySound, new GUIContent("Empty Sound"));
            EditorGUILayout.PropertyField(e_MuzzleFlash, new GUIContent("Muzzle Flash"));
            EditorGUILayout.PropertyField(e_CartridgeEjection, new GUIContent("Cartridge Ejection"));
            GUILayout.BeginVertical(EditorStyles.helpBox);
            f_SpreadSystem = EditorGUILayout.Foldout(f_SpreadSystem, "Spread System", true);
            if (f_SpreadSystem)
            {
                EditorGUILayout.PropertyField(e_SpreadSystem.FindPropertyRelative("amplitudeX"), new GUIContent("Amplitude by X axis"));
                EditorGUILayout.PropertyField(e_SpreadSystem.FindPropertyRelative("amplitudeZmin"), new GUIContent("Min Amplitude by Z axis"));
                EditorGUILayout.PropertyField(e_SpreadSystem.FindPropertyRelative("amplitudeZmax"), new GUIContent("Max Amplitude by Z axis"));
                EditorGUILayout.PropertyField(e_SpreadSystem.FindPropertyRelative("force"), new GUIContent("Force amplitude"));
                EditorGUILayout.PropertyField(e_SpreadSystem.FindPropertyRelative("lockCamera"), new GUIContent("Auto return", "Auto return camera on default position"));
                EditorGUILayout.PropertyField(e_SpreadSystem.FindPropertyRelative("speed"), new GUIContent("Amplitude speed"));
                e_BulletSpreadParams.DoLayoutList();
            }
            GUILayout.EndVertical();
            EndBox();
            EndBackground();
            serializedObject.ApplyModifiedProperties();

            if (GUI.changed)
                EditorUtility.SetDirty(instance);
        }
    }
}