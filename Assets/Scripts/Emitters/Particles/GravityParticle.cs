using UnityEngine;

public class GravityParticle : ParticleBase
{
	ParticleEmitterBase<GravityParticle> emitter;
	public SplatterComponent splater;
	public float GravityFactor = 1;
	public bool orianteVersSpeed;

	public void Initialize(ParticleEmitterBase<GravityParticle> emitter, Vector3 position, Vector2 velocity, float friction = 0.98f, float lifeTime = 5f, float fadeOut = 1f)
	{
		this.emitter = emitter;
		base.Initialize(position, velocity, friction, lifeTime, 0f, fadeOut);
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		velocity += Graviton.Instance.Gravity * GravityFactor;
		if(orianteVersSpeed){
			var diff = transform.position - new Vector3(velocity.x, velocity.y,0);
			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
         	transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
		}
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
