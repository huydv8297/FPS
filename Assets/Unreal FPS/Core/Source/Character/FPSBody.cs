/* =====================================================================
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
	[RequireComponent(typeof(Animator))]
	public class FPSBody : MonoBehaviour
	{
		[SerializeField] private float default_Y;
		[SerializeField] private float crouch_Y;
		[SerializeField] private InverseKinematics inverseKinematics = new InverseKinematics();

		private FPController controller;
		private Animator animator;

		/// <summary>
		/// Start is called on the frame when a script is enabled just before
		/// any of the Update methods is called the first time.
		/// </summary>
		private void Start()
		{
			animator = GetComponent<Animator>();
			controller = transform.root.GetComponent<FPController>();
			inverseKinematics.Init(animator);
		}

		/// <summary>
		/// Update is called every frame, if the MonoBehaviour is enabled.
		/// </summary>
		private void Update()
		{
			float speed = UInput.GetAxis("Vertical");
			float direction = UInput.GetAxis("Horizontal");
			float amount = Mathf.Clamp01(Mathf.Abs(speed) + Mathf.Abs(direction));

			if (amount > 0)
			{
				if (controller.IsRunning)
					speed = 2;
				animator.SetFloat("Speed", speed);
				animator.SetFloat("Direction", direction);
			}

			animator.SetBool("IsCrouching", controller.FPCrouch.IsCrouch);
			float fixedVerticalPosition;
			if (controller.FPCrouch.IsCrouch)
				fixedVerticalPosition = Mathf.MoveTowards(transform.localPosition.y, crouch_Y, 7 * Time.deltaTime);
			else
				fixedVerticalPosition = Mathf.MoveTowards(transform.localPosition.y, default_Y, 7 * Time.deltaTime);
			transform.localPosition = new Vector3(transform.localPosition.x, fixedVerticalPosition, transform.localPosition.z);
		}

		/// <summary>
		/// Callback for setting up animation IK (inverse kinematics).
		/// </summary>
		/// <param name="layerIndex">Index of the layer on which the IK solver is called.</param>
		void OnAnimatorIK(int layerIndex)
		{
			inverseKinematics.FootIK();
		}
	}
}