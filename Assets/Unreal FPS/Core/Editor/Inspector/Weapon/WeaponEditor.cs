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
    [CustomEditor(typeof(Weapon))]
    public class WeaponEditor : UEditor
    {
        private SerializedProperty e_ID;
        private SerializedProperty e_DisplayName;
        private SerializedProperty e_Description;
        private SerializedProperty e_Group;
        private SerializedProperty e_Space;
        private SerializedProperty e_Image;
        private SerializedProperty e_Drop;
        private bool editID;

        private void OnEnable()
        {
            e_ID = serializedObject.FindProperty("id");
            e_DisplayName = serializedObject.FindProperty("displayName");
            e_Description = serializedObject.FindProperty("description");
            e_Group = serializedObject.FindProperty("group");
            e_Space = serializedObject.FindProperty("space");
            e_Image = serializedObject.FindProperty("image");
            e_Drop = serializedObject.FindProperty("drop");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            GUILayout.BeginVertical("HelpBox");
            GUILayout.Space(7);
            GUILayout.Label("Weapon", TitleLabel);
            GUILayout.Space(7);
            if (!editID)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("ID");
                GUILayout.FlexibleSpace();
                GUILayout.Label(e_ID.stringValue, "TextField", GUILayout.Width(246));
                if (GUILayout.Button("Edit", GUILayout.Width(37)))
                    editID = !editID;
                GUILayout.EndHorizontal();
            }
            else
            {
                EditorGUILayout.PropertyField(e_ID, new GUIContent("ID"));
            }
            EditorGUILayout.PropertyField(e_DisplayName, new GUIContent("Display Name"));
            EditorGUILayout.PropertyField(e_Description, new GUIContent("Description"));
            EditorGUILayout.PropertyField(e_Group, new GUIContent("Group"));
            EditorGUILayout.PropertyField(e_Space, new GUIContent("Space"));
            EditorGUILayout.PropertyField(e_Image, new GUIContent("Image"));
            EditorGUILayout.PropertyField(e_Drop, new GUIContent("Drop Prefab"));
            GUILayout.Space(7);
            GUILayout.EndVertical();
            serializedObject.ApplyModifiedProperties();
        }
    }
}