/* ================================================================
   ---------------------------------------------------
   Project   :    Unreal FPS
   Publisher :    Infinite Dawn
   Author    :    Tamerlan Favilevich
   ---------------------------------------------------
   Copyright © Tamerlan Favilevich 2017 - 2018 All rights reserved.
   ================================================================ */

using UnityEngine;

namespace UnrealFPS
{
    public class CanvasManager : MonoBehaviour
    {
        [SerializeField] private FPController playerController;
        [SerializeField] private Transform menuCanvas; //Menu Canvas
        [SerializeField] private Transform hudCanvas; //HUD Canvas
        [SerializeField] private bool pauseGame; //Set game pause
        [SerializeField] private bool isActive = false; //Menu active state

        private IHealth playerHealth;

        protected virtual void Start()
        {
            playerHealth = playerController.GetComponent<IHealth>();
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected virtual void Update()
        {
            if (UInput.GetButtonDown("Menu"))
            {
                isActive = !isActive;
                menuCanvas.gameObject.SetActive(isActive);
                hudCanvas.gameObject.SetActive(!isActive);
                if (playerHealth.IsAlive)
                    playerController.LockMovement = isActive;
                playerController.MouseLook.SetCursorLock(!isActive);
                if (pauseGame) Time.timeScale = (isActive) ? 0 : 1;
            }
        }

        /// <summary>
        /// [Menu Canvas Summary]
        /// </summary>
        /// <value></value>
        public Transform MenuCanvas { get { return menuCanvas; } set { menuCanvas = value; } }

        /// <summary>
        /// [HUD Canvas Summary]
        /// </summary>
        /// <value></value>
        public Transform HUDCanvas { get { return hudCanvas; } set { hudCanvas = value; } }
    }
}