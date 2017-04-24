using System.Collections.Generic;
using UnityEngine;

public class FireAbleObject : MonoBehaviour
{

	public static int nbInFire;


	public ParticleSystem smokeParticles;
	public GameObject[] fireAnimatedSprites;
	public float timeToBurn;

	private bool inFire;
	private float timeInFire;


	public bool IsOnFire { get { return inFire; } }


	public float internalTemperature;
	void Start()
	{
		internalTemperature = 0;
	}

	void OnEnable()
	{
		Groups.Add(this);
	}
	void OnDisable()
	{
		Groups.Remove(this);
		if(inFire)
			nbInFire--;
	}

	void FixedUpdate()
	{

		internalTemperature = Mathf.Lerp(internalTemperature, Planet.Instance.Temperature, Chronos.Instance.DeltaTime);
		if (internalTemperature >= GameConstants.Instance.TreeBurnTemperatureThreshold)
		{
			StartFire();
		}

		if (inFire)
		{
			timeInFire += Chronos.Instance.DeltaTime;
			if (timeInFire > timeToBurn)
			{
				Destroy(gameObject);
			}
		}
	}

	public void StartFire()
	{
		if (!inFire)
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
		if (inFire)
		{
			nbInFire--;
			inFire = false;

			smokeParticles.Stop();

			foreach (GameObject fire in fireAnimatedSprites)
				fire.SetActive(false);
		}

	}
}
