/* ====================================================================
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
    public class PreferencesMenu : UEditor
    {
        // Load Preferences Data from resources folder
        private static PreferencesData preferencesData = Resources.Load("Data/PreferencesData") as PreferencesData;

        [PreferenceItem("Unreal FPS")]
        private static void PreferencesGUI()
        {
            if (preferencesData != null)
            {
                GUILayout.BeginVertical(EditorStyles.helpBox);
                GUILayout.Label("Paths", EditorStyles.boldLabel);
                preferencesData.RootPath = EditorGUILayout.TextField("Root", preferencesData.RootPath);
                preferencesData.PluginPath = EditorGUILayout.TextField("Plugins", preferencesData.PluginPath);
                GUILayout.EndVertical();

                GUILayout.BeginVertical(EditorStyles.helpBox);
                GUILayout.Label("Editor", EditorStyles.boldLabel);
                preferencesData.BackgroundColor = EditorGUILayout.ColorField("Background Color", preferencesData.BackgroundColor);
                preferencesData.BoxColor = EditorGUILayout.ColorField("Box Color", preferencesData.BoxColor);
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Reset", EditorStyles.miniButton))
                {
                    preferencesData.BackgroundColor = new Color32(97, 95, 93, 255);
                    preferencesData.BoxColor = Color.white;
                }
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();

                EditorGUILayout.HelpBox("If you change paths or the specified paths do not coincide with the current ones, change them to the correct", MessageType.Info);

                if (GUI.changed)
                    EditorUtility.SetDirty(preferencesData);
            }
            else
            {
                GUILayout.Label("Preferences Data not exists\nAdd Preferences Data in Editor/Resources/Data/");
            }
        }

        public static PreferencesData Data { get { return preferencesData; } }
    }
}