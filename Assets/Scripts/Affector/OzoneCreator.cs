using UnityEngine;

public class OzoneCreator : MonoBehaviour
{

	void Start()
	{

	}

	void FixedUpdate()
	{
		if (Planet.Instance.CO2 > GameConstants.Instance.TreeCO2ConsumationRate)
		{
			Planet.Instance.Ozone += Chronos.Instance.DeltaTime * GameConstants.Instance.TreeOzoneCreationRate;
			Planet.Instance.CO2 -= Chronos.Instance.DeltaTime * GameConstants.Instance.TreeCO2ConsumationRate;
		}

	}

}