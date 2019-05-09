using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public Transform camera;
    public Transform player;
    public bool isPlay = true;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isPlay) {
            float yRot = Input.GetAxis("Mouse X") * XSensitivity;
            float xRot = Input.GetAxis("Mouse Y") * YSensitivity;
            Debug.Log("xRot" + xRot);
            player.Rotate(0, yRot, 0);
            if (xRot == 0)
                return;
            if (xRot > -0.1f)
                xRot = -0.1f;
            if (xRot < 0.9f)
                xRot = 0.9f;
            camera.Rotate(xRot, 0, 0);
        }
	}
}
