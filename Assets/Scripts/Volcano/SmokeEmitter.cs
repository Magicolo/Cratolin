using UnityEngine;

public class SmokeEmitter : ParticleEmitterBase<SmokeParticle>
{
	public Vector2 SpawnDelay = new Vector2(0.25f, 1f);
	public float LifeTime = 10f;
	public float FadeIn = 1f;
	public float FadeOut = 3f;
	public float FadeMaxA = 1f;
	public Vector2 XOffsetRange = Vector2.zero;
	public Vector2 YOffsetRange = Vector2.zero;
	public Vector2 VelocityX = new Vector2(-15f, 15f);
	public Vector2 VelocityY = new Vector2(10f, 10f);

	public int MaxSpawn = -1;
	int spawned = 0;

	float nextParticle;

	void OnEnable()
	{
		nextParticle = Chronos.Instance.Time;

	}

	void FixedUpdate()
	{
		if (Chronos.Instance.Time >= nextParticle)
		{
			if (MaxSpawn != -1 && spawned++ > MaxSpawn)
			{
				nextParticle += 1000;
				return;
			}

			nextParticle += Random.Range(SpawnDelay.x, SpawnDelay.y);
			Spawn();

		}
	}

	public override SmokeParticle Spawn()
	{
		var particle = base.Spawn();
		var xOffset = Random.Range(XOffsetRange.x, XOffsetRange.y);
		var yOffset = Random.Range(YOffsetRange.x, YOffsetRange.y);
		var position = transform.position + new Vector3(xOffset, yOffset);

		particle.Initialize(this, position,
			new Vector2(Random.Range(VelocityX.x, VelocityX.y), Random.Range(VelocityY.x, VelocityY.y)), lifeTime: LifeTime, fadeIn: FadeIn, fadeOut: FadeOut, fadeMaxA: FadeMaxA);
		Planet.Instance.CO2 += GameConstants.Instance.SmokeParticleCO2Emission;

		return particle;
	}
}



