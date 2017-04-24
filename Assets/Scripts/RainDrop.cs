using System.Collections.Generic;
using UnityEngine;

public class RainDrop : MonoBehaviour
{
	public LayerMask PlanetLayer;
	public float speed;

	void FixedUpdate()
	{
		transform.position -= transform.up * Chronos.Instance.DeltaTime * speed;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.GetComponent<Sea>() != null)
			collision.gameObject.GetComponent<Sea>().IncreaseWater();

		if (collision.gameObject.GetComponent<Walker>() != null)
			collision.gameObject.GetComponent<Walker>().ClearFire();

		if (collision.gameObject.GetComponentInParent<FireAbleObject>() != null)
		{
			collision.gameObject.GetComponentInParent<FireAbleObject>().StopFire();
			Planet.Instance.Cooldown(1f);
		}

		if (collision.gameObject.GetComponentInParent<Volcano>() != null)
			collision.gameObject.GetComponentInParent<Volcano>().heat--;

		var splatterC = collision.gameObject.GetComponentInParent<SplatterElementComponent>();
		if (splatterC != null && splatterC.RainKillsMe)
			splatterC.DIE();

		RemoveLava();

		if (PlanetLayer == (PlanetLayer | (1 << collision.gameObject.layer)))
		{
			Planet.Instance.Temperature -= GameConstants.Instance.RainDropCoolingFactor;
			Destroy(gameObject);
		}
	}

	private void RemoveLava()
	{
		var Todie = new List<SplatterComponent>();
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
