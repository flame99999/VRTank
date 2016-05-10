using UnityEngine;
using System.Collections;

public class GunRot : MonoBehaviour {

	public Transform cameraTransform;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = cameraTransform.rotation;
	}
}
