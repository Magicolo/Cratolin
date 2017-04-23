using UnityEngine;

public class Planet : MonoBehaviour
{
	public static Planet Instance { get; private set; }

	public PlanetSplosionEmitter EmitterWhenDestroyed;
	public AudioClip explosionPlanet;

	public Transform Root;

	public float temperature;
	public float Temperature
	{
		get { return temperature; }
		set { temperature = Mathf.Clamp(value, -100f, 100); }
	}

	public float co2;
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

	public float PlanetPressure
	{
		get { return Mathf.Min((CO2 + Ozone) / 76.852f, 1); }
	}

	void Awake()
	{
		Instance = this;
	}

	public void Cooldown(float v)
	{
		if (temperature - v > 0)
			temperature -= v;
	}

	void FixedUpdate()
	{
		Temperature += Chronos.Instance.DeltaTime * GameConstants.Instance.PlanetCooldownRate;
		Temperature += Chronos.Instance.DeltaTime * GameConstants.Instance.PlanetPressureCooldownFactor * PlanetPressure;

		if (CO2 >= 75f)
			Temperature += Chronos.Instance.DeltaTime * GameConstants.Instance.PlanetSerreEffectHeatRate;
	}

	public void Destroy()
	{
		SoundManager.Instance.PlaySound(explosionPlanet);

		PowerManager.Instance.TrySpawnPower(PowerManager.Powers.Volcano, transform.position);
		gameObject.SetActive(false);

		if (EmitterWhenDestroyed != null)
			Instantiate(EmitterWhenDestroyed, transform.position, transform.rotation);

	}
}
