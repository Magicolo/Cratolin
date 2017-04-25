using System.Collections.Generic;
using UnityEngine;

public class IceRainParticle : ParticleBase
{
	public LayerMask PlanetLayer;
	IceRainEmitter emitter;

	public void Initialize(IceRainEmitter emitter, Vector3 position, Vector2 velocity)
	{
		base.Initialize(position, velocity, 1f, 5f, 0.1f, 0.5f);

		this.emitter = emitter;
	}

	protected override void Despawn()
	{
		Planet.Instance.Temperature -= GameConstants.Instance.RainDropCoolingFactor;
		emitter.Despawn(this);
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.GetComponent<Sea>() != null)
			collision.gameObject.GetComponent<Sea>().IncreaseWater();

		if (collision.gameObject.GetComponent<Walker>() != null)
			collision.gameObject.GetComponent<Walker>().ClearFire();

		if (collision.gameObject.GetComponent<Seedling>() != null)
			collision.gameObject.GetComponent<Seedling>().ClearFire();

		if (collision.gameObject.GetComponentInParent<FireAbleObject>() != null)
		{
			collision.gameObject.GetComponentInParent<FireAbleObject>().StopFire();
			Planet.Instance.Cooldown(1f);
		}

		if (collision.gameObject.GetComponentInParent<Volcano>() != null)
			collision.gameObject.GetComponentInParent<Volcano>().Heat--;

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
