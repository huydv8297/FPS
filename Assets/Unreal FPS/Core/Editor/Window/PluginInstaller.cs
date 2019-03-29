/* ================================================================
   ---------------------------------------------------
   Project   :    Unreal FPS
   Publisher :    Infinite Dawn
   Author    :    Tamerlan Favilevich
   ---------------------------------------------------
   Copyright © Tamerlan Favilevich 2017 - 2018 All rights reserved.
   ================================================================ */

using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace UnrealFPS.Editor
{
    [System.Serializable]
    public struct Plugin
    {
        public string name;
        public string group;
        public string path;
    }

    public class PluginInstaller : EditorWindow
    {
        private static Vector2 PluginInstallerWindowSize = new Vector2(500, 300);
        private Vector2 scrollPos;
        private static string PluginPath;
        private List<Plugin> plugins = new List<Plugin>();
        private List<string> groups = new List<string>();
        private string installPlugins;

        [MenuItem("Unreal FPS/Plugin Installer", false, 201)]
        public static void Open()
        {
            PluginInstaller pluginInstallerWindow = (PluginInstaller)GetWindow(typeof(PluginInstaller), true, "Plugin Installer");
            pluginInstallerWindow.minSize = new Vector2(PluginInstallerWindowSize.x, PluginInstallerWindowSize.y);
            pluginInstallerWindow.maxSize = new Vector2(PluginInstallerWindowSize.x, PluginInstallerWindowSize.y);
            pluginInstallerWindow.position = new Rect(
                (Screen.currentResolution.width / 2) - (PluginInstallerWindowSize.x / 2),
                (Screen.currentResolution.height / 2) - (PluginInstallerWindowSize.y / 2),
                PluginInstallerWindowSize.x,
                PluginInstallerWindowSize.y);
            pluginInstallerWindow.Show();
        }

        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        protected virtual void OnEnable()
        {
            // Generate Plugins
            PluginPath = PreferencesMenu.Data.RootPath + PreferencesMenu.Data.PluginPath;
            DirectoryInfo directoryInfo = new DirectoryInfo(PluginPath);
            for (int i = 0; i < directoryInfo.GetDirectories().Length; i++)
            {
                FileInfo[] fileInfo = directoryInfo.GetDirectories()[i].GetFiles("*.unitypackage");
                for (int j = 0; j < fileInfo.Length; j++)
                {
                    plugins.Add(new Plugin()
                    {
                        name = Path.GetFileNameWithoutExtension(fileInfo[j].Name),
                        group = directoryInfo.GetDirectories()[i].Name,
                        path = PluginPath + "/" + directoryInfo.GetDirectories()[i].Name + "/" + fileInfo[j].Name
                    });
                }
            }

            // Get and sort groups
            for (int i = 0; i < plugins.Count; i++)
            {
                if (!groups.Contains(plugins[i].group))
                {
                    groups.Add(plugins[i].group);
                }
            }
            groups.Sort();

            // Set Default Value for Install Plugin
            installPlugins = "None";
        }

        /// <summary>
        /// OnGUI is called for rendering and handling GUI events.
        /// This function can be called multiple times per frame (one call per event).
        /// </summary>
        protected virtual void OnGUI()
        {
            scrollPos = GUILayout.BeginScrollView(scrollPos);
            GUILayout.BeginHorizontal(EditorStyles.label);
            GUILayout.Label("Plugins", EditorStyles.boldLabel);
            GUILayout.EndHorizontal();
            for(int i = 0; i < groups.Count; i++)
            {
                GUILayout.BeginVertical(EditorStyles.helpBox);
                GUILayout.Label(groups[i], EditorStyles.boldLabel);
                for (int j = 0; j < plugins.Count; j++)
                {
                    if(plugins[j].group == groups[i])
                    {
                        if (GUILayout.Button(plugins[j].name, GUILayout.Height(20)))
                        {
                            installPlugins = plugins[j].name;
                        }
                    }
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndScrollView();
            GUILayout.Space(5);
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Install: " + installPlugins, GUILayout.Height(20)))
            {
                string path = plugins.Find(x => x.name == installPlugins).path;
                if (!string.IsNullOrEmpty(path))
                {
                    AssetDatabase.ImportPackage(path, true);
                }
            }
            GUILayout.Space(5);
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
            GUILayout.EndVertical();
            
        }
    }
}