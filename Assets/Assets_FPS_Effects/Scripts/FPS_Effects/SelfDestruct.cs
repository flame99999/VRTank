using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {

	public float lifeTime;
	private float dieTime;

	void Start () 
	{
		dieTime = Time.time + lifeTime;
	}

	void Update()
	{
		if ( Time.time >= dieTime )
			Destroy ( gameObject );
	}
}
