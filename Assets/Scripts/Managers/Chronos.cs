using UnityEngine;

public class Chronos : MonoBehaviour
{
	private static Chronos instance;
	public static Chronos Instance {
		get{
			if(instance == null)
				instance = GameObject.FindObjectOfType<Chronos>();
			if(instance == null)
				instance = new GameObject("Chronos").AddComponent<Chronos>();
			return instance;
		}  
	}

	public float LifeTime = 180f;

	public float TimeScale
	{
		get { return UnityEngine.Time.timeScale; }
		set { UnityEngine.Time.timeScale = value; }
	}
	public float Time { get; private set; }
	public float NormalizedTime { get { return Time / LifeTime; } }
	public float DeltaTime { get { return UnityEngine.Time.fixedDeltaTime; } }

	void FixedUpdate()
	{
		Time += DeltaTime;
	}
}
