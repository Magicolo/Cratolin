using UnityEngine;

public class RainCloud : MonoBehaviour
{

	public float distanceToGround;
	public GameObject rainDropPrefab;
	public float rainZoneWidth;
	public float timeBetweenRainDrop;
	public float lifeTime;
	public AudioClip audioClip;

	private float lastTimeSpawn;

	// Use this for initialization
	void Start()
	{
		lastTimeSpawn = Chronos.Instance.Time;
		Destroy(gameObject, lifeTime);

		SoundManager.Instance.PlaySound(audioClip);
	}

	void OnEnable()
	{
		Groups.Add(this);
	}
	void OnDisable()
	{
		Groups.Remove(this);
	}

	// Update is called once per frame
	void Update()
	{
		transform.up = transform.position.normalized;

		if (Chronos.Instance.Time - lastTimeSpawn > timeBetweenRainDrop)
		{
			lastTimeSpawn = Chronos.Instance.Time;
			GameObject obj = Instantiate(rainDropPrefab);
			obj.transform.position = transform.position;
			obj.transform.up = transform.up;

			obj.transform.position += transform.right * Random.Range(-rainZoneWidth * 0.5f, rainZoneWidth * 0.5f);
			obj.transform.parent = null;
		}
	}
}
