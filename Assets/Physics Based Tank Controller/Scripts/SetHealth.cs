using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetHealth : MonoBehaviour {

    public Health tankHealth;
    public Slider healthBar;
	
	// Update is called once per frame
	void Update () {
        healthBar.value = tankHealth.health;
    }
}
