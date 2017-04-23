using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour {


	public bool active = false;

	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			active = !active;
			FindObjectOfType<CheatModImage>().SetVisibility(active);
		}

		if(!active) return;


		if(Input.GetKey(KeyCode.T))
			Planet.Instance.Temperature += Time.deltaTime * 50;
		if(Input.GetKey(KeyCode.Y))
			Planet.Instance.Temperature -= Time.deltaTime * 50;

		if(Input.GetKey(KeyCode.Alpha1))
			Chronos.Instance.TimeScale = 1;
		if(Input.GetKey(KeyCode.Alpha2))
			Chronos.Instance.TimeScale = 2;
		if(Input.GetKey(KeyCode.Alpha3))
			Chronos.Instance.TimeScale = 4;
		if(Input.GetKey(KeyCode.Alpha4))
			Chronos.Instance.TimeScale = 8;

		if(Input.GetKey(KeyCode.O))
			foreach (var item in FindObjectsOfType<Sea>())
				item.IncreaseWater();
		if(Input.GetKey(KeyCode.P))
			foreach (var item in FindObjectsOfType<Sea>())
				item.ReduceWater();
	}

}