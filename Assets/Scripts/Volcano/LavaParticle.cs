using UnityEngine;

public class LavaParticle : ParticleBase
{
	Lava lava;

	public void Initialize(Lava lava, Vector2 position, Vector2 velocity, float friction = 0.98f, float lifeTime = 5f, float fadeOut = 1f)
	{
		base.Initialize(position, velocity, friction, lifeTime, fadeOut);

		this.lava = lava;
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		velocity += Graviton.Instance.Gravity;
	}

	protected override void Despawn()
	{
		lava.Despawn(this);
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		Despawn();
	}
}
