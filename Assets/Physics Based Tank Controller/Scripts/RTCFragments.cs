//----------------------------------------------
//          Realistic Tank Controller
//
// Copyright © 2015 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using System.Collections;

public class RTCFragments : MonoBehaviour {

	private bool broken = false;

	void Start () {
		GetComponent<Rigidbody>().isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(!broken)
			Checking();

	}

	void Checking(){

		RaycastHit hit;
		Debug.DrawRay(transform.position, -transform.forward * .35f);
		
		if(Physics.Raycast(transform.position, -transform.forward, out hit)){
			if(hit.rigidbody && hit.rigidbody.isKinematic != true){
				GetComponent<Rigidbody>().isKinematic = false;
				broken = true;
			}
		}

	}

	void OnCollisionEnter (Collision collision) {

		if(collision.relativeVelocity.magnitude < 1.5f)
			return;
		
		if(collision.transform.gameObject.layer != LayerMask.NameToLayer("Fragment")){
			GetComponent<Rigidbody>().isKinematic = false;
		}

	}


}
