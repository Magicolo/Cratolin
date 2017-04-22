using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatnetShatter : MonoBehaviour {
	
	public ParticleSystem ExplosionParticleSystem;

	public bool testDestroy;	
	void Start () {
		
	}
	
	void Update () {
		if(testDestroy){
			testDestroy = false;
			Shatter();
		}
	}
    public void Shatter()
    {
		ExplosionParticleSystem.Play();

        Destroy(this.gameObject);
    }

}