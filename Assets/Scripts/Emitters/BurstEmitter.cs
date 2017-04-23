using UnityEngine;
using Random = UnityEngine.Random;

public abstract class BurstEmitter<T> : ParticleEmitterBase<T> where T : ParticleBase
{
	public Vector2 Burst = new Vector2(1f, 5f);

	public Vector2 StartDelay = new Vector2(0.25f, 1f);
	public bool Once;
	public bool DestroyWhenDone;
	private bool doneOnce;
	public Vector2 Cooldown = new Vector2(0.25f, 1f);

	float nextParticle;

	void OnEnable()
	{
		nextParticle = Chronos.Instance.Time + RandomUtils.RangeFloat(StartDelay);
		doneOnce = false;
	}

	void FixedUpdate()
	{
		if (Once && doneOnce)
		{
			if (ParticleCount == 0 && DestroyWhenDone)
				Destroy(gameObject);

			return;
		}


		while (Chronos.Instance.Time >= nextParticle)
		{
			var burst = (int)Random.Range(Burst.x, Burst.y);

			for (int i = 0; i < burst; i++)
			{
				nextParticle += RandomUtils.RangeFloat(Cooldown);
				Spawn();
			}

			doneOnce = true;

			if (Once) break;
		}
	}
}
