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
    [CustomEditor(typeof(PhysicsBullet))]
    [CanEditMultipleObjects]
    public class PhysicsBulletEditor : UEditor
    {
        private SerializedProperty e_Damage;
        private SerializedProperty e_HitEffect;
        private SerializedProperty e_Power;
        private SerializedProperty e_Radius;
        private SerializedProperty e_LifeTime;
        private SerializedProperty e_DestroyOnHit;
        private SerializedProperty e_NumberBullet;
        private SerializedProperty e_Variance;

        private BulletType bulletType;
        private bool folduotHitEffect;
        private string fold;

        void OnEnable()
        {
            e_Damage = serializedObject.FindProperty("damage");
            e_HitEffect = serializedObject.FindProperty("bulletHitEffects");
            e_LifeTime = serializedObject.FindProperty("lifetime");
            e_DestroyOnHit = serializedObject.FindProperty("destroyOnHit");
            e_Power = serializedObject.FindProperty("power");
            e_Radius = serializedObject.FindProperty("radius");
            e_NumberBullet = serializedObject.FindProperty("numberBullet");
            e_Variance = serializedObject.FindProperty("variance");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            BeginBackground();
            Title("Physics Bullet");
            BeginBox();
            bulletType = (BulletType) EditorGUILayout.EnumPopup("Type", bulletType);
            e_Damage.intValue = EditorGUILayout.IntField("Damage", e_Damage.intValue);
            if (bulletType == BulletType.Fractional)
            {
                e_NumberBullet.intValue = EditorGUILayout.IntSlider("Number Bullet", e_NumberBullet.intValue, 1, 1000);
                e_Variance.floatValue = EditorGUILayout.FloatField("Variance", e_Variance.floatValue);
            }
            e_Power.floatValue = EditorGUILayout.FloatField("Power", e_Power.floatValue);
            e_Radius.floatValue = EditorGUILayout.FloatField("Radius", e_Radius.floatValue);
            e_LifeTime.floatValue = EditorGUILayout.FloatField("Timer", e_LifeTime.floatValue);
            e_DestroyOnHit.boolValue = EditorGUILayout.Toggle("Destory On Hit", e_DestroyOnHit.boolValue);
            folduotHitEffect = EditorGUILayout.Foldout(folduotHitEffect, new GUIContent("Hit Effect"));
            if (folduotHitEffect)
            {
                for (int i = 0; i < e_HitEffect.arraySize; i++)
                {
                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                    EditorGUILayout.PropertyField(e_HitEffect.GetArrayElementAtIndex(i), true);
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Remove", GUILayout.Width(70)))
                        e_HitEffect.DeleteArrayElementAtIndex(i);
                    EditorGUILayout.EndHorizontal();
                    GUILayout.Space(3);
                    EditorGUILayout.EndVertical();
                }
                GUILayout.Space(3);
                if (GUILayout.Button("Add New Surface"))
                    e_HitEffect.arraySize++;
            }
            EndBox();
            EndBackground();
            serializedObject.ApplyModifiedProperties();
        }
    }
}