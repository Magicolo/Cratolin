using System.Linq;
using UnityEngine;

public class WindParticle : MonoBehaviour
{
	public LayerMask maskPlanet;
	public float distanceFormPlanetCenter;
	public float moveSpeed;
	public Tree[] TreePrefab;
	public Color InFireColor;
	private Color NormalColor;

	private float direction = 1;
	private float lifeTimer;
	private float waveAmplitude;
	private bool polenized = false;
	private bool inFire = false;
	private bool disabled = false;

	public float Direction { get { return direction; } set { direction = value; } }

	void OnEnable()
	{
		Groups.Add(this);
		NormalColor = GetComponentInChildren<TrailRenderer>().material.GetColor("_TintColor");
	}

	void OnDisable()
	{
		Groups.Remove(this);
	}

	void Start()
	{
		lifeTimer = Random.Range(0f, 10f);
		waveAmplitude = Random.Range(0f, 15f);

		GetComponent<TrailRenderer>().sortingOrder = -5;
		GetComponent<TrailRenderer>().Clear();

		moveSpeed = moveSpeed * Random.Range(0.8f, 1.2f);

		//// Not all particles have trails
		//if (Random.Range(0, 5) > 0)
		//    Destroy(GetComponent<TrailRenderer>());

		transform.up = transform.position.normalized;
		transform.position = transform.up * (distanceFormPlanetCenter + Mathf.Sin(lifeTimer * 4) * waveAmplitude);
	}

	void FixedUpdate()
	{

		if (disabled)
			return;

		var color = (inFire) ? InFireColor : NormalColor;
		GetComponentInChildren<TrailRenderer>().material.SetColor("_TintColor", new Color(color.r, color.g, color.b, color.a));

		GetComponentInChildren<SpriteRenderer>().enabled = polenized;

		// Destroy on hitting ground
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 1000f, maskPlanet);
		if (hit.collider != null)
		{
			if (polenized && Groups.Get<Tree>().Count(t => Vector2.Distance(transform.position, t.transform.position) > 100f) < 3f)
				Instantiate(TreePrefab[Random.Range(0, TreePrefab.Length)], transform.position, Quaternion.FromToRotation(Vector2.up, transform.position.normalized), Planet.Instance.Root);

			Destroy(gameObject, 5);
			disabled = true;
			return;
		}

		RaycastHit2D hitTree = Physics2D.Raycast(transform.position, Vector2.zero);

		if (GameConstants.Instance.WindTakesFireWithVolcano && hitTree.collider != null
			&& hitTree.collider.GetComponentInParent<Volcano>() != null)
		{
			inFire = true;
			polenized = false;
		}

		if (hitTree.collider != null && hitTree.collider.GetComponentInParent<Tree>() != null)
		{
			var tree = hitTree.collider.GetComponentInParent<Tree>();
			var fire = hitTree.collider.GetComponentInParent<FireAbleObject>();
			if (inFire && fire != null && !fire.IsOnFire)
			{
				// propagate fire particle on other tree;
				fire.StartFire();

			}
			else if (fire != null && fire.IsOnFire)
			{
				inFire = true;
				polenized = false;
			}
			else if (!inFire && tree.MyState.Equals(Tree.States.Idle) && tree.Sprites.Last().gameObject.activeInHierarchy)
				polenized = true;
		}


		foreach (var cloud in Groups.Get<RainCloud>())
		{
			var distance = Mathf.Abs((cloud.transform.position - transform.position).magnitude);

			if (distance < 70)
				inFire = false;

			if (distance < 90)
			{
				cloud.transform.position += cloud.transform.right * moveSpeed * 0.4f * Chronos.Instance.DeltaTime * (1 - distance / 90);
			}
		}

		lifeTimer += Chronos.Instance.DeltaTime;

		transform.up = transform.position.normalized;
		transform.position = transform.up * (distanceFormPlanetCenter + Mathf.Sin(lifeTimer * 4) * waveAmplitude);

		transform.position += transform.right * Chronos.Instance.DeltaTime * moveSpeed * Direction;

		// go more and more closer to the planet
		distanceFormPlanetCenter -= Chronos.Instance.DeltaTime * 5;

	}
}
