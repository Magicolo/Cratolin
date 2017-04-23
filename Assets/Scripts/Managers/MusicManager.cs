using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<AudioSource>().pitch = Mathf.Clamp(Time.timeScale  * 0.2f, 1f, 100f);
	}
}
