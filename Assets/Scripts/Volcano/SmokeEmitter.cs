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
		var xOffset = Random.Range(XOffsetRange.x, XOffsetRange.y);
		var yOffset = Random.Range(YOffsetRange.x, YOffsetRange.y);
		var p = transform.position + new Vector3(xOffset,yOffset);
		particle.Initialize(this, p, new Vector2(Random.Range(-15f, 15f), 10f)
			, lifeTime: LifeTime, fadeIn:FadeIn, fadeOut:FadeOut, fadeMaxA:FadeMaxA);
		Planet.Instance.CO2 += GameConstants.Instance.SmokeParticleCO2Emission;

		return particle;
	}
}



