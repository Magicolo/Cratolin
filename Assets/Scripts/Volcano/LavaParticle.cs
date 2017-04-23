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
		if(splater != null){
			var go = GameObject.Instantiate(splater,transform.position, transform.rotation);
			go.transform.parent = GameObject.FindGameObjectWithTag("Root").transform;
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		Despawn();
	}
}
