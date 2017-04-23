using UnityEngine;

public class LavaParticle : ParticleBase
{
	LavaEmitter lava;
	public SplatterComponent splater;

	public void Initialize(LavaEmitter lava, Vector3 position, Vector2 velocity, float friction = 0.98f, float lifeTime = 5f, float fadeOut = 1f)
	{
		base.Initialize(position, velocity, friction, lifeTime, 0f, fadeOut);

		this.lava = lava;
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		velocity += Graviton.Instance.Gravity;
	}

	protected override void Despawn()
	{
		lava.Despawn(this);

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
