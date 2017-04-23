using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OzoneCreator : MonoBehaviour {

	void Start () {

	}

	void Update () {
		if(Planet.Instance.CO2 > GameConstants.Instance.TreeCO2ConsumationRate){
			Planet.Instance.Ozone += GameConstants.Instance.TreeOzoneCreationRate;
			Planet.Instance.CO2 -= GameConstants.Instance.TreeCO2ConsumationRate;
		}
		
	}

}