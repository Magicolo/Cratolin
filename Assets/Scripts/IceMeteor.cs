using System.Collections;
using UnityEngine;

public class IceMeteor : MonoBehaviour
{
	public enum States { Falling, Melting }

	public float Speed = 50f;
	public float MeltTime = 4f;
	public IceRainEmitter Rain;
	public IceMeteorEmitter Chunks;
	public SpriteRenderer NormalRenderer;
	public SpriteRenderer BurningRenderer;

	int cloudCollisionCount;
	float stateTime;
	float meltTime;
	States state;

	float MeltRatio { get { return Mathf.Clamp01(meltTime / MeltTime); } }
	float BurnRatio { get { return Mathf.Pow(MeltRatio, 0.25f); } }
	float SlowDownRatio { get { return 1f - Mathf.Pow(MeltRatio, 0.25f); } }

	void Awake()
	{
		SwitchState(States.Falling);
	}

	void FixedUpdate()
	{
		stateTime += Chronos.Instance.DeltaTime;

		var color = BurningRenderer.color;
		color.a = BurnRatio;
		BurningRenderer.color = color;
		NormalRenderer.transform.localScale = Vector3.one * (1f - MeltRatio);

		switch (state)
		{
			case States.Falling: UpdateFalling(); break;
			case States.Melting: UpdateMelting(); break;
		}
	}

	void UpdateFalling()
	{
		var direction = (Planet.Instance.Root.position - transform.position).normalized;
		transform.position += direction * Speed * SlowDownRatio;
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(Vector3.down, direction), Chronos.Instance.DeltaTime);

		if (cloudCollisionCount > 0)
			SwitchState(States.Melting);
	}

	void UpdateMelting()
	{
		const float duration = 4f;

		meltTime += Chronos.Instance.DeltaTime;
		var direction = (Planet.Instance.Root.position - transform.position).normalized;
		transform.position += direction * Speed * SlowDownRatio;
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(Vector3.down, direction), Chronos.Instance.DeltaTime);

		if (cloudCollisionCount <= 0)
			SwitchState(States.Falling);

		if (meltTime >= MeltTime)
		{
			PowerManager.Instance.TrySpawnPower(PowerManager.Powers.Rain, transform.position);
			Destroy(gameObject);
		}
	}

	void SwitchState(States state)
	{
		this.state = state;
		stateTime = 0f;
		Rain.gameObject.SetActive(state == States.Melting);
	}

	IEnumerator Break()
	{
		Chunks.transform.parent = null;
		Chunks.gameObject.SetActive(true);

		yield return null;

		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponentInParent<SmokeCloudParticle>() != null)
			cloudCollisionCount++;
		else if (collision.GetComponentInParent<Planet>() != null)
			StartCoroutine(Break());
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.GetComponentInParent<SmokeCloudParticle>() != null)
			cloudCollisionCount--;
	}
}
