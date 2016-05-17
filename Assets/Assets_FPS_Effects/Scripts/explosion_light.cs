using UnityEngine;
using System.Collections;

public class explosion_light : MonoBehaviour {

	private Light explosionLight;
	private float dieTime;

	public float lifeTime;

	void Start () 
	{
		explosionLight = GetComponent<Light> ();
		dieTime = Time.time + lifeTime;
	}
	

	void Update () 
	{
		float flLife = (dieTime - Time.time);

		explosionLight.intensity = Mathf.Lerp (explosionLight.intensity, flLife, Time.deltaTime * 10 );

		if  ( dieTime <= Time.time )
			Destroy ( gameObject );
	}
}
