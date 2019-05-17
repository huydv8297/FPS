using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public Transform camera;
    public Transform player;
    public bool isPlay = true;
    public float yRot;
    public float xRot;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isPlay) {
            yRot = Input.GetAxis("Mouse X") * XSensitivity;
            xRot = Input.GetAxis("Mouse Y") * YSensitivity;

            player.Rotate(0, yRot, 0);
            camera.Rotate(xRot, 0, 0);
            Debug.Log(camera.rotation.eulerAngles);
            if (camera.rotation.eulerAngles.x > 100 || camera.rotation.eulerAngles.x < 98)
                camera.Rotate(-xRot, 0, 0);
        }
	}
}
