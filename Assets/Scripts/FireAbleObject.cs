using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAbleObject : MonoBehaviour {

    public static int nbInFire;

    public static readonly List<FireAbleObject> FireAbles = new List<FireAbleObject>();


    public ParticleSystem smokeParticles;
    public GameObject[] fireAnimatedSprites;
    public float timeToBurn;

    private bool inFire;
    private float timeInFire;

    
    public bool IsOnFire { get { return inFire; } }


    public float internalTemperature;
    void Start () {
		internalTemperature = 0;
        FireAbles.Add(this);
	}

    void OnDisable()
    {
        FireAbles.Remove(this);
    }
	
	void Update () {

        internalTemperature = Mathf.Lerp(internalTemperature, Planet.Instance.Temperature,Chronos.Instance.DeltaTime);
        if(internalTemperature >= GameConstants.Instance.TreeBurnTemperatureThreshold){
            StartFire();
        }

		if(inFire)
        {
            timeInFire += Time.deltaTime;
            if(timeInFire > timeToBurn)
            {
                nbInFire--;
                Destroy(gameObject);
            }
        }
	}

    public void StartFire()
    {
        if(!inFire)
        {
            nbInFire++;
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
            nbInFire--;
            inFire = false;

            smokeParticles.Stop();

            foreach (GameObject fire in fireAnimatedSprites)
                fire.SetActive(false);
        }
        
    }
}
