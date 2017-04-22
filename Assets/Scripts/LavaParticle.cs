using UnityEngine;

public class LavaParticle : MonoBehaviour
{
	Lava lava;
	Vector2 velocity;
	float friction;

	public void Initialize(Lava lava, Vector2 position, Vector2 velocity, float friction)
	{
		this.lava = lava;
		transform.position = position;
		this.velocity = velocity;
		this.friction = friction;
	}

	void FixedUpdate()
	{
		transform.localPosition += (Vector3)velocity * Chronos.Instance.DeltaTime;
		velocity += Graviton.Instance.Gravity;
		velocity *= friction;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		lava.Despawn(this);
	}
}
