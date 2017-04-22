using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelPowerItem : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.eulerAngles = Vector3.zero;
	}

    public void Spawn(Vector3 pPosition)
    {
        PowerBase basePower = GetComponent<PowerBase>();

        if(basePower != null)
            basePower.Create(pPosition);
    }
}
