using UnityEngine;
using System.Collections;

public class FPS_BulletImpacts : MonoBehaviour {

	private float flMinHitImpactSoundDist = 10.0f;
	private float flMaxHitImpactSoundDist = 300.0f;

//		CONCRETE = 0,
	public ParticleSystem 	m_psConcreteBulletImpact;
	public ParticleSystem 	m_psConcreteDecal;
	public AudioClip[]		m_acConcreteBulletImpact;

//		DIRT,
	public ParticleSystem 	m_psDirtBulletImpact;
	public ParticleSystem 	m_psDirtDecal;
	public AudioClip[]		m_acDirtBulletImpact;

//		FLESH,
	public ParticleSystem 	m_psFleshBulletImpact;
	public ParticleSystem 	m_psFleshDecal;
	public AudioClip[]		m_acFleshBulletImpact;

//		FOLIAGE,
	public ParticleSystem 	m_psFoliageBulletImpact;
	public AudioClip[]		m_acFoliageBulletImpact;

//		GLASS,
	public ParticleSystem 	m_psGlassBulletImpact;
	public ParticleSystem 	m_psGlassDecal;
	public AudioClip[]		m_acGlassBulletImpact;

//		METAL,
	public ParticleSystem 	m_psMetalBulletImpact;
	public ParticleSystem 	m_psMetalDecal;
	public AudioClip[]		m_acMetalBulletImpact;

//		PAPER,
	public ParticleSystem 	m_psPaperBulletImpact;
	public ParticleSystem 	m_psPaperDecal;
	public AudioClip[]		m_acPaperBulletImpact;

//		PLASTER,
	public ParticleSystem 	m_psPlasterBulletImpact;
	public ParticleSystem 	m_psPlasterDecal;
	public AudioClip[]		m_acPlasterBulletImpact;

//		PLASTIC,
	public ParticleSystem 	m_psPlasticBulletImpact;
	public ParticleSystem 	m_psPlasticDecal;
	public AudioClip[]		m_acPlasticBulletImpact;

//		WATER,
	public ParticleSystem 	m_psWaterBulletImpact;
	public AudioClip[]		m_acWaterBulletImpact;

//		WOOD
	public ParticleSystem 	m_psWoodBulletImpact;
	public ParticleSystem 	m_psWoodDecal;
	public AudioClip[]		m_acWoodBulletImpact;

//	STONE
	public ParticleSystem 	m_psStoneBulletImpact;
	public ParticleSystem 	m_psStoneDecal;
	public AudioClip[]		m_acStoneBulletImpact;
//	BRICK
	public ParticleSystem 	m_psBrickBulletImpact;
	public ParticleSystem 	m_psBrickDecal;
	public AudioClip[]		m_acBrickBulletImpact;
//	SAND
	public ParticleSystem 	m_psSandBulletImpact;
	public ParticleSystem 	m_psSandDecal;
	public AudioClip[]		m_acSandBulletImpact;
//	MUD
	public ParticleSystem 	m_psMudBulletImpact;
	public AudioClip[]		m_acMudBulletImpact;

	
	public void DoBulletImpact( GameObject hitObject, Vector3 vecPos, Vector3 vecNormal, Globals.matID matID )
	{
		Quaternion quatAngle = Quaternion.LookRotation( vecNormal );

		if ( matID == Globals.matID.CONCRETE )
		{
			Instantiate( m_psConcreteBulletImpact, vecPos, quatAngle );
			ParticleSystem decal = (ParticleSystem) Instantiate( m_psConcreteDecal, vecPos + vecNormal * 0.020f, quatAngle );
			decal.transform.parent = hitObject.transform;	// parent the decal to the object

			int iBulletHitSoundIndex = Mathf.Clamp( Random.Range ( 0, m_acConcreteBulletImpact.Length ),  0,  m_acConcreteBulletImpact.Length - 1 );
			PlayOneTime( m_acConcreteBulletImpact[iBulletHitSoundIndex], vecPos, 0.2f, Random.Range(0.75f, 1.0f ), flMinHitImpactSoundDist, flMaxHitImpactSoundDist );
		}
		else if ( matID == Globals.matID.DIRT )
		{
			Instantiate( m_psDirtBulletImpact, vecPos, quatAngle );
			ParticleSystem decal = (ParticleSystem) Instantiate( m_psDirtDecal, vecPos  + vecNormal * 0.020f, quatAngle );
			decal.transform.parent = hitObject.transform;	// parent the decal to the object

			int iBulletHitSoundIndex = Mathf.Clamp( Random.Range ( 0, m_acDirtBulletImpact.Length ),  0,  m_acDirtBulletImpact.Length - 1 );
			PlayOneTime( m_acDirtBulletImpact[iBulletHitSoundIndex], vecPos, 0.2f, Random.Range(0.75f, 1.0f ), flMinHitImpactSoundDist, flMaxHitImpactSoundDist );
		}
		else if ( matID == Globals.matID.FLESH )
		{
			Instantiate( m_psFleshBulletImpact, vecPos, quatAngle );
			ParticleSystem decal = (ParticleSystem) Instantiate( m_psFleshDecal, vecPos  + vecNormal * 0.020f, quatAngle );
			decal.transform.parent = hitObject.transform;	// parent the decal to the object

			int iBulletHitSoundIndex = Mathf.Clamp( Random.Range ( 0, m_acFleshBulletImpact.Length ),  0,  m_acFleshBulletImpact.Length - 1 );
			PlayOneTime( m_acFleshBulletImpact[iBulletHitSoundIndex], vecPos, 0.2f, Random.Range(0.75f, 1.0f ), flMinHitImpactSoundDist, flMaxHitImpactSoundDist );
		}
		else if ( matID == Globals.matID.FOLIAGE )
		{
			Instantiate( m_psFoliageBulletImpact, vecPos, quatAngle );
			int iBulletHitSoundIndex = Mathf.Clamp( Random.Range ( 0, m_acFoliageBulletImpact.Length ),  0,  m_acFoliageBulletImpact.Length - 1 );
			PlayOneTime( m_acFoliageBulletImpact[iBulletHitSoundIndex], vecPos, 0.2f, Random.Range(0.75f, 1.0f ), flMinHitImpactSoundDist, flMaxHitImpactSoundDist );
		}
		else if ( matID == Globals.matID.GLASS )
		{
			Instantiate( m_psGlassBulletImpact, vecPos, quatAngle );
			ParticleSystem decal = (ParticleSystem) Instantiate( m_psGlassDecal, vecPos  + vecNormal * 0.020f, quatAngle );
			decal.transform.parent = hitObject.transform;	// parent the decal to the object

			int iBulletHitSoundIndex = Mathf.Clamp( Random.Range ( 0, m_acGlassBulletImpact.Length ),  0,  m_acGlassBulletImpact.Length - 1 );
			PlayOneTime( m_acGlassBulletImpact[iBulletHitSoundIndex], vecPos, 0.2f, Random.Range(0.75f, 1.0f ), flMinHitImpactSoundDist, flMaxHitImpactSoundDist );
		}
		else if ( matID == Globals.matID.METAL )
		{
			Instantiate( m_psMetalBulletImpact, vecPos, quatAngle );
			ParticleSystem decal = (ParticleSystem) Instantiate( m_psMetalDecal, vecPos  + vecNormal * 0.020f, quatAngle );
			decal.transform.parent = hitObject.transform;	// parent the decal to the object

			int iBulletHitSoundIndex = Mathf.Clamp( Random.Range ( 0, m_acMetalBulletImpact.Length ),  0,  m_acMetalBulletImpact.Length - 1 );
			PlayOneTime( m_acMetalBulletImpact[iBulletHitSoundIndex], vecPos, 0.2f, Random.Range(0.75f, 1.0f ), flMinHitImpactSoundDist, flMaxHitImpactSoundDist );
		}
		else if ( matID == Globals.matID.PAPER )
		{
			Instantiate( m_psPaperBulletImpact, vecPos, quatAngle );
			ParticleSystem decal = (ParticleSystem) Instantiate( m_psPaperDecal, vecPos  + vecNormal * 0.020f, quatAngle );
			decal.transform.parent = hitObject.transform;	// parent the decal to the object

			int iBulletHitSoundIndex = Mathf.Clamp( Random.Range ( 0, m_acPaperBulletImpact.Length ),  0,  m_acPaperBulletImpact.Length - 1 );
			PlayOneTime( m_acPaperBulletImpact[iBulletHitSoundIndex], vecPos, 0.2f, Random.Range(0.75f, 1.0f ), flMinHitImpactSoundDist, flMaxHitImpactSoundDist );
		}
		else if ( matID == Globals.matID.PLASTER )
		{
			Instantiate( m_psPlasterBulletImpact, vecPos, quatAngle );
			ParticleSystem decal = (ParticleSystem) Instantiate( m_psPlasterDecal, vecPos  + vecNormal * 0.020f, quatAngle );
			decal.transform.parent = hitObject.transform;	// parent the decal to the object

			int iBulletHitSoundIndex = Mathf.Clamp( Random.Range ( 0, m_acPlasterBulletImpact.Length ),  0,  m_acPlasterBulletImpact.Length - 1 );
			PlayOneTime( m_acPlasterBulletImpact[iBulletHitSoundIndex], vecPos, 0.2f, Random.Range(0.75f, 1.0f ), flMinHitImpactSoundDist, flMaxHitImpactSoundDist );
		}
		else if ( matID == Globals.matID.PLASTIC )
		{
			Instantiate( m_psPlasticBulletImpact, vecPos, quatAngle );
			ParticleSystem decal = (ParticleSystem) Instantiate( m_psPlasticDecal, vecPos  + vecNormal * 0.020f, quatAngle );
			decal.transform.parent = hitObject.transform;	// parent the decal to the object

			int iBulletHitSoundIndex = Mathf.Clamp( Random.Range ( 0, m_acPlasticBulletImpact.Length ),  0,  m_acPlasticBulletImpact.Length - 1 );
			PlayOneTime( m_acPlasticBulletImpact[iBulletHitSoundIndex], vecPos, 0.2f, Random.Range(0.75f, 1.0f ), flMinHitImpactSoundDist, flMaxHitImpactSoundDist );
		}
		else if ( matID == Globals.matID.WATER )
		{
			Instantiate( m_psWaterBulletImpact, vecPos, quatAngle );
			
			int iBulletHitSoundIndex = Mathf.Clamp( Random.Range ( 0, m_acWaterBulletImpact.Length ),  0,  m_acWaterBulletImpact.Length - 1 );
			PlayOneTime( m_acWaterBulletImpact[iBulletHitSoundIndex], vecPos, 0.2f, Random.Range(0.75f, 1.0f ), flMinHitImpactSoundDist, flMaxHitImpactSoundDist );
		}
		else if ( matID == Globals.matID.WOOD )
		{
			Instantiate( m_psWoodBulletImpact, vecPos, quatAngle );
			ParticleSystem decal = (ParticleSystem) Instantiate( m_psWoodDecal, vecPos  + vecNormal * 0.020f, quatAngle );
			decal.transform.parent = hitObject.transform;	// parent the decal to the object

			int iBulletHitSoundIndex = Mathf.Clamp( Random.Range ( 0, m_acWoodBulletImpact.Length ),  0,  m_acWoodBulletImpact.Length - 1 );
			PlayOneTime( m_acWoodBulletImpact[iBulletHitSoundIndex], vecPos, 0.2f, Random.Range(0.75f, 1.0f ), flMinHitImpactSoundDist, flMaxHitImpactSoundDist  );
		}
		else if ( matID == Globals.matID.STONE )
		{
			Instantiate( m_psStoneBulletImpact, vecPos, quatAngle );
			ParticleSystem decal = (ParticleSystem) Instantiate( m_psStoneDecal, vecPos + vecNormal * 0.020f, quatAngle );
			decal.transform.parent = hitObject.transform;	// parent the decal to the object
			
			int iBulletHitSoundIndex = Mathf.Clamp( Random.Range ( 0, m_acStoneBulletImpact.Length ),  0,  m_acStoneBulletImpact.Length - 1 );
			PlayOneTime( m_acStoneBulletImpact[iBulletHitSoundIndex], vecPos, 0.2f, Random.Range(0.75f, 1.0f ), flMinHitImpactSoundDist, flMaxHitImpactSoundDist );
		}
		else if ( matID == Globals.matID.SAND )
		{
			Instantiate( m_psSandBulletImpact, vecPos, quatAngle );
			ParticleSystem decal = (ParticleSystem) Instantiate( m_psSandDecal, vecPos + vecNormal * 0.020f, quatAngle );
			decal.transform.parent = hitObject.transform;	// parent the decal to the object
			
			int iBulletHitSoundIndex = Mathf.Clamp( Random.Range ( 0, m_acSandBulletImpact.Length ),  0,  m_acSandBulletImpact.Length - 1 );
			PlayOneTime( m_acSandBulletImpact[iBulletHitSoundIndex], vecPos, 0.2f, Random.Range(0.75f, 1.0f ), flMinHitImpactSoundDist, flMaxHitImpactSoundDist );
		}
		else if ( matID == Globals.matID.BRICK )
		{
			Instantiate( m_psBrickBulletImpact, vecPos, quatAngle );
			ParticleSystem decal = (ParticleSystem) Instantiate( m_psBrickDecal, vecPos + vecNormal * 0.020f, quatAngle );
			decal.transform.parent = hitObject.transform;	// parent the decal to the object
			
			int iBulletHitSoundIndex = Mathf.Clamp( Random.Range ( 0, m_acBrickBulletImpact.Length ),  0,  m_acBrickBulletImpact.Length - 1 );
			PlayOneTime( m_acBrickBulletImpact[iBulletHitSoundIndex], vecPos, 0.2f, Random.Range(0.75f, 1.0f ), flMinHitImpactSoundDist, flMaxHitImpactSoundDist );
		}
		else if ( matID == Globals.matID.MUD )
		{
			Instantiate( m_psMudBulletImpact, vecPos, quatAngle );

			int iBulletHitSoundIndex = Mathf.Clamp( Random.Range ( 0, m_acMudBulletImpact.Length ),  0,  m_acMudBulletImpact.Length - 1 );
			PlayOneTime( m_acMudBulletImpact[iBulletHitSoundIndex], vecPos, 0.2f, Random.Range(0.75f, 1.0f ), flMinHitImpactSoundDist, flMaxHitImpactSoundDist );
		}
	}



	void PlayOneTime (AudioClip clip, Vector3 position, float volume, float pitch, float flMinDistance = -1.0f, float flMaxDistance = -1.0f )
	{
		if (clip == null)
			return;
		
		GameObject go = new GameObject ("One shot audio");
		go.transform.parent = transform;
		go.transform.position = position;
		AudioSource source = go.AddComponent <AudioSource>() as AudioSource;
		source.clip = clip;
		source.volume = volume;
		source.pitch = pitch;
		if ( flMaxDistance != -1.0f )
			source.maxDistance = flMaxDistance;
		if ( flMinDistance != -1.0f )
			source.minDistance = flMinDistance;
		
		source.Play ();
		Destroy (go, clip.length);
	}
}
