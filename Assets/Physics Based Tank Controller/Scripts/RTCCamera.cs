//----------------------------------------------
//          Realistic Tank Controller
//
// Copyright © 2015 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.VR;

public class RTCCamera : MonoBehaviour
{
	public Transform tank;
	public float distance = 15.0f;
	public float xSpeed = 50.0f;
	public float ySpeed = 50.0f;
	public float yMinLimit = -20.0f;
	public float yMaxLimit = 40.0f;
	
	private float x;
	private float y;

	public float heightOffset = 3.5f;
	
	void Awake()
	{

		Vector3 angles = transform.eulerAngles;
		x = angles.x;
		y = angles.y;
		
		if(GetComponent<Rigidbody>() != null)
		{
			GetComponent<Rigidbody>().freezeRotation = true;
		}



	}
	
	void LateUpdate()
	{
        if (!VRSettings.enabled)
        {
            if (tank != null)
            {

                x += (float)(Input.GetAxis("Mouse X") * xSpeed * 0.02f);
                y -= (float)(Input.GetAxis("Mouse Y") * ySpeed * 0.02f);

                y = ClampAngle(y, yMinLimit, yMaxLimit);

                Quaternion rotation = Quaternion.Euler(y, x, 0);
                Vector3 position = rotation * (new Vector3(0.0f, heightOffset, -distance)) + tank.position;

                transform.rotation = rotation;
                transform.position = position;

            }
        }
		
	}
	
	private float ClampAngle(float angle, float min, float max)
	{
		if(angle < -360)
		{
			angle += 360;
		}
		if(angle > 360)
		{
			angle -= 360;
		}
		return Mathf.Clamp (angle, min, max);
	}
}