using UnityEngine;

public class Planet : MonoBehaviour
{
	public static Planet Instance { get; private set; }

	public float Rotations = 4f;

	void Awake()
	{
		Instance = this;
	}

	void FixedUpdate()
	{
		transform.localEulerAngles = new Vector3(
			transform.localEulerAngles.x,
			transform.localEulerAngles.y,
			Chronos.Instance.NormalizedTime * Rotations * 360f);
	}
}
