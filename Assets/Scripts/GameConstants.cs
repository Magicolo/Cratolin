using System;
using UnityEngine;

[Serializable]
public class GameConstants : MonoBehaviour
{
	public static GameConstants Instance { get; private set; }

	public float IceMeteorCoolingAmount = 25f;
	public float RainDropCoolingFactor = 0.01f;
	public float TreeOzoneCreationRate = 0.2f;
	public float TreeCO2ConsumationRate = 0.1f;
	public float SmokeParticleCO2Emission = 1;
	public float LavaEmitterTemperature = 1;
	public float MeteorEmitterTemperature = 1;
	public float PlanetCooldownRate = -1;

	public float PlanetSerreEffectHeatRate = 2;
	public float PlanetPressureCooldownFactor = 5f;

	public float TreeBurnTemperatureThreshold = 70f;
	public float SeaDryoffTemperatureThreshold = 80f;
	public float SeaDryoffRate = -0.2f;

	public int FirePowerNBFirableObjectInFireNeeded = 20;

	public float LavaExtinguish = 1;


	void Awake()
	{
		Instance = this;
	}
}