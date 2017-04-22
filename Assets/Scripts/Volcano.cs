using UnityEngine;

public class Volcano : MonoBehaviour
{
	public enum States { Spawning, Idle, Erupting, Extinguished }

	public SpriteRenderer Sprite;
	public float ShakeAmplitude = 5f;
	public float SpawnSpeed = 3f;

	States state;

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
		var position = Sprite.transform.localPosition;
		position.x = Random.Range(-ShakeAmplitude, ShakeAmplitude);
		position.y += SpawnSpeed * Chronos.Instance.DeltaTime;

		if (position.y >= 0f)
			position = Vector2.zero;

		Sprite.transform.localPosition = position;

		if (position.y >= 0f)
			state = States.Idle;

	}

	void UpdateIdle()
	{

	}

	void UpdateErupting()
	{

	}

	void UpdateExtinguished()
	{

	}
}
