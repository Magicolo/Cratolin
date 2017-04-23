using UnityEngine;

public class MeteorRockSplosionEmitter : BurstEmitter<GravityParticle>
{

	public override GravityParticle Spawn()
	{
		var particle = base.Spawn();
		particle.Initialize(this, transform.position, new Vector2(Random.Range(-150f, 150f), Random.Range(50f, 150f)), lifeTime: 3f);

		return particle;
	}

	public override void Despawn(GravityParticle particle)
	{
		base.Despawn(particle);

		//Planet.Instance.Temperature += GameConstants.Instance.LavaEmitterTemperature;
	}
}