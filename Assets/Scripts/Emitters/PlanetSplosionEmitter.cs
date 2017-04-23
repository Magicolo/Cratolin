using UnityEngine;

public class PlanetSplosionEmitter : BurstEmitter<GravityParticle>
{
	public override GravityParticle Spawn()
	{
		var particle = base.Spawn();
		var speed = 100f;
		particle.Initialize(this, transform.position, new Vector2(Random.Range(-speed, speed), Random.Range(-speed, speed))
		, lifeTime: 15f, friction: 1f);

		return particle;
	}
}