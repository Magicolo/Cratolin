using System.Collections.Generic;
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
	private bool inFire;
	private float lastTimeInFire;

	public bool IsEvolved { get { return Time.time - timeSpawn > 20; } }
	public bool ReachedGoal { get; set; }

	// Use this for initialization
	void Start()
	{
		transform.parent = Planet.Instance.transform;
		timeSpawn = Time.time;

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
			timeSinceLastFear = Time.time;

			bool fearLeftOf = (Vector2.Angle(transform.right, pPosition)) > 90 && (Vector2.Angle(transform.right, pPosition)) < 180;

			if (fearLeftOf)
				isGoingRight = false;
			else
				isGoingRight = true;
		}
	}

	public void CatchFire()
	{
		timeSinceLastFear = Time.time;
		fireAnim.gameObject.SetActive(true);
		lastTimeInFire = Time.time;
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

			if (Time.time - timeSinceLastFear < 3 || inFire)
				speed *= 3;

			return speed;
		}
	}

	// Update is called once per frame
	void LateUpdate()
	{
		isWalking = !ReachedGoal && (!IsEvolved || Time.time - timeSinceLastFear > 1f);


		if (inFire && Time.time - lastTimeInFire > 10)
		{
			Destroy(gameObject);
			return;
		}


		// stick to ground
		RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 100000, WalkerCollision);
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
				transform.localPosition += transform.right * Time.deltaTime * CurrentSpeed;
			else if (isGoingRight && !canGoRight)
				isGoingRight = false;
			else if (!isGoingRight && canGoLeft)
				transform.localPosition -= transform.right * Time.deltaTime * CurrentSpeed;
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
			lookAroundTimer -= Time.deltaTime;

			if (lookAroundTimer < 0)
				lookAroundTimer = Random.Range(4f, 10f);
		}

		scared.SetActive(IsEvolved && Time.time - timeSinceLastFear < 1f);

		walkBase.SetActive(!IsEvolved);
		walkEvolved.SetActive(!scared.activeInHierarchy && IsEvolved && isWalking);

		lookAround.SetActive(!scared.activeInHierarchy && IsEvolved && !isWalking && lookAroundTimer < 1f);
		idle.SetActive(!scared.activeInHierarchy && IsEvolved && !isWalking && !lookAround.activeInHierarchy);
	}
}
