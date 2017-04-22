using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainDrop : MonoBehaviour {

    public LayerMask PlanetLayer;
    public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position -= transform.up * Time.deltaTime * speed;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Sea>() != null)
            collision.gameObject.GetComponent<Sea>().IncreaseWater();

        if (PlanetLayer == (PlanetLayer | (1 << collision.gameObject.layer)))
        {
            Destroy(gameObject);
        }
    }
}
