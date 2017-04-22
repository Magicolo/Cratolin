public class SmokeCloudEmitter : ParticleEmitterBase<SmokeCloudParticle>
{
	public float SpawnDelay = 8f;

	float nextCloud;

	void OnEnable()
	{
		nextCloud = Chronos.Instance.Time;
	}

	void FixedUpdate()
	{
		while (Chronos.Instance.Time > nextCloud)
		{
			nextCloud += SpawnDelay;
			Spawn();
		}
	}

	public override SmokeCloudParticle Spawn()
	{
		var cloud = base.Spawn();
		cloud.Initialize(this, transform.position, 1.5f);

		return cloud;
	}
}
