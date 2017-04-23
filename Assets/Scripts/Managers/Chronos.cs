using UnityEngine;

public class Chronos : MonoBehaviour
{
	public static Chronos Instance { get; private set; }

	public float LifeTime = 180f;

	public float TimeScale
	{
		get { return UnityEngine.Time.timeScale; }
		set { UnityEngine.Time.timeScale = value; }
	}
	public float Time { get; private set; }
	public float NormalizedTime { get { return Time / LifeTime; } }
	public float DeltaTime { get { return UnityEngine.Time.fixedDeltaTime; } }

	void Awake()
	{
		Instance = this;
	}

	void FixedUpdate()
	{
		Time += DeltaTime;
	}
}
