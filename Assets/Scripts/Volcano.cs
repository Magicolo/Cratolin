﻿using UnityEngine;

public class Volcano : MonoBehaviour
{
	public enum States { Spawning, Idle, Erupting, Extinguished }

	public Lava Lava;
	public SpriteRenderer Sprite;
	public float ShakeAmplitude = 5f;
	public float SpawnSpeed = 3f;

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
		var position = Sprite.transform.localPosition;
		position.x = Random.Range(-ShakeAmplitude, ShakeAmplitude);
		position.y += SpawnSpeed * Chronos.Instance.DeltaTime;

		if (position.y >= 0f)
			position = Vector2.zero;

		Sprite.transform.localPosition = position;

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
		Lava.gameObject.SetActive(state == States.Erupting);
	}
}
