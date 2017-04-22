using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelPowerItem : MonoBehaviour {

    public GameObject prefabToSpawn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.eulerAngles = Vector3.zero;
	}

    public void Spawn(Vector3 pPosition)
    {
        GameObject obj = Instantiate(prefabToSpawn);
        obj.transform.position = pPosition;
    }
}
