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
    [CustomEditor(typeof(WeaponIdentifier))]
    public class WeaponIdentifierEditor : UEditor
    {
        private SerializedProperty e_Weapon;
        private int currentID;

        protected virtual void OnEnable()
        {
            e_Weapon = serializedObject.FindProperty("weapon");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            BeginBackground();
            Title("Weapon Identifier");
            GUILayout.BeginVertical("Button");
            GUILayout.Space(7);
            GUILayout.Space(3);
            if (e_Weapon.objectReferenceValue != null)
            {
                Weapon weapon = e_Weapon.objectReferenceValue as Weapon;
                GUILayout.BeginVertical(EditorStyles.helpBox);
                GUILayout.Space(3);
                GUILayout.BeginHorizontal();
                GUILayout.Label("Weapon", EditorStyles.boldLabel);
                GUILayout.FlexibleSpace();
                currentID = EditorGUIUtility.GetControlID(FocusType.Passive) + 100;
                if (GUILayout.Button("Load New", EditorStyles.miniButton))
                {
                    EditorGUIUtility.ShowObjectPicker<Weapon>(null, false, "", currentID);
                }
                if (Event.current.commandName == "ObjectSelectorClosed" && EditorGUIUtility.GetObjectPickerControlID() == currentID)
                {
                    currentID = -1;
                    if (EditorGUIUtility.GetObjectPickerObject() != null)
                    {
                        if (EditorGUIUtility.GetObjectPickerObject() as Weapon)
                        {
                            e_Weapon.objectReferenceValue = (Weapon)EditorGUIUtility.GetObjectPickerObject();
                        }
                        else
                        {
                            Debug.Log("Selected Element is not Weapon");
                        }
                    }
                }
                GUILayout.EndHorizontal();
                GUILayout.Space(10);
                EditorGUILayout.TextField("ID",  weapon.Id);
                EditorGUILayout.TextField("Display name", weapon.DisplayName);
                EditorGUILayout.TextField("Description", weapon.Description);
                EditorGUILayout.TextField("Group", weapon.Group);
                EditorGUILayout.TextField("Space", weapon.Space.ToString());
                EditorGUILayout.ObjectField("Image", weapon.Image, typeof(Sprite), false);
                EditorGUILayout.ObjectField("Drop", weapon.Drop, typeof(GameObject), false);
                GUILayout.Space(3);
                GUILayout.EndVertical();
            }
            else
            {
                EditorGUILayout.PropertyField(e_Weapon, new GUIContent("Weapon"));
                EditorGUILayout.HelpBox("Add Weapon", MessageType.Info);
            }
            GUILayout.Space(7);
            GUILayout.EndVertical();
            EndBackground();
            serializedObject.ApplyModifiedProperties();
        }
    }
}