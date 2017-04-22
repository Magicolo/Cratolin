public class SmokeCloudEmitter : ParticleEmitterBase<SmokeCloud>
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

	public override SmokeCloud Spawn()
	{
		var cloud = base.Spawn();
		cloud.Initialize(this, transform.position);

		return cloud;
	}
}
