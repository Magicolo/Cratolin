using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour {

	void Update () {
		if(Input.GetKey(KeyCode.T))
			Planet.Instance.Temperature += Time.deltaTime * 50;
		if(Input.GetKey(KeyCode.Y))
			Planet.Instance.Temperature -= Time.deltaTime * 50;
	}

}