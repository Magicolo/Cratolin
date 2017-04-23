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

		if (collision.gameObject.GetComponent<Walker>() != null)
			collision.gameObject.GetComponent<Walker>().ClearFire();

		if (collision.gameObject.GetComponentInParent<FireAbleObject>() != null)
			collision.gameObject.GetComponentInParent<FireAbleObject>().StopFire();

		if (collision.gameObject.GetComponentInParent<Volcano>() != null)
			collision.gameObject.GetComponentInParent<Volcano>().heat--;

		var splatterC = collision.gameObject.GetComponentInParent<SplatterElementComponent>();
		if (splatterC != null && splatterC.RainKillsMe)
			splatterC.DIE();
		
        RemoveLava();

		if (PlanetLayer == (PlanetLayer | (1 << collision.gameObject.layer)))
		{
			Destroy(gameObject);
		}
	}

	private void RemoveLava()
	{
		List<SplatterComponent> Todie = new List<SplatterComponent>();
		foreach (var lava in SplatterComponent.Splatters["Lava"])
		{
			var distance = Mathf.Abs((lava.transform.position - transform.position).magnitude);
			if (distance < 0.4 * lava.radiusEffect)
                Todie.Add(lava);
		}

		while (Todie.Count != 0)
		{
			var removeMe = Todie[0];
			Todie.RemoveAt(0);
			removeMe.DIE();
		}
	}
}
