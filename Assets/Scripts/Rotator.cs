using UnityEngine;

public class Rotator : MonoBehaviour
{
	public float Rotations = 1f;
	public Transform Transform;

	void Update()
	{
		Transform.localEulerAngles = new Vector3(
			Transform.localEulerAngles.x,
			Transform.localEulerAngles.y,
			Chronos.Instance.NormalizedTime * Rotations * 360f);
	}
}
