using UnityEngine;

public abstract class ParticleBase : MonoBehaviour
{
	public SpriteRenderer Renderer;

	protected Vector2 velocity;
	protected float friction;
	protected float lifeTime;
	protected float fadeIn;
	protected float fadeOut;
	protected float fadeMaxA;
	protected bool hasFaded;
	protected bool hasInitialized;

	protected void Initialize(Vector3 position, Vector2 velocity, float friction, float lifeTime, float fadeIn, float fadeOut, float fadeMaxA = 1)
	{
		transform.position = position;
		transform.localEulerAngles = Vector3.zero;
		this.velocity = velocity;
		this.friction = friction;
		this.lifeTime = lifeTime;
		this.fadeIn = fadeIn;
		this.fadeOut = fadeOut;
		this.fadeMaxA = fadeMaxA;
		hasFaded = false;

		if (fadeIn > 0)
		{
			var color = Renderer.color;
			color.a = 0f;
			Renderer.color = color;
		}
		hasInitialized = true;
	}

	protected virtual void FixedUpdate()
	{
		if(!hasInitialized) return;
		lifeTime -= Chronos.Instance.DeltaTime;

		if (!hasFaded && fadeIn > 0f)
		{
			var color = Renderer.color;
			color.a += Chronos.Instance.DeltaTime / fadeOut;
			Renderer.color = color;
			hasFaded |= color.a >= fadeMaxA;
		}
		else if (lifeTime <= fadeOut && fadeOut > 0f)
		{
			var color = Renderer.color;
			color.a -= Chronos.Instance.DeltaTime / fadeOut;
			Renderer.color = color;
		}

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
