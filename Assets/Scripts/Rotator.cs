using UnityEngine;

public class Rotator : MonoBehaviour
{
	public float Rotations = 1f;
	public Transform Transform;

	void FixedUpdate()
	{
		transform.localEulerAngles = new Vector3(
			transform.localEulerAngles.x,
			transform.localEulerAngles.y,
			Chronos.Instance.NormalizedTime * Rotations * 360f);
	}
}
