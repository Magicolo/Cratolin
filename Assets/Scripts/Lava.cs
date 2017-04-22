using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Lava : MonoBehaviour
{
	public LavaParticle Prefab;
	public Vector2 SpawnDelay = new Vector2(0.25f, 1f);
	public Vector2 Burst = new Vector2(1f, 5f);

	float nextParticle;
	readonly Stack<LavaParticle> pool = new Stack<LavaParticle>();

	void OnEnable()
	{
		nextParticle = Chronos.Instance.CurrentTime;
	}

	void FixedUpdate()
	{
		while (Chronos.Instance.CurrentTime >= nextParticle)
		{
			var burst = (int)Random.Range(Burst.x, Burst.y);

			for (int i = 0; i < burst; i++)
			{
				nextParticle += Random.Range(SpawnDelay.x, SpawnDelay.y);
				Spawn();
			}
		}
	}

	public LavaParticle Spawn()
	{
		var particle = pool.Count > 0 ? pool.Pop() : Instantiate(Prefab, transform);
		particle.gameObject.SetActive(true);
		particle.Initialize(this, transform.position, new Vector2(Random.Range(-50f, 50f), Random.Range(25f, 100f)), 0.98f);

		return particle;
	}

	public void Despawn(LavaParticle particle)
	{
		particle.gameObject.SetActive(false);
		pool.Push(particle);
	}
}
