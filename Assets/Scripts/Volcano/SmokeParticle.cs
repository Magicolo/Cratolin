using UnityEngine;

public class SmokeParticle : ParticleBase
{
	SmokeEmitter smoke;

	public void Initialize(SmokeEmitter smoke, Vector2 position, Vector2 velocity, float friction = 0.999f, float lifeTime = 10f, float fadeOut = 3f)
	{
		base.Initialize(position, velocity, friction, lifeTime, fadeOut);

		this.smoke = smoke;
	}

	protected override void Despawn()
	{
		smoke.Despawn(this);
	}
}
