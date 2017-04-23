﻿using UnityEngine;

public class Volcano : MonoBehaviour
{
	public enum States { Spawning, Idle, Erupting, Extinguished }

	public float ShakeAmplitude = 5f;
	public float SpawnSpeed = 3f;
	public LavaEmitter Lava;
	public SmokeEmitter Smoke;
	public SmokeCloudEmitter Cloud;
	public SmokeEmitter ExtinguishedEmitterPrefab;
	public SpriteRenderer Renderer;
	public Sprite Erupting;
	public Sprite Extinguished;
	public Transform VolcanoExit;

	float stateTime;
	States state;

	public int heat = 10;

	void Awake()
	{
		SwitchState(States.Spawning);
	}

	void FixedUpdate()
	{
		stateTime += Chronos.Instance.DeltaTime;
		

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
		if(heat <= 0){
			SwitchState(States.Extinguished);
		}
	}

	void UpdateErupting()
	{
		if(heat <= 0){
			SwitchState(States.Extinguished);
		}
	}

	void UpdateExtinguished()
	{

	}

	void SwitchState(States state)
	{
		this.state = state;
		this.stateTime = 0f;

		Renderer.sprite = state == States.Extinguished ? Extinguished : Erupting;
		Lava.enabled = (state == States.Erupting);
		Smoke.enabled = (state == States.Idle || state == States.Erupting);
		Cloud.enabled = (state == States.Idle || state == States.Erupting);

		if(state.Equals(States.Extinguished)){
			if (ExtinguishedEmitterPrefab != null)
			{
				var newGameObject = Instantiate(ExtinguishedEmitterPrefab, VolcanoExit.position, transform.rotation);
				newGameObject.transform.parent = Planet.Instance.Root;
				
			}

		}
	}
}
