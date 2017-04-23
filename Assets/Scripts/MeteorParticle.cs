using UnityEngine;

public class MeteorParticle : ParticleBase
{
	MeteorEmitter emitter;

	public void Initialize(MeteorEmitter emitter, Vector3 position, Vector2 velocity)
	{
		base.Initialize(position, velocity, 1f, 30f, 1f, 1f);

		this.emitter = emitter;
	}

	protected override void Despawn()
	{
		emitter.Despawn(this);
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		Destroy(gameObject);
	}
}
