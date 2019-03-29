/* ================================================================
   ---------------------------------------------------
   Project   :    Unreal FPS
   Publisher :    Infinite Dawn
   Author    :    Tamerlan Favilevich
   ---------------------------------------------------
   Copyright © Tamerlan Favilevich 2017 - 2018 All rights reserved.
   ================================================================ */

using System;
using UnityEngine;

namespace UnrealFPS
{
    [Serializable]
    public class FPTilts
    {
        #region Private SerializeField Variable
        [SerializeField] private float angle;
        [SerializeField] private float maxDelta;
        [SerializeField] private float outputRange;
        [SerializeField] private float smooth;
        #endregion

        #region Private Variable
        private NGMouseLook mouseLook;
        private Camera camera;
        private Vector3 originalCameraPosition;
        #endregion

        /// <summary>
        /// Initialize the required components
        /// </summary>
        /// <param name="mouseLook"></param>
        /// <param name="camera"></param>
        /// <param name="originalCameraPosition"></param>
        public void Init(NGMouseLook mouseLook, Camera camera, Vector3 originalCameraPosition)
        {
            this.mouseLook = mouseLook;
            this.camera = camera;
            this.originalCameraPosition = originalCameraPosition;
        }

        /// <summary>
        /// Player Tilts Handler
        /// </summary>
        public void UpdateTilts()
        {
            Quaternion resetRotation = Quaternion.Euler(mouseLook.m_CameraTargetRot.x, mouseLook.m_CameraTargetRot.y, 0);

            //Right Rotation Tilts
            if (UInput.GetButtonDown("TiltRight"))
                mouseLook.m_CameraTargetRot.z = Mathf.MoveTowards(mouseLook.m_CameraTargetRot.z, -angle, maxDelta * Time.deltaTime);

            if (UInput.GetButtonUp("TiltRight"))
                mouseLook.m_CameraTargetRot = resetRotation;

            //Left Rotation Tilts
            if (UInput.GetButtonDown("TiltLeft"))
                mouseLook.m_CameraTargetRot.z = Mathf.MoveTowards(mouseLook.m_CameraTargetRot.z, angle, maxDelta * Time.deltaTime);

            if (UInput.GetButtonUp("TiltLeft"))
                mouseLook.m_CameraTargetRot = resetRotation;

            //Right Position Tilts
            if (UInput.GetButton("TiltRight"))
                camera.transform.localPosition = Vector3.Lerp(camera.transform.localPosition, new Vector3(outputRange, camera.transform.localPosition.y, camera.transform.localPosition.z), smooth * Time.deltaTime);

            //Left Position Tilts
            if (UInput.GetButton("TiltLeft"))
                camera.transform.localPosition = Vector3.Lerp(camera.transform.localPosition, new Vector3(-outputRange, camera.transform.localPosition.y, camera.transform.localPosition.z), smooth * Time.deltaTime);

            //Return Camera Posotion
            if (!UInput.GetButton("TiltRight") && !UInput.GetButton("TiltLeft"))
                camera.transform.localPosition = Vector3.Slerp(camera.transform.localPosition, new Vector3(originalCameraPosition.x, originalCameraPosition.y, originalCameraPosition.z), smooth * Time.deltaTime);
        }

        /// <summary>
        /// Tilts smooth
        /// </summary>
        public float Smooth
        {
            get
            {
                return smooth;
            }

            set
            {
                smooth = value;
            }
        }

        /// <summary>
        /// Tilts angle
        /// </summary>
        public float Angle
        {
            get
            {
                return angle;
            }

            set
            {
                angle = value;
            }
        }

        /// <summary>
        /// Tilts output range
        /// </summary>
        public float OutputRange
        {
            get
            {
                return outputRange;
            }

            set
            {
                outputRange = value;
            }
        }

        /// <summary>
        /// Tilts max delta
        /// </summary>
        public float MaxDelta
        {
            get
            {
                return maxDelta;
            }

            set
            {
                maxDelta = value;
            }
        }
    }
}
