using UnityEngine;

public class SmokeCloudParticle : ParticleBase
{
	SmokeCloudEmitter emitter;
	float moveSpeed;

	public void Initialize(SmokeCloudEmitter emitter, Vector3 position, float moveSpeed, float lifeTime = 75f, float fadeIn = 5f, float fadeOut = 4f)
	{
		base.Initialize(position, Vector2.zero, 1f, lifeTime, fadeIn, fadeOut);

		this.emitter = emitter;
		this.fadeIn = fadeIn;
		this.moveSpeed = moveSpeed;
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		if (hasFaded)
			transform.RotateAround(Planet.Instance.transform.position, Vector3.forward, moveSpeed * Chronos.Instance.DeltaTime);
	}

	protected override void Despawn()
	{
		emitter.Despawn(this);
	}
}
