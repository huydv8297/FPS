using UnityEngine;
using System.Collections;

namespace UnrealFPS
{
	public struct ScanResult
	{
		public ScanFlags scanFlags;
		public KeyCode key;
		public int joystick;
		public int joystickAxis;
		public float joystickAxisValue;
		public int mouseAxis;
		public object userData;
	}
}