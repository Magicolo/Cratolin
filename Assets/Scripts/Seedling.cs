﻿using System.Linq;
using UnityEngine;

public class Seedling : MonoBehaviour
{
	public enum States { Spawning, Walking }

	public float ShakeAmplitude = 5f;
	public float SpawnSpeed = 5f;
	public float TargetY;
	public float DistanceToGround;
	public LayerMask Mask;
	public Transform Visual;
	public GameObject Fire;

	bool isGoingRight = true;
	public bool inFire;
	float lastTimeInFire;
	float stateTime;
	States state;

	public float CurrentSpeed
	{
		get
		{
			float speed = 20;

			//to compensate for planet rotation
			if (isGoingRight)
				speed *= 2;
			if (inFire)
				speed *= 3f;

			return speed;
		}
	}

	void Awake()
	{
		SwitchState(States.Spawning);
	}

	void OnEnable()
	{
		Groups.Add(this);
	}

	void OnDisable()
	{
		Groups.Remove(this);
	}

	void FixedUpdate()
	{
		stateTime += Chronos.Instance.DeltaTime;

		switch (state)
		{
			case States.Spawning: UpdateSpawning(); break;
			case States.Walking: UpdateWalking(); break;
		}
	}

	public void CatchFire()
	{
		Fire.SetActive(true);
		lastTimeInFire = Chronos.Instance.Time;
		inFire = true;
	}

	public void ClearFire()
	{
		inFire = false;
		Fire.SetActive(false);
	}

	void UpdateSpawning()
	{
		var position = Visual.localPosition;
		position.x = Random.Range(-ShakeAmplitude, ShakeAmplitude);
		position.y += SpawnSpeed * Chronos.Instance.DeltaTime;

		if (position.y >= TargetY)
			position = new Vector2(0, TargetY);

		Visual.localPosition = position;

		Visual.GetComponentInChildren<SpriteRenderer>().flipX = !isGoingRight;

		if (position.y >= TargetY)
			SwitchState(States.Walking);
	}

	void UpdateWalking()
	{
		const float duration = 6f;

		if (inFire && Chronos.Instance.Time - lastTimeInFire > 7f)
		{
			Destroy(gameObject);
			return;
		}

		if (inFire && Groups.Get<Sea>().Any(s => s.SeaCollider.bounds.Contains(transform.position)))
			ClearFire();

		// stick to ground
		RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 100000, Mask);
		if (hit.collider != null)
		{
			Collider2D col = hit.collider;
			transform.up = hit.normal;
			transform.position = hit.point + hit.normal * DistanceToGround;
		}

		// walk in current direction, left or right
		RaycastHit2D hitLeftRight = Physics2D.Raycast(transform.position + transform.up * DistanceToGround, transform.right, DistanceToGround * 2, Mask);
		bool canGoRight = (hitLeftRight.collider == null);
		hitLeftRight = Physics2D.Raycast(transform.position + transform.up * DistanceToGround, -transform.right, DistanceToGround * 2, Mask);
		bool canGoLeft = (hitLeftRight.collider == null);

		if (isGoingRight && canGoRight)
			transform.localPosition += transform.right * Chronos.Instance.DeltaTime * CurrentSpeed;
		else if (isGoingRight && !canGoRight)
			isGoingRight = false;
		else if (!isGoingRight && canGoLeft)
			transform.localPosition -= transform.right * Chronos.Instance.DeltaTime * CurrentSpeed;
		else if (!isGoingRight && !canGoLeft)
			isGoingRight = true;

		//Visual.GetComponentInChildren<SpriteRenderer>().flipX = !isGoingRight;

		Visual.transform.localScale = new Vector3((isGoingRight ? -1 : 1), 1, 1);

		if (stateTime > duration)
			SwitchState(States.Walking); // Changes direction maybe.
	}

	void SwitchState(States state)
	{
		this.state = state;
		stateTime = 0f;
		isGoingRight = Random.value > 0.5f;
	}
}
