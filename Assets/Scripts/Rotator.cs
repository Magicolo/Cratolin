using UnityEngine;

public class Rotator : MonoBehaviour
{
	public float Speed = 1f;
	public Transform Transform;

	void FixedUpdate()
	{
		Transform.Rotate(0f, 0f, Speed * Chronos.Instance.DeltaTime);
	}
}
