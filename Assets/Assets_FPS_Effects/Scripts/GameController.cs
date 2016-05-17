using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Transform[]  barrelSpawnPoints;
	public GameObject[]	barrels;
	public GUIText		Timer;
	public GUIText		Score;
	public GUIText		Multiplier;

	private		int	maxBarrelNumber = 10;
	private		int	numberOfBarrels;
	private 	float	barrelSpawnTime;

	private		int currentScore;
	private		int	currentMultiplier;
	private		float multiplierEndTime;
	private		float multiplierDuration;
	private		float	roundTime;

	// Use this for initialization
	void Start () {
		barrelSpawnTime = Time.time ;
		currentMultiplier = 1;
		currentScore = 0;
		multiplierDuration = 2.85f;

		Score.text = currentScore.ToString();
		roundTime = 120;
	}
	
	void Update () 
	{
		if ( roundTime <= 0 )
		{
			Score.color = Color.red;
			Timer.color = Color.grey;
		}
		else
		{
			CountBarrels ();
			UpdateScoreMultiplier ();

			if ( barrelSpawnTime <= Time.time && numberOfBarrels <= maxBarrelNumber )
				SpawnBarrel();
		}


		roundTime -= Time.deltaTime;

		int minutes = ((int)roundTime) / 60;
		int seconds = ((int)roundTime) % 60;

		int tens = seconds / 10;
		int ones = seconds % 10;

		//Timer.text = ((int)(roundTime)).ToString ();

		if ( roundTime > 0 )
			Timer.text = minutes + ":" + tens + ones;
		else
			Timer.text = "0:00";

		if ( Input.GetKey( KeyCode.R ) )
			Application.LoadLevel ( "barrel_Shooting_Game" );
	}

	void CountBarrels()
	{
		numberOfBarrels = GameObject.FindGameObjectsWithTag ("Barrels").Length;
	}

	void SpawnBarrel()
	{
		// pick a random spawnpoint
		int iRandomSpawn = (int)Random.Range ( 0, barrelSpawnPoints.Length );

		Transform spawnPoint = barrelSpawnPoints [iRandomSpawn];

		GameObject barrel = (GameObject)Instantiate (barrels [ (int)Random.Range (0, barrels.Length)], spawnPoint.position, spawnPoint.rotation);
		barrel.GetComponent<Rigidbody>().AddForce( Random.insideUnitSphere * Random.Range (11100, 51100 ) );


		barrelSpawnTime = Time.time + 1.5f; 
	}

	public void BarrelDestroyed()
	{
		if ( roundTime <= 0 )
			return;

		currentScore += currentMultiplier;

		IncreaseMultiplier ();

		Score.text = currentScore.ToString ();
	}

	void	IncreaseMultiplier()
	{
		currentMultiplier++;

		if ( currentMultiplier <= 3 )
			multiplierEndTime = Time.time + multiplierDuration * 2.5f;
		else if ( currentMultiplier <= 6 )
			multiplierEndTime = Time.time + multiplierDuration * 1.7f;
		else
			multiplierEndTime = Time.time + multiplierDuration;
	}

	void UpdateScoreMultiplier()
	{
		float flTimeLeft = multiplierEndTime - Time.time;

		if ( flTimeLeft <= 0 )
		{
			Multiplier.enabled = false;
			currentMultiplier = 1;
		}
		else
		{
			Multiplier.enabled = true;
			Multiplier.text = currentMultiplier.ToString() + "x";

			Color newColor = Color.green;
			newColor.a = ( flTimeLeft / multiplierDuration );

			Multiplier.color = newColor;
		}
	}
}
