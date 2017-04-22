using UnityEngine;

public class SmokeParticle : ParticleBase
{
	Smoke smoke;

	public void Initialize(Smoke smoke, Vector2 position, Vector2 velocity, float friction = 0.99f, float lifeTime = 10f, float fadeOut = 3f)
	{
		base.Initialize(position, velocity, friction, lifeTime, fadeOut);

		this.smoke = smoke;
	}

	protected override void Despawn()
	{
		smoke.Despawn(this);
	}
}
