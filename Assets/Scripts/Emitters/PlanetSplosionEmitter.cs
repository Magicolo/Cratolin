using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSplosionEmitter : BurstEmitter<GravityParticle> {
	
	public override GravityParticle Spawn()
	{
		var particle = base.Spawn();
		particle.Initialize(this, transform.position, new Vector2(Random.Range(-360, 360), Random.Range(-360, 360)));

		return particle;
	}

	public override void Despawn(GravityParticle particle)
	{
		base.Despawn(particle);

	}
}