using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAbleObject : MonoBehaviour {

    public ParticleSystem smokeParticles;
    public GameObject[] fireAnimatedSprites;
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

        foreach (GameObject fire in fireAnimatedSprites)
            fire.SetActive(true);
    }

    public void StopFire()
    {
        if(inFire)
        {
            inFire = false;

            smokeParticles.Stop();

            foreach (GameObject fire in fireAnimatedSprites)
                fire.SetActive(false);
        }
        
    }
}
