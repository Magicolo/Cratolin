using UnityEngine;

public class GravityParticle : ParticleBase
{
	ParticleEmitterBase<GravityParticle> emitter;
	public SplatterComponent splater;
	public float GravityFactor = 1;

	public void Initialize(ParticleEmitterBase<GravityParticle> emitter, Vector3 position, Vector2 velocity, float friction = 0.98f, float lifeTime = 5f, float fadeOut = 1f)
	{
		this.emitter = emitter;
		base.Initialize(position, velocity, friction, lifeTime, 0f, fadeOut);
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		velocity += Graviton.Instance.Gravity * GravityFactor;
	}

	protected override void Despawn()
	{
		emitter.Despawn(this);

		if (splater != null)
		{
			var splat = Instantiate(splater, transform.position, transform.rotation);
			splat.transform.parent = Planet.Instance.Root;
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		var splatterC = collision.gameObject.GetComponent<SplatterElementComponent>();
		if(splatterC != null && splatterC.FireKillsMe){
			splatterC.DIE();
			if (splater != null)
			{
				var splat = Instantiate(splater, transform.position, transform.rotation);
				splat.transform.parent = Planet.Instance.Root;
			}
		}
		
		Despawn();
	}
}
