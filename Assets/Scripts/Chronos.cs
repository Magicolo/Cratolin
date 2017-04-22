using UnityEngine;

public class Chronos : MonoBehaviour
{
	public static Chronos Instance { get; private set; }

	public float LifeTime = 180f;

	public float TimeScale
	{
		get { return Time.timeScale; }
		set { Time.timeScale = value; }
	}
	public float CurrentTime { get; private set; }
	public float DeltaTime { get { return Time.fixedDeltaTime; } }

	void Awake()
	{
		Instance = this;
	}

	void FixedUpdate()
	{
		CurrentTime += DeltaTime;
	}
}
