//----------------------------------------------
//          Realistic Tank Controller
//
// Copyright © 2015 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]

public class RTCTankBullet : MonoBehaviour {

	public GameObject explosionPrefab;
	public float explosionForce = 300000f;
	public float explosionRadius = 5f;
	public int lifeTimeOfTheBullet = 5;
	private float lifeTime;

	void Start(){

		GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
		GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;

	}

	void Update () {
	
		lifeTime += Time.deltaTime;

		if(gameObject.activeSelf && lifeTime > lifeTimeOfTheBullet)
			Explosion();

	}
	

	void OnCollisionEnter (Collision col) {
	
		Explosion();
		
	}

	void Explosion(){

		Instantiate(explosionPrefab, transform.position, transform.rotation);
		Collider[] colliders = Physics.OverlapSphere(transform.position, 5f);
		foreach (Collider hit in colliders) {
			if (hit && hit.GetComponent<Rigidbody>()){
				hit.GetComponent<Rigidbody>().isKinematic = false;
				hit.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, .3f);
			}
		}

		Destroy (gameObject);
		
	}

}
