using UnityEngine;

public class SmokeParticle : ParticleBase
{
	SmokeEmitter smoke;

	public void Initialize(SmokeEmitter smoke, Vector3 position, Vector2 velocity, float friction = 0.995f, float lifeTime = 10f, float fadeIn = 1f, float fadeOut = 3f)
	{
		base.Initialize(position, velocity, friction, lifeTime, fadeIn, fadeOut);

		this.smoke = smoke;
	}

	protected override void Despawn()
	{
		smoke.Despawn(this);
	}
}
