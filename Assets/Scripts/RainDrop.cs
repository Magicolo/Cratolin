using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainDrop : MonoBehaviour {

    public LayerMask PlanetLayer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position -= transform.up * Time.deltaTime;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(PlanetLayer == (PlanetLayer | (1 << collision.gameObject.layer)))
        {
            Destroy(gameObject);
        }
    }
}
