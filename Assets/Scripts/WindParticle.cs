using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindParticle : MonoBehaviour {

    public float distanceFormPlanetCenter;
    public float moveSpeed;

    private float direction = 1;
    private float lifeTimer;
    private float waveAmplitude;

    public float Direction { get { return direction; } set { direction = value; } }

	// Use this for initialization
	void Start () {
        lifeTimer = Random.Range(0f, 10f);
        waveAmplitude = Random.Range(0f, 25f);

        GetComponent<TrailRenderer>().sortingOrder = -5;
        GetComponent<TrailRenderer>().Clear();

        // Not all particles have trails
        if (Random.Range(0, 4) > 0)
            Destroy(GetComponent<TrailRenderer>());
    }
	
	// Update is called once per frame
	void Update () {

        lifeTimer += Time.deltaTime;

        transform.up = transform.position.normalized;
        transform.position = transform.up * (distanceFormPlanetCenter + Mathf.Sin(lifeTimer * 4) * waveAmplitude);

        transform.position += transform.right * Time.deltaTime * moveSpeed * Direction;

    }
}
