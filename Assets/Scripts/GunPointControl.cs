﻿using UnityEngine;
using System.Collections;

public class GunPointControl : MonoBehaviour {
	public Transform m_gun;
	public Camera m_camera;
	public GameObject m_ImageSource;
	public UnityEngine.UI.Text debugText;
	public Vector3 debugVec3;

	private float m_distance = 20.0f;
	private float m_moveValue = 0.5f;


	private GameObject[] m_Images;
	private int m_imagesCount = 20;
	// Use this for initialization
	void Start () {
		
		m_Images = new GameObject[m_imagesCount];
		for (int i = 0; i < m_imagesCount; ++i) {
			m_Images[i] = GameObject.Instantiate (m_ImageSource);
			m_Images [i].transform.position = new Vector3 (0.0f, 0.0f, 0.0f);
			m_Images [i].transform.parent = m_ImageSource.transform.parent;
		}

		m_ImageSource.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 t = m_gun.position + m_gun.forward * m_distance;

		Vector3 cameraLookAt = m_camera.transform.position + m_camera.transform.forward * m_distance;
		Vector3 lookAt = t + (cameraLookAt - t).normalized * m_moveValue;
		m_gun.LookAt (lookAt);

		Vector3 screenV = m_camera.WorldToScreenPoint (t);
		screenV.x-=Screen.width/2;
		screenV.y-=Screen.height/2;
		GetComponent<RectTransform> ().localPosition = screenV;

		//Vector3 vTest = GetComponent<RectTransform> ().localPosition;

//		GetComponent<Transform> ().position = m_camera.WorldToScreenPoint (t);
//		Vector3 vTest = GetComponent<Transform> ().position;
		//debugText.text = "x=" + vTest.x + " y=" + vTest.y + " z=" + vTest.z;

		Vector3 lookAt2D = m_camera.WorldToScreenPoint (lookAt);
		//lookAt2D.z = 0;
		Vector3 cameraLookAt2D = m_camera.WorldToScreenPoint (cameraLookAt);
		Vector3 v = new Vector3(1, 0, 0);

		float angle = 360.0f / m_imagesCount;
		float d = 0.5f *(cameraLookAt2D-lookAt2D).magnitude;

		d = Mathf.Clamp (d, 20, 200);

		for (int i = 0; i < m_imagesCount; ++i) {
			v = Quaternion.Euler(0, 0, angle) * v;
			Vector3 temp = lookAt2D + v * d;
			temp.x-=Screen.width/2;
			temp.y-=Screen.height/2;
			m_Images [i].GetComponent<RectTransform> ().localPosition = temp;
            m_Images[i].GetComponent<RectTransform>().localRotation = Quaternion.identity;


        }
	}
}