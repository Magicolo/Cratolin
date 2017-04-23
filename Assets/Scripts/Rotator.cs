using UnityEngine;

public class Rotator : MonoBehaviour
{
	public float Rotations = 1f;
	public Transform Transform;

    private void Start()
    {
        if (Transform == null)
            Transform = transform;
    }

	void Update()
	{
		Transform.localEulerAngles = new Vector3(
			Transform.localEulerAngles.x,
			Transform.localEulerAngles.y,
			Chronos.Instance.NormalizedTime * Rotations * 360f);
	}
}
