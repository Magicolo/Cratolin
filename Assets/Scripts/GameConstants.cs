using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameConstants : MonoBehaviour {

	public static GameConstants Instance { get; private set; }
	public float TreeOzoneCreationRate = 0.2f;
	public float TreeCO2ConsumationRate = 0.1f;
	public float SmokeParticleCO2Emission = 1 ;
	public float LavaEmitterTemperature = 1;
	public float PlanetCooldownRate = -1;

	public float PlanetSerreEffectHeatRate = 2;
	public float PlanetPressureCooldownFactor = 0.25f;
	
	
	public float TreeBurnTemperaturThreshold = 70f;


	void Awake()
	{
		Instance = this;
	}
}