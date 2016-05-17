using UnityEngine;
using System.Collections;

public class explode : MonoBehaviour {

	public GameObject explosionEffect;
	public Transform  explosionEffectLocation;
	public float health;

	private bool bExploded;
	private GameController	gameController;

	void Awake()
	{
		bExploded = false;

		GameObject goTemp = GameObject.FindGameObjectWithTag ("GameController");
		gameController = goTemp.GetComponent<GameController> ();
	}

	void Update () 
	{
	
		if (health <= 0 && bExploded == false )
		{
			bExploded = true;
			Instantiate (explosionEffect, explosionEffectLocation.position, Quaternion.LookRotation( Vector3.up ) );
			Destroy (gameObject );

			gameController.BarrelDestroyed();
		}
	}



	public void TakeDamage( float flDamage )
	{
		health -= flDamage;
	}
}
