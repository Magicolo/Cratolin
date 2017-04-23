using UnityEngine;

public class Invader : MonoBehaviour
{
	public enum States { Arriving, Charging, Beaming, Waiting, Leaving }

	public float MoveSpeed = 2f;
	public float StopDistance = 200f;
	public Sprite Normal;
	public Sprite Charged;
	public SpriteRenderer Renderer;
	public LineRenderer Beam;

	States state;
	float stateTime;
	Vector3 startPosition;
	Vector3 cameraPosition;

	void Awake()
	{
		startPosition = transform.position;
		cameraPosition = Camera.main.transform.position;

		SwitchState(States.Arriving);
	}

	void FixedUpdate()
	{
		stateTime += Chronos.Instance.DeltaTime;
		switch (state)
		{
			case States.Arriving: UpdateArriving(); break;
			case States.Charging: UpdateCharging(); break;
			case States.Beaming: UpdateBeaming(); break;
			case States.Waiting: UpdateWaiting(); break;
			case States.Leaving: UpdateLeaving(); break;
		}
	}

	void UpdateArriving()
	{
		var difference = Planet.Instance.Root.position - transform.position;
		var distance = difference.magnitude;

		if (distance <= StopDistance)
			SwitchState(States.Charging);
		else
			transform.position += (difference / distance) * MoveSpeed * Chronos.Instance.DeltaTime;
	}

	void UpdateCharging()
	{
		Shake(stateTime);

		if (stateTime > 4f)
		{
			Beam.SetPositions(new Vector3[] { (Vector2)transform.position, (Vector2)Planet.Instance.Root.position });
			SwitchState(States.Beaming);
		}
	}

	void UpdateBeaming()
	{
		const float duration = 6f;
		Shake(stateTime * duration + 4f);

		if (stateTime > duration)
		{
			WhiteScreen.Instance.Fade(1f);
			Planet.Instance.Destroy();
			SwitchState(States.Waiting);
		}
		else
			WhiteScreen.Instance.Fade(stateTime / duration);
	}

	void UpdateWaiting()
	{
		const float duration = 8f;

		if (stateTime > duration)
		{
			WhiteScreen.Instance.Fade(0f);
			SwitchState(States.Leaving);
		}
		else if (stateTime > duration / 2f)
			WhiteScreen.Instance.Fade(1f - (stateTime - duration / 2f) / (duration / 2f));
	}

	void UpdateLeaving()
	{
		var direction = (startPosition - transform.position).normalized;
		transform.position += direction * MoveSpeed * Chronos.Instance.DeltaTime;
	}

	void Shake(float amplitude)
	{
		Camera.main.transform.position = cameraPosition + new Vector3(Random.Range(-amplitude, amplitude), Random.Range(-amplitude, amplitude));
	}

	void SwitchState(States state)
	{
		this.state = state;

		stateTime = 0f;
		Camera.main.transform.position = cameraPosition;
		Beam.enabled = state == States.Beaming;
	}
}
