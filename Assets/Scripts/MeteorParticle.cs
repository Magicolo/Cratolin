﻿using UnityEngine;

public class MeteorParticle : ParticleBase
{
	public SplatterComponent Splater;

	MeteorEmitter emitter;

	public void Initialize(MeteorEmitter emitter, Vector3 position, Vector2 velocity)
	{
		base.Initialize(position, velocity, 1f, 30f, 0.1f, 1f);

		this.emitter = emitter;
	}

	protected override void Despawn()
	{
		if (Splater != null)
		{	
			var splat = Instantiate(Splater, transform.position, transform.rotation);
			splat.transform.parent = Planet.Instance.Root;
		}

		emitter.Despawn(this);
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (Splater != null)
		{	
			var splat = Instantiate(Splater, transform.position, transform.rotation);
			splat.transform.parent = Planet.Instance.Root;
		}

		Destroy(gameObject);
	}
}
