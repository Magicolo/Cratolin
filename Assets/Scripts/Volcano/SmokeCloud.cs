using UnityEngine;

public class SmokeCloud : ParticleBase
{
	SmokeCloudEmitter emitter;
	float fadeIn;
	float moveSpeed;

	public void Initialize(SmokeCloudEmitter emitter, Vector2 position, float lifeTime = 20f, float fadeIn = 5f, float fadeOut = 4f, float moveSpeed = 1.5f)
	{
		base.Initialize(position, Vector2.zero, 1f, lifeTime, fadeOut);

		this.emitter = emitter;
		this.fadeIn = fadeIn;
		this.moveSpeed = moveSpeed;
	}

	void OnEnable()
	{
		var color = Renderer.color;
		color.a = 0f;
		Renderer.color = color;
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		if (Renderer.color.a < 1f)
		{
			var color = Renderer.color;
			color.a += Chronos.Instance.DeltaTime / fadeIn;
			Renderer.color = color;
		}
		else
			transform.RotateAround(Planet.Instance.transform.position, Vector3.forward, moveSpeed * Chronos.Instance.DeltaTime);
	}

	protected override void Despawn()
	{
		emitter.Despawn(this);
	}
}
