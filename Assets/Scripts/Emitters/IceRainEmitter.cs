using UnityEngine;

public class IceRainEmitter : ParticleEmitterBase<IceRainParticle>
{
	public Vector2 SpawnDelay = new Vector2(0.1f, 0.1f);

	float nextParticle;

	void OnEnable()
	{
		nextParticle = Chronos.Instance.Time;
	}

	void FixedUpdate()
	{
		while (Chronos.Instance.Time >= nextParticle)
		{
			nextParticle += Random.Range(SpawnDelay.x, SpawnDelay.y);
			Spawn();
		}
	}

	public override IceRainParticle Spawn()
	{
		var particle = base.Spawn();
		particle.Initialize(this, transform.position + Vector3.right * Random.Range(-25f, 25f), new Vector2(0f, -50f));

		return particle;
	}
}
