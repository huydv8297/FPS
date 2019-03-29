/* ====================================================================
   ---------------------------------------------------
   Project   :    Unreal FPS
   Publisher :    Infinite Dawn
   Author    :    Tamerlan Favilevich
   ---------------------------------------------------
   Copyright © Tamerlan Favilevich 2017 - 2018 All rights reserved.
   ================================================================ */

using UnityEngine;

namespace UnrealFPS.Editor
{
    public class PreferencesData : ScriptableObject
    {
        [SerializeField] private string rootPath;
        [SerializeField] private string pluginPath;
        [SerializeField] private Color32 backgroundColor;
        [SerializeField] private Color32 boxColor;

        /// <summary>
        /// Root folder path
        /// </summary>
        public string RootPath
        {
            get
            {
                return rootPath;
            }

            set
            {
                rootPath = value;
            }
        }

        /// <summary>
        /// Plugin folder path
        /// </summary>
        public string PluginPath
        {
            get
            {
                return pluginPath;
            }

            set
            {
                pluginPath = value;
            }
        }

        /// <summary>
        /// Panel color
        /// </summary>
        public Color32 BackgroundColor
        {
            get
            {
                return backgroundColor;
            }

            set
            {
                backgroundColor = value;
            }
        }

        /// <summary>
        /// Box color
        /// </summary>
        public Color32 BoxColor
        {
            get
            {
                return boxColor;
            }

            set
            {
                boxColor = value;
            }
        }
    }
}