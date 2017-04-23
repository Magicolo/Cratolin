using UnityEngine;
using Random = UnityEngine.Random;

public class LavaEmitter : ParticleEmitterBase<LavaParticle>
{
	public Vector2 SpawnDelay = new Vector2(0.25f, 1f);
	public Vector2 Burst = new Vector2(1f, 5f);

	float nextParticle;

	void OnEnable()
	{
		nextParticle = Chronos.Instance.Time;
	}

	void FixedUpdate()
	{
		while (Chronos.Instance.Time >= nextParticle)
		{
			var burst = (int)Random.Range(Burst.x, Burst.y);

			for (int i = 0; i < burst; i++)
			{
				nextParticle += Random.Range(SpawnDelay.x, SpawnDelay.y);
				Spawn();
			}
		}
	}

	public override LavaParticle Spawn()
	{
		var particle = base.Spawn();
		particle.Initialize(this, transform.position, new Vector2(Random.Range(-150f, 150f), Random.Range(50f, 150f)));

		return particle;
	}

	public override void Despawn(LavaParticle particle)
	{
		base.Despawn(particle);

		Planet.Instance.Temperature += Chronos.Instance.DeltaTime * GameConstants.Instance.LavaEmitterTemperature;
	}
}
