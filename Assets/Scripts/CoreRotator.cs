using UnityEngine;

public class CoreRotator : MonoBehaviour
{
	public float Rotations = 1f;
	public Transform Transform;
	public float MinTemperaturSpeedFactor = 1;

    private void Start()
    {
        if (Transform == null)
            Transform = transform;
    }

	float lastT = 0;

	void Update()
	{
		var f = Mathf.Clamp((Planet.Instance.Temperature / 50),MinTemperaturSpeedFactor,1);
		var s = Rotations * f ;
		var z = Transform.localEulerAngles.z;
		var t = Chronos.Instance.Time - lastT;
		lastT = Chronos.Instance.Time;

		Transform.localEulerAngles = new Vector3(
			Transform.localEulerAngles.x,
			Transform.localEulerAngles.y,
			z + t * s);
	}
}
