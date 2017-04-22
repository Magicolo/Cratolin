using UnityEngine;

public class Smoke : ParticleEmitter<SmokeParticle>
{
	public Vector2 SpawnDelay = new Vector2(0.25f, 1f);

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

	public override SmokeParticle Spawn()
	{
		var particle = base.Spawn();
		particle.Initialize(this, transform.position, new Vector2(Random.Range(-25f, 25f), 25f));

		return particle;
	}
}
