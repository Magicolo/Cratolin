using UnityEngine;

public abstract class ParticleBase : MonoBehaviour
{
	public SpriteRenderer Renderer;
	public Rigidbody2D Body;

	protected Vector2 velocity;
	protected float friction;
	protected float lifeTime;
	protected float fadeOut;

	protected void Initialize(Vector2 position, Vector2 velocity, float friction, float lifeTime, float fadeOut)
	{
		transform.position = position;
		this.velocity = velocity;
		this.friction = friction;
		this.lifeTime = lifeTime;
		this.fadeOut = fadeOut;
	}

	protected virtual void FixedUpdate()
	{
		lifeTime -= Chronos.Instance.DeltaTime;
		Renderer.color = new Color(Renderer.color.r, Renderer.color.g, Renderer.color.b, Mathf.Clamp01(lifeTime / fadeOut));

		if (lifeTime > 0)
		{
			transform.localPosition += (Vector3)velocity * Chronos.Instance.DeltaTime;
			velocity *= friction;
		}
		else
			Despawn();
	}

	protected abstract void Despawn();
}
