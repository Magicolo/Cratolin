﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Walker : MonoBehaviour
{
	public static readonly List<Walker> Walkers = new List<Walker>();

	public float distanceToGround;
	public LayerMask WalkerCollision;
	public Transform Visual;
	public GameObject walkBase;
	public GameObject walkEvolved;
	public GameObject scared;
	public GameObject idle;
	public GameObject lookAround;
	public GameObject fireAnim;

	private bool isGoingRight = true;
	private float timeSinceLastFear = float.MinValue;
	private float timeSpawn;
	private bool isWalking = true;

	private float lookAroundTimer = 1000;
	public bool inFire;
	private float lastTimeInFire;

	public bool IsEvolved { get { return Chronos.Instance.Time - timeSpawn > 20; } }
	public bool ReachedGoal { get; set; }

	// Use this for initialization
	void Start()
	{
		transform.parent = Planet.Instance.transform;
		timeSpawn = Chronos.Instance.Time;

		lookAroundTimer = Random.Range(0f, 5f);
	}

	void OnEnable()
	{
		Walkers.Add(this);
	}

	void OnDisable()
	{
		Walkers.Remove(this);
	}

	public void Fear(Vector3 pPosition)
	{
		if (!ReachedGoal)
		{
			timeSinceLastFear = Chronos.Instance.Time;

			bool fearLeftOf = (Vector2.Angle(transform.right, pPosition)) > 90 && (Vector2.Angle(transform.right, pPosition)) < 180;

			if (fearLeftOf)
				isGoingRight = false;
			else
				isGoingRight = true;
		}
	}

	public void CatchFire()
	{
		timeSinceLastFear = Chronos.Instance.Time;
		fireAnim.gameObject.SetActive(true);
		lastTimeInFire = Chronos.Instance.Time;
		inFire = true;
	}

	public void ClearFire()
	{
		inFire = false;
		fireAnim.gameObject.SetActive(false);
	}

	public float CurrentSpeed
	{
		get
		{
			float speed = 20;

			//to compensate for planet rotation
			if (isGoingRight)
				speed *= 2;

			if (Chronos.Instance.Time - timeSinceLastFear < 3 || inFire)
				speed *= 3;

			return speed;
		}
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		isWalking = !ReachedGoal && (!IsEvolved || Chronos.Instance.Time - timeSinceLastFear > 1f);


		if (inFire && Chronos.Instance.Time - lastTimeInFire > 7f)
		{
			Destroy(gameObject);
			return;
		}

		if (inFire && Groups.Get<Sea>().Any(s => s.SeaCollider.bounds.Contains(transform.position)))
			ClearFire();


		// stick to ground
		RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up * 100f, -transform.up, 100000, WalkerCollision);
		if (hit.collider != null)
		{
			Collider2D col = hit.collider;
			transform.up = hit.normal;
			transform.position = hit.point + hit.normal * distanceToGround;
		}

		// walk in current direction, left or right
		if (isWalking)
		{


			RaycastHit2D hitLeftRight = Physics2D.Raycast(transform.position + transform.up * distanceToGround, transform.right, distanceToGround * 2, WalkerCollision);
			bool canGoRight = (hitLeftRight.collider == null);
			hitLeftRight = Physics2D.Raycast(transform.position + transform.up * distanceToGround, -transform.right, distanceToGround * 2, WalkerCollision);
			bool canGoLeft = (hitLeftRight.collider == null);


			if (isGoingRight && canGoRight)
				transform.localPosition += transform.right * Chronos.Instance.DeltaTime * CurrentSpeed;
			else if (isGoingRight && !canGoRight)
				isGoingRight = false;
			else if (!isGoingRight && canGoLeft)
				transform.localPosition -= transform.right * Chronos.Instance.DeltaTime * CurrentSpeed;
			else if (!isGoingRight && !canGoLeft)
				isGoingRight = true;
		}


		UpdateSprite();

		if (IsEvolved)
		{
			Monolith.Instance.Emerge();

			if (!ReachedGoal && !Monolith.Instance.IsCompleted && Vector2.Distance(Monolith.Instance.transform.position, transform.position) < Monolith.Instance.AttrackDistance)
			{
				ReachedGoal = true;
				transform.parent = Planet.Instance.Root;
				Monolith.Instance.RegisterWalker(this);
			}
		}
	}

	private void UpdateSprite()
	{
		Visual.transform.localScale = new Vector3((isGoingRight ? -1 : 1), 1, 1);

		if (ReachedGoal)
		{
			lookAroundTimer -= Chronos.Instance.DeltaTime;

			if (lookAroundTimer < 0)
				lookAroundTimer = Random.Range(4f, 10f);
		}

		scared.SetActive(IsEvolved && Chronos.Instance.Time - timeSinceLastFear < 1f);

		walkBase.SetActive(!IsEvolved);
		walkEvolved.SetActive(!scared.activeInHierarchy && IsEvolved && isWalking);

		lookAround.SetActive(!scared.activeInHierarchy && IsEvolved && !isWalking && lookAroundTimer < 1f);
		idle.SetActive(!scared.activeInHierarchy && IsEvolved && !isWalking && !lookAround.activeInHierarchy);
	}
}
