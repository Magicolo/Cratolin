using UnityEngine;

public class Graviton : MonoBehaviour
{
	public static Graviton Instance { get; private set; }

	public Vector2 Gravity = new Vector2(0f, -9.8f);

	void Awake()
	{
		Instance = this;
	}
}
