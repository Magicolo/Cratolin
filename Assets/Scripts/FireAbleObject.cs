using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAbleObject : MonoBehaviour {

    public ParticleSystem smokeParticles;
    public float timeToBurn;

    private bool inFire;
    private float timeInFire;
    

	void Start () {
		
	}
	
	void Update () {
		if(inFire)
        {
            timeInFire += Time.deltaTime;
            if(timeInFire > timeToBurn)
            {
                Destroy(gameObject);
            }
        }
	}

    public void StartFire()
    {
        inFire = true;
        timeInFire = 0;

        smokeParticles.Play();
    }

    public void StopFire()
    {
        if(inFire)
        {
            inFire = false;

            smokeParticles.Stop();
        }
        
    }
}
