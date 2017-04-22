using UnityEngine;

public class Planet : MonoBehaviour
{
	public static Planet Instance { get; private set; }

	public float Temperature { get; set; }
	public float CO2 { get; set; }
	public float Ozone { get; set; }

	void Awake()
	{
		Instance = this;
	}

	void FixedUpdate()
	{
		Temperature = Mathf.Max(Temperature - Chronos.Instance.DeltaTime, -100f);
	}
}
