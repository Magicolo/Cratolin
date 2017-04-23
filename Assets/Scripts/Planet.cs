﻿using UnityEngine;

public class Planet : MonoBehaviour
{
	public static Planet Instance { get; private set; }
<<<<<<< 59f6acd47b9f12801809b0ebe07a1fd918da6cf1
	public float temperature;
	public float Temperature
	{
		get { return temperature; }
		set { temperature = Mathf.Clamp(value, -100f, 100); }
	}
	public float co2;
=======

	public Transform Root;

	public float Temperature { get; set; }
	private float co2;
>>>>>>> 00540da8672160875794c087341af6797b614a90
	public float CO2
	{
		get { return co2; }
		set { co2 = Mathf.Clamp(value, 0, 100); }
	}
	public float ozone;
	public float Ozone
	{
		get { return ozone; }
		set { ozone = Mathf.Clamp(value, 0, 100); }
	}

	public float PlanetPressure{
		get{return Mathf.Min((CO2 + Ozone)/76.852f, 1); }
	}

	void Awake()
	{
		Instance = this;
	}

	void FixedUpdate()
	{
		Temperature += Chronos.Instance.DeltaTime 
			* GameConstants.Instance.PlanetCooldownRate 
			* (GameConstants.Instance.PlanetPressureCooldownFactor * (1-PlanetPressure));

		if(CO2 >= 50){
			Temperature += Chronos.Instance.DeltaTime * GameConstants.Instance.PlanetSerreEffectHeatRate;
		}
	}

	public void Destroy()
	{
		gameObject.SetActive(false);
	}
}
