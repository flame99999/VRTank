//----------------------------------------------
//          Realistic Tank Controller
//
// Copyright © 2015 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using System.Collections;

public class RTCSetCom : MonoBehaviour {

	public Vector3 COM;

	void Start () {

		GetComponent<Rigidbody>().centerOfMass = COM;
	
	}

}
