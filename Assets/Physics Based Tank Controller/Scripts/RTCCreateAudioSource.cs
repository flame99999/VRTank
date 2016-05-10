//----------------------------------------------
//          Realistic Tank Controller
//
// Copyright © 2015 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using System.Collections;

public class RTCCreateAudioSource : MonoBehaviour {

	public static AudioSource NewAudioSource(GameObject go, string audioName, float minDistance, float volume, AudioClip audioClip, bool loop, bool playNow, bool destroyAfterFinished){
		
		GameObject audioSource = new GameObject(audioName);
		audioSource.transform.position = go.transform.position;
		audioSource.transform.rotation = go.transform.rotation;
		audioSource.transform.parent = go.transform;
		audioSource.AddComponent<AudioSource>();
		audioSource.GetComponent<AudioSource>().minDistance = minDistance;
		audioSource.GetComponent<AudioSource>().volume = volume;
		audioSource.GetComponent<AudioSource>().clip = audioClip;
		audioSource.GetComponent<AudioSource>().loop = loop;
		audioSource.GetComponent<AudioSource>().spatialBlend = 1f;
		
		if(playNow)
			audioSource.GetComponent<AudioSource>().Play();
		
		if(destroyAfterFinished){
			if(audioClip)
				Destroy(audioSource, audioClip.length);
			else
				Destroy(audioSource);
		}
		
		return audioSource.GetComponent<AudioSource>();
		
	}

}
