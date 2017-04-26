using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetHeatShader : MonoBehaviour
{

	private SpriteRenderer sr;

	void Start () {
		sr = GetComponent<SpriteRenderer>();
	}

	void Update () {
		var heatFactor = 0f;
		if(Planet.Instance.Temperature >= 50)
			heatFactor = Mathf.Clamp((Planet.Instance.Temperature-50)/50,0,1);
		sr.material.SetFloat("_HeatFactor", heatFactor);
		var coldFactor = Mathf.Abs(Mathf.Clamp(Planet.Instance.Temperature/100,-1,0)) * 0.2f;
		sr.material.SetFloat("_ColdFactor", coldFactor);
	}

}