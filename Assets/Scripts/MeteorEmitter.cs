using UnityEngine;

public class MeteorEmitter : ParticleEmitterBase<MeteorParticle>
{
	public Vector2 Speed = new Vector2(1f, 2f);
	public float Distance = 1000f;
	public Vector2 SpawnDelay = new Vector2(1f, 8f);
	public Vector2 Burst = new Vector2(1f, 4f);

	float nextParticle;

	void FixedUpdate()
	{
		nextParticle = nextParticle == 0f ? Chronos.Instance.Time : nextParticle;

		if (Chronos.Instance.Time >= nextParticle)
		{
			var burst = (int)Random.Range(Burst.x, Burst.y);

			for (int i = 0; i < burst; i++)
				Spawn();

			nextParticle += Random.Range(SpawnDelay.x, SpawnDelay.y);
		}
	}

	public override MeteorParticle Spawn()
	{
		var particle = base.Spawn();
		var direction = (Planet.Instance.Root.position + (Vector3)Random.insideUnitCircle).normalized;
		var position = direction * Distance;
		var velocity = Quaternion.Euler(0f, 0f, Random.Range(-25f, 25f)) * -direction * Random.Range(Speed.x, Speed.y);

		particle.Initialize(this, position, velocity);

		return particle;
	}
}
