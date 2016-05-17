using UnityEngine;
using System.Collections;

public class controller : MonoBehaviour {

	public float rotationSpeed;


	public Camera	mainCamera;
	public Transform cameraAnchor;
	public Transform muzzleflashLocation;
	public int	fireRate = 800;
	public Light muzzleLight;
	public GUIText	muzzleFlashTypeLabel;

	public ParticleSystem[]	muzzleFlashes;

	public int		currentMuzzleFlashType;

	private LineRenderer	laserAim;
	private Vector3 vecPointAt;
	private float 	lastFireTime;
	private float	enableMuzzleLightUntil;
	private FPS_BulletImpacts	bulletImpactCTRL;
	private float currentRotationSpeed;
	private float muzzleflashChangeTimeThrottle;

	void Start () 
	{
		vecPointAt = transform.position + transform.up * -100;
		lastFireTime = -1.0f;
		enableMuzzleLightUntil = -1.0f;

		GameObject goBulletImpactCTRL = GameObject.FindGameObjectWithTag ("Bullet_Impacts_Control");
		if ( goBulletImpactCTRL != null )
			bulletImpactCTRL = goBulletImpactCTRL.GetComponent< FPS_BulletImpacts>();

		laserAim = GetComponent<LineRenderer> ();

		currentRotationSpeed = rotationSpeed;
		muzzleflashChangeTimeThrottle = Time.time;
	}
	
	void Update() 
	{
		if  (Input.GetKey( KeyCode.M ) && muzzleflashChangeTimeThrottle <= Time.time )
		{
			muzzleflashChangeTimeThrottle = Time.time + 0.25f;

			currentMuzzleFlashType++;

			if ( currentMuzzleFlashType > 2 )
				currentMuzzleFlashType = 0;
		}

		switch ( currentMuzzleFlashType )
		{
			case 0:		muzzleFlashTypeLabel.text = "MuzzleFlash Generic"; break;
			case 1:		muzzleFlashTypeLabel.text = "MuzzleFlash Star"; break;
			case 2:		muzzleFlashTypeLabel.text = "MuzzleFlash Vee"; break;
		}
				
		float vertical = Input.GetAxis("Vertical") * currentRotationSpeed;
		float horizontal = Input.GetAxis("Horizontal") * currentRotationSpeed;

		if (Mathf.Abs (vertical) <= 0.1f && Mathf.Abs (horizontal) <= 0.1f)
			currentRotationSpeed = Mathf.Lerp (currentRotationSpeed, 0, Time.deltaTime * 2.5f );
		else
			currentRotationSpeed = Mathf.Lerp (currentRotationSpeed, rotationSpeed, Time.deltaTime * 1.5f );

		Debug.Log (currentRotationSpeed);

		vertical *= Time.deltaTime * currentRotationSpeed * 0.03f;
		horizontal *= Time.deltaTime * currentRotationSpeed * 0.03f;
		//transform.Rotate(vertical * -1, 0, horizontal);

		// move the gun
		transform.RotateAround (transform.position, Vector3.up, horizontal );
		transform.RotateAround (transform.position, transform.right, vertical * -1 );

		// update teh camera position/rotation
		vecPointAt = Vector3.Lerp (vecPointAt, transform.position + transform.up * -100, Time.deltaTime * 4);
		mainCamera.transform.LookAt (vecPointAt);
		mainCamera.transform.position = Vector3.Lerp (mainCamera.transform.position, cameraAnchor.position, Time.deltaTime * 4);

		if ( Input.GetButton( "Fire1" ) )
			FireBullet();

		fireRate = Mathf.Clamp (fireRate, 100, 900);

		if ( enableMuzzleLightUntil >= Time.time )
			muzzleLight.enabled = true;
		else
			muzzleLight.enabled = false;


		// draw the laser sight
		laserAim.SetPosition (0, muzzleflashLocation.position);
		laserAim.SetPosition (1, muzzleflashLocation.position + muzzleflashLocation.forward * 200 );
	}

	void FireBullet()
	{

		float timeElapsed = Time.time - lastFireTime;
		if ( timeElapsed < 1.0f / ( fireRate / 60.0f ) )
			return;

		lastFireTime = Time.time;


		enableMuzzleLightUntil = Time.time + 0.02f;

		Instantiate (muzzleFlashes [currentMuzzleFlashType], muzzleflashLocation.position, muzzleflashLocation.rotation);

		AudioSource ass = GetComponent<AudioSource> ();
		ass.Play ();

		// do a traceline to check what surface we hit
		RaycastHit hit;
		Physics.Linecast (muzzleflashLocation.position, muzzleflashLocation.position + muzzleflashLocation.transform.forward * 1000, out hit );

		if ( hit.collider != null )
		{
			if ( hit.collider.gameObject == null )
				return;



			bulletImpactCTRL.DoBulletImpact( hit.collider.gameObject, hit.point, hit.normal, Globals.GetMaterialID( hit.collider.sharedMaterial ) );

			GameObject objectHit = hit.collider.gameObject;
			if ( objectHit != null )
			{
				explode explodeScript = objectHit.GetComponent<explode>();
				if ( explodeScript != null )
					explodeScript.TakeDamage( 10 );
			}
		}
	}
}
