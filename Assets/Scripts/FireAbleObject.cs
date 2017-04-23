using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAbleObject : MonoBehaviour {

    public ParticleSystem smokeParticles;
    public GameObject[] fireAnimatedSprites;
    public float timeToBurn;

    private bool inFire;
    private float timeInFire;
    
    public bool IsOnFire { get { return inFire; } }


    private float internalTemperature;
    void Start () {
		internalTemperature = 0;
	}
	
	void Update () {

        internalTemperature = Mathf.Lerp(internalTemperature, Planet.Instance.Temperature,Chronos.Instance.DeltaTime);
        if(internalTemperature >= GameConstants.Instance.TreeBurnTemperaturThreshold){
            StartFire();
        }

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
        if(!inFire)
        {
            inFire = true;
            timeInFire = 0;

            smokeParticles.Play();

            foreach (GameObject fire in fireAnimatedSprites)
                fire.SetActive(true);
        }
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
