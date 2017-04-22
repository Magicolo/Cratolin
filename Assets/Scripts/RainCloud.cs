using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainCloud : MonoBehaviour {

    public float distanceToGround;
    public GameObject rainDropPrefab;
    public float rainZoneWidth;

    private float lastTimeSpawn;

	// Use this for initialization
	void Start () {
        lastTimeSpawn = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
		if(Time.time - lastTimeSpawn > 1)
        {
            lastTimeSpawn = Time.time;
            GameObject obj = Instantiate(rainDropPrefab);
            obj.transform.position = transform.position;
            obj.transform.up = transform.up;

            obj.transform.position += transform.right * Random.Range(-rainZoneWidth * 0.5f, rainZoneWidth * 0.5f);
        }
	}
}
