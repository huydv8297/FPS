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
    [CustomEditor(typeof(FPController))]
    [CanEditMultipleObjects]
    public class FPControllerEditor : UEditor
    {
        #region Private SerializeField Variable
        private SerializedProperty e_Camera;
        private SerializedProperty e_WalkSpeed;
        private SerializedProperty e_RunSpeed;
        private SerializedProperty e_RunstepLenghten;
        private SerializedProperty e_JumpSpeed;
        private SerializedProperty e_StickToGroundForce;
        private SerializedProperty e_GravityMultiplier;
        private SerializedProperty e_UseFovKick;
        private SerializedProperty e_UseHeadBob;
        private SerializedProperty e_StepInterval;
        private SerializedProperty e_JumpSound;
        private SerializedProperty e_LandSound;
        #endregion

        #region Instance
        private SerializedProperty e_MouseLook;
        private SerializedProperty e_FovKick;
        private SerializedProperty e_HeadBob;
        private SerializedProperty e_JumpBob;
        private SerializedProperty e_FPSwimming;
        private SerializedProperty e_FPCrouch;
        private SerializedProperty e_FPTilts;
        private SerializedProperty e_FPClimb;
        private SerializedProperty e_FPGrab;
        private SerializedProperty e_PickUpWeapon;
        #endregion

        #region Private Variable
        private bool foldout_FootStepSound;
        private bool foldout_Movement;
        private bool foldout_JumpSound;
        private bool foldout_Climb;
        private bool foldout_ClimbSound;
        #endregion

        void OnEnable()
        {
            e_Camera = serializedObject.FindProperty("m_Camera");
            e_WalkSpeed = serializedObject.FindProperty("m_WalkSpeed");
            e_RunSpeed = serializedObject.FindProperty("m_RunSpeed");
            e_RunstepLenghten = serializedObject.FindProperty("m_RunstepLenghten");
            e_JumpSpeed = serializedObject.FindProperty("m_JumpSpeed");
            e_StickToGroundForce = serializedObject.FindProperty("m_StickToGroundForce");
            e_GravityMultiplier = serializedObject.FindProperty("m_GravityMultiplier");
            e_MouseLook = serializedObject.FindProperty("m_MouseLook");
            e_UseFovKick = serializedObject.FindProperty("m_UseFovKick");
            e_FovKick = serializedObject.FindProperty("m_FovKick");
            e_UseHeadBob = serializedObject.FindProperty("m_UseHeadBob");
            e_HeadBob = serializedObject.FindProperty("m_HeadBob");
            e_JumpBob = serializedObject.FindProperty("m_JumpBob");
            e_StepInterval = serializedObject.FindProperty("m_StepInterval");
            e_JumpSound = serializedObject.FindProperty("m_JumpSound");
            e_LandSound = serializedObject.FindProperty("m_LandSound");
            e_FPCrouch = serializedObject.FindProperty("m_FPCrouch");
            e_FPTilts = serializedObject.FindProperty("m_FPTilts");
            e_FPGrab = serializedObject.FindProperty("m_FPGrab");
            e_FPClimb = serializedObject.FindProperty("m_FPClimb");
            e_FPSwimming = serializedObject.FindProperty("m_FPSwimming");
            e_PickUpWeapon = serializedObject.FindProperty("m_PickUpWeapon");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            BeginBackground();

            Title("First Person Controller");


            //Foldout Movement
            GUILayout.BeginVertical("Button", GUILayout.Height(50));
            EditorGUILayout.Space();
            foldout_Movement = EditorGUILayout.Foldout(foldout_Movement, new GUIContent("Movement"));
            if (foldout_Movement)
            {
                //Camera
                EditorGUILayout.PropertyField(e_Camera, new GUIContent("Camera", "Add to here the Weapon Camera"));

                //Walk Speed
                EditorGUILayout.Slider(e_WalkSpeed, 0.0f, 100.0f, new GUIContent("Walk Speed", "Change the value \"Walk Speed\" to change walk speed of player movement."));

                //Run Speed
                EditorGUILayout.Slider(e_RunSpeed, 0.0f, 100.0f, new GUIContent("Run Speed", "Change the value \"Run Speed\" to change rub speed of player movement."));

                //Step Interval
                EditorGUILayout.Slider(e_StepInterval, 0.0f, 100.0f, new GUIContent("Step Interval", "Value \"Step Interval\" the changing the interval between steps (walk)."));

                //Runstep Lenghten
                EditorGUILayout.Slider(e_RunstepLenghten, 0.0f, 100.0f, new GUIContent("Runstep Lenghten", "Value \"Runstep Lenghten\" the changing the interval between steps (run)."));

                //Jump Force
                EditorGUILayout.Slider(e_JumpSpeed, 0.0f, 100.0f, new GUIContent("Jump Force", "Value \"Jump Force\" the changes strength of the jump."));

                //Stick To Ground Force
                EditorGUILayout.Slider(e_StickToGroundForce, 0.0f, 100.0f, new GUIContent("Stick To Ground Force"));

                //Gravity Multiplier
                EditorGUILayout.Slider(e_GravityMultiplier, 0.0f, 100.0f, new GUIContent("Gravity Multiplier", "The value \"Gravity Multiplier\" affects the strength of attraction."));
            }
            if (!foldout_Movement) { EditorGUILayout.LabelField("Edit Movement"); }
            EditorGUILayout.Space();
            GUILayout.EndVertical();


            //Mouse Look
            GUILayout.BeginVertical("Button", GUILayout.Height(50));
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(e_MouseLook, new GUIContent("Mouse Control"), true);
            EditorGUILayout.LabelField("Edit the Mouse Control");
            EditorGUILayout.Space();
            GUILayout.EndVertical();


            //FOV Kick
            GUILayout.BeginVertical("Button", GUILayout.Height(50));
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(e_FovKick, new GUIContent("FOV Kick"), true);
            EditorGUILayout.LabelField("Edit the FOV Kick");
            EditorGUILayout.PropertyField(e_UseFovKick, new GUIContent("Use Fov Kick"));
            EditorGUILayout.Space();
            GUILayout.EndVertical();


            //Head Bob
            GUILayout.BeginVertical("Button", GUILayout.Height(50));
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(e_HeadBob, new GUIContent("Head Bob"), true);
            EditorGUILayout.LabelField("Edit the Head Bob");
            EditorGUILayout.PropertyField(e_UseHeadBob, new GUIContent("Use Head Bob"));
            EditorGUILayout.Space();
            GUILayout.EndVertical();


            //Jump Bob
            GUILayout.BeginVertical("Button", GUILayout.Height(50));
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(e_JumpBob, new GUIContent("Jump Bob"), true);
            EditorGUILayout.LabelField("Edit the Jump Bob");
            EditorGUILayout.Space();
            GUILayout.EndVertical();


            //Foldout Jump
            GUILayout.BeginVertical("Button");
            EditorGUILayout.Space();
            foldout_JumpSound = EditorGUILayout.Foldout(foldout_JumpSound, new GUIContent("Jump Sound"));
            if (foldout_JumpSound)
            {
                //Jump Sound
                EditorGUILayout.PropertyField(e_JumpSound, new GUIContent("Jump Sound", "The sound when you jump"), true);

                //Land Sound
                EditorGUILayout.PropertyField(e_LandSound, new GUIContent("Land Sound", "The sound upon landing"), true);
            }
            if (!foldout_JumpSound) { EditorGUILayout.LabelField("Edit Jump Sound"); }
            EditorGUILayout.Space();
            GUILayout.EndVertical();

            //Foldout Surface List
            GUILayout.BeginVertical("Button");
            EditorGUILayout.Space();
            foldout_FootStepSound = EditorGUILayout.Foldout(foldout_FootStepSound, new GUIContent("FootStep Sound"));
            if (foldout_FootStepSound)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Edit Footsteps Sound");
                if(GUILayout.Button("Movement Sound Manager")) { MovementSoundManager.Open(); }
                GUILayout.EndHorizontal();
            }
            if (!foldout_FootStepSound) { EditorGUILayout.LabelField("Edit FootStep Sound"); }
            EditorGUILayout.Space();
            GUILayout.EndVertical();

            //Foldout Crouch
            GUILayout.BeginVertical("Button");
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(e_FPCrouch, new GUIContent("Crouch"), true);
            EditorGUILayout.LabelField("Edit Crouch");
            EditorGUILayout.Space();
            GUILayout.EndVertical();


            //Foldout Grab
            GUILayout.BeginVertical("Button");
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(e_FPGrab, new GUIContent("Grab"), true);
            EditorGUILayout.LabelField("Edit Grab");
            EditorGUILayout.Space();
            GUILayout.EndVertical();


            //Foldout Tilts
            GUILayout.BeginVertical("Button");
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(e_FPTilts, new GUIContent("Tilts"), true);
            EditorGUILayout.LabelField("Edit Tilts");
            EditorGUILayout.Space();
            GUILayout.EndVertical();


            //Foldout Climb
            GUILayout.BeginVertical("Button");
            EditorGUILayout.Space();
            foldout_Climb = EditorGUILayout.Foldout(foldout_Climb, new GUIContent("Climb"));
            if (foldout_Climb)
            {
                EditorGUILayout.PropertyField(e_FPClimb.FindPropertyRelative("speed"), new GUIContent("Speed"));
                EditorGUILayout.PropertyField(e_FPClimb.FindPropertyRelative("playSoundCycle"), new GUIContent("Play Sound Cycle"));
                EditorGUILayout.PropertyField(e_FPClimb.FindPropertyRelative("playTime"), new GUIContent("Play Time"));
                EditorGUILayout.PropertyField(e_FPClimb.FindPropertyRelative("useWeapon"), new GUIContent("Use Weapon"));
                GUILayout.BeginHorizontal();
                GUILayout.Label("Edit Climb Sound");
                if(GUILayout.Button("Movement Sound Manager")) { MovementSoundManager.Open(); }
                GUILayout.EndHorizontal();
            }
            if (!foldout_FootStepSound) { EditorGUILayout.LabelField("Edit Climb System"); }
            EditorGUILayout.Space();
            GUILayout.EndVertical();

            //Foldout Swim
            GUILayout.BeginVertical("Button");
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(e_FPSwimming, new GUIContent("Swim (Pre-Alpha)"), true);
            EditorGUILayout.LabelField("Edit Swim");
            EditorGUILayout.Space();
            GUILayout.EndVertical();

            //Foldout Pick Up Weapon
            GUILayout.BeginVertical("Button");
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(e_PickUpWeapon, new GUIContent("Pick Up Weapon"), true);
            EditorGUILayout.LabelField("Edit Pick Up Weapon");
            EditorGUILayout.Space();
            GUILayout.EndVertical();

            EndBackground();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
