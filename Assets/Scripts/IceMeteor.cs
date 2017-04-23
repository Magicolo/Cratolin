using UnityEngine;

public class IceMeteor : MonoBehaviour
{
	public enum States { Falling, Melting }

	public float Speed = 50f;
	public float MeltTime = 4f;
	public IceRainEmitter Rain;
	public SpriteRenderer NormalRenderer;
	public SpriteRenderer BurningRenderer;

	int cloudCollisionCount;
	float stateTime;
	float meltTime;
	States state;

	float MeltRatio { get { return Mathf.Clamp01(meltTime / MeltTime); } }
	float BurnRatio { get { return Mathf.Pow(MeltRatio, 0.5f); } }
	float SlowDownRatio { get { return 1f - BurnRatio; } }

	void FixedUpdate()
	{
		stateTime += Chronos.Instance.DeltaTime;

		var color = BurningRenderer.color;
		color.a = BurnRatio;
		BurningRenderer.color = color;
		transform.localScale = Vector3.one * (1f - MeltRatio);

		switch (state)
		{
			case States.Falling: UpdateFalling(); break;
			case States.Melting: UpdateMelting(); break;
		}
	}

	void UpdateFalling()
	{
		meltTime = Mathf.Max(meltTime - Chronos.Instance.DeltaTime, 0f);
		var direction = (Planet.Instance.Root.position - transform.position).normalized;
		transform.position += direction * Speed * SlowDownRatio;

		if (cloudCollisionCount > 0)
			SwitchState(States.Melting);
	}

	void UpdateMelting()
	{
		const float duration = 4f;

		meltTime += Chronos.Instance.DeltaTime;
		var direction = (Planet.Instance.Root.position - transform.position).normalized;
		transform.position += direction * Speed * SlowDownRatio;

		if (cloudCollisionCount <= 0)
			SwitchState(States.Falling);
	}

	void SwitchState(States state)
	{
		this.state = state;
		stateTime = 0f;
		Rain.gameObject.SetActive(state == States.Melting);
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponentInParent<SmokeCloudParticle>() != null)
			cloudCollisionCount++;
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.GetComponentInParent<SmokeCloudParticle>() != null)
			cloudCollisionCount--;
	}
}
