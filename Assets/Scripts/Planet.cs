using UnityEngine;

public class Planet : MonoBehaviour
{
	public static Planet Instance { get; private set; }

	public float Temperature { get; set; }
	private float co2;
	public float CO2
	{
		get { return co2; }
		set { co2 = Mathf.Clamp(value, 0, 100); }
	}
	private float ozone;
	public float Ozone
	{
		get { return ozone; }
		set { ozone = Mathf.Clamp(value, 0, 100); }
	}

	void Awake()
	{
		Instance = this;
	}

	void FixedUpdate()
	{
		Temperature = Mathf.Max(Temperature - Chronos.Instance.DeltaTime, -100f);
	}

	public void Destroy()
	{
		gameObject.SetActive(false);
	}
}
