using UnityEngine;

public class Volcano : MonoBehaviour
{
	public enum States { Spawning, Idle, Erupting, Extinguished }

	public float ShakeAmplitude = 5f;
	public float SpawnSpeed = 3f;
	public Lava Lava;
	public Smoke Smoke;
	public SpriteRenderer Renderer;
	public Sprite Erupting;
	public Sprite Extinguished;

	States state;

	void Awake()
	{
		SwitchState(States.Spawning);
	}

	void FixedUpdate()
	{
		switch (state)
		{
			case States.Spawning: UpdateSpawning(); break;
			case States.Idle: UpdateIdle(); break;
			case States.Erupting: UpdateErupting(); break;
			case States.Extinguished: UpdateExtinguished(); break;
		}
	}

	void UpdateSpawning()
	{
		var position = Renderer.transform.localPosition;
		position.x = Random.Range(-ShakeAmplitude, ShakeAmplitude);
		position.y += SpawnSpeed * Chronos.Instance.DeltaTime;

		if (position.y >= 0f)
			position = Vector2.zero;

		Renderer.transform.localPosition = position;

		if (position.y >= 0f)
			SwitchState(States.Idle);

	}

	void UpdateIdle()
	{
		SwitchState(States.Erupting);
	}

	void UpdateErupting()
	{

	}

	void UpdateExtinguished()
	{

	}

	void SwitchState(States state)
	{
		this.state = state;

		Renderer.sprite = state == States.Extinguished ? Extinguished : Erupting;
		Lava.gameObject.SetActive(state == States.Erupting);
		Smoke.gameObject.SetActive(state == States.Idle || state == States.Erupting);
	}
}
