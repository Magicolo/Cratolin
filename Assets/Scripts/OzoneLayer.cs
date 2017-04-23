using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OzoneLayer : MonoBehaviour {

	public Color FullCO2;	
	public Color FullOzone;

	private SpriteRenderer sr;
	void Start () {
		sr = this.GetComponent<SpriteRenderer>();
	}

	void Update () {
		float t = (Planet.Instance.Ozone - Planet.Instance.CO2 ) / 100;
		float a = Planet.Instance.PlanetPressure;
		var c = Color.Lerp(FullCO2,FullOzone,t);
		sr.color = new Color(c.r, c.g, c.b, a);

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainMenu")
            sr.color = FullOzone;
	}

}