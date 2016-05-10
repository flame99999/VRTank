using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIEvent : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void StartScene(string name)
	{
		SceneManager.LoadScene(name);
	}
}
