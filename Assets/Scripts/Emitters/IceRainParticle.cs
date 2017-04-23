using UnityEngine;

public class IceRainParticle : ParticleBase
{
	IceRainEmitter emitter;

	public void Initialize(IceRainEmitter emitter, Vector3 position, Vector2 velocity)
	{
		base.Initialize(position, velocity, 1f, 5f, 0.1f, 0.5f);

		this.emitter = emitter;
	}

	protected override void Despawn()
	{
		emitter.Despawn(this);
	}
}
