using UnityEngine;
using System.Collections;
using UnityEngine.VR;
public class CameraControl : MonoBehaviour {

	public float speedH = 2.0f;
	public float speedV = 2.0f;
	private float yaw = 0.0f;
	private float pitch = 0.0f;

	// Use this for initialization
	void Start () {
        InputTracking.Recenter();
		//#if UNITY_EDITOR
		Cursor.visible = false;
		//#endif
	}
	
	// Update is called once per frame
	void Update () {
		//#if UNITY_EDITOR
		yaw += speedH * Input.GetAxis("Mouse X");
		pitch -= speedV * Input.GetAxis("Mouse Y");

		transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
		//#endif
	}
}
