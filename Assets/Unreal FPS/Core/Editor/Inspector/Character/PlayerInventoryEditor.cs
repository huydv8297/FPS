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
    [CustomEditor(typeof(PlayerInventory))]
    [CanEditMultipleObjects]
    public class PlayerInventoryEditor : UEditor
    {
        private SerializedProperty e_FPSCamera;
        private SerializedProperty e_InventoryGroups;
        private bool isEdit;
        private bool foldout;

        private void OnEnable()
        {
            e_FPSCamera = serializedObject.FindProperty("fpsCamera");
            e_InventoryGroups = serializedObject.FindProperty("inventoryGroups");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            BeginBackground();
            Title("Inventory");
            GUILayout.BeginVertical("Button");
            GUILayout.Space(5);
            foldout = EditorGUILayout.Foldout(foldout, "Inventory", true);
            if (foldout)
            {
                EditorGUILayout.PropertyField(e_FPSCamera, new GUIContent("FPS Camera"));
                GUILayout.Space(5);
                for (int i = 0; i < e_InventoryGroups.arraySize; i++)
                {
                    SerializedProperty e_InventoryGroup = e_InventoryGroups.GetArrayElementAtIndex(i);
                    GUILayout.BeginVertical();
                    GUILayout.Space(3);
                    if (!isEdit)
                    {
                        GUILayout.Label(e_InventoryGroup.FindPropertyRelative("name").stringValue, EditorStyles.boldLabel);
                    }
                    else
                    {
                        EditorGUILayout.PropertyField(e_InventoryGroup.FindPropertyRelative("name"), new GUIContent("Group Name"));
                    }

                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    GUILayout.Space(2);
                    for (int j = 0; j < e_InventoryGroup.FindPropertyRelative("weaponCompartment").arraySize; j++)
                    {
                        SerializedProperty weaponCompartment = e_InventoryGroup.FindPropertyRelative("weaponCompartment").GetArrayElementAtIndex(j);
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Weapon " + (j + 1));
                        GUILayout.Space(1);
                        EditorGUILayout.PropertyField(weaponCompartment.FindPropertyRelative("key"), GUIContent.none, GUILayout.Width(100));
                        GUILayout.Space(3);
                        EditorGUILayout.PropertyField(weaponCompartment.FindPropertyRelative("weapon"), GUIContent.none);
                        if (GUILayout.Button("x", EditorStyles.miniButton, GUILayout.Width(20), GUILayout.Height(17)))
                        {
                            e_InventoryGroup.FindPropertyRelative("weaponCompartment").DeleteArrayElementAtIndex(j);
                        }
                        GUILayout.EndHorizontal();

                    }
                    GUILayout.Space(2);
                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Add Weapon", EditorStyles.miniButton, GUILayout.Height(17)))
                    {
                        e_InventoryGroup.FindPropertyRelative("weaponCompartment").arraySize++;
                    }
                    if (isEdit)
                    {
                        if (GUILayout.Button("Remove Group", EditorStyles.miniButton, GUILayout.Height(17)))
                        {
                            e_InventoryGroups.DeleteArrayElementAtIndex(i);
                        }
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.Space(2);
                    GUILayout.EndVertical();

                    GUILayout.EndVertical();
                    GUILayout.Space(5);
                }
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Add Group", EditorStyles.miniButtonLeft, GUILayout.Height(17)))
                {
                    e_InventoryGroups.arraySize++;
                }
                if (isEdit && GUILayout.Button("Apply", EditorStyles.miniButtonRight, GUILayout.Height(17)))
                {
                    isEdit = false;
                }
                else if (!isEdit && GUILayout.Button("Edit", EditorStyles.miniButtonRight, GUILayout.Height(17)))
                {
                    isEdit = true;
                }
                GUILayout.EndHorizontal();
            }
            if(!foldout){GUILayout.Label("Edit Inventory");}
            GUILayout.Space(5);
            GUILayout.EndVertical();
            EndBackground();
            serializedObject.ApplyModifiedProperties();
        }

    }
}