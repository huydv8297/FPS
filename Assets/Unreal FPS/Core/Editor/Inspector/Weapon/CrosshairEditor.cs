///* ================================================================
//   ---------------------------------------------------
//   Project   :    Unreal FPS
//   Publisher :    Infinite Dawn
//   Author    :    Tamerlan Favilevich
//   ---------------------------------------------------
//   Copyright © Tamerlan Favilevich 2017 - 2018 All rights reserved.
//   ================================================================ */

//using UnityEditor;
//using UnityEditorInternal;
//using UnityEngine;

//namespace UnrealFPS.Editor
//{
//    [CustomEditor(typeof(Crosshair))]
//    [CanEditMultipleObjects]
//    public class CrosshairEditor : InspectorEditor
//    {
//        private SerializedProperty e_Crosshair;
//        private ReorderableList e_CrosshairStruct;

//        private void OnEnable()
//        {
//            e_Crosshair = serializedObject.FindProperty("crosshair");
//            e_CrosshairStruct = new ReorderableList(serializedObject,
//                    serializedObject.FindProperty("crosshairStruct"),
//                    true, true, true, true);
//            e_CrosshairStruct.drawHeaderCallback = (Rect rect) =>
//            {
//                EditorGUI.LabelField(rect, "Crosshair State");
//            };
//            e_CrosshairStruct.drawElementCallback =
//                (Rect rect, int index, bool isActive, bool isFocused) =>
//                {
//                    var element = e_CrosshairStruct.serializedProperty.GetArrayElementAtIndex(index);
//                    rect.y += 2;
//                    EditorGUI.LabelField(new Rect(rect.x + 5, rect.y, 60, EditorGUIUtility.singleLineHeight), "State:");
//                    EditorGUI.LabelField(new Rect(rect.x + 125, rect.y, 60, EditorGUIUtility.singleLineHeight), "Width:");
//                    EditorGUI.LabelField(new Rect(rect.x + 225, rect.y, 60, EditorGUIUtility.singleLineHeight), "Height:");
//                    EditorGUI.LabelField(new Rect(rect.x + 330, rect.y, 60, EditorGUIUtility.singleLineHeight), "Smooth:");
//                    EditorGUI.PropertyField(
//                        new Rect(rect.x + 50, rect.y, 70, EditorGUIUtility.singleLineHeight),
//                        element.FindPropertyRelative("name"), GUIContent.none);
//                    EditorGUI.PropertyField(
//                        new Rect(rect.x + 170, rect.y, 50, EditorGUIUtility.singleLineHeight),
//                        element.FindPropertyRelative("width"), GUIContent.none);
//                    EditorGUI.PropertyField(
//                        new Rect(rect.x + 275, rect.y, 50, EditorGUIUtility.singleLineHeight),
//                        element.FindPropertyRelative("height"), GUIContent.none);
//                    EditorGUI.PropertyField(
//                    new Rect(rect.x + 385, rect.y, 50, EditorGUIUtility.singleLineHeight),
//                    element.FindPropertyRelative("smooth"), GUIContent.none);
//                };
//            InitGUIStyle(FontStyle.Bold, TextAnchor.MiddleCenter);
//        }

//        public override void OnInspectorGUI()
//        {
//            serializedObject.Update();
//            BeginPanel();
//            Title("Crosshair");

//            GUILayout.BeginVertical("HelpBox");
//            GUILayout.Space(3);
//            GUILayout.BeginVertical("Box");
//            GUILayout.Space(3);
//            EditorGUILayout.PropertyField(e_Crosshair, new GUIContent("Crosshair texture"));
//            GUILayout.Space(3);
//            GUILayout.EndVertical();
//            e_CrosshairStruct.DoLayoutList();
//            GUILayout.Space(3);
//            GUILayout.EndVertical();
//            EndPanel();
//            serializedObject.ApplyModifiedProperties();
//        }
//    }
//}