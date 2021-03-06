﻿using UnityEngine;

public class Rotator : MonoBehaviour
{
	public float Rotations = 1f;
	public Transform Transform;
	public float MinTemperaturSpeedFactor = 1;

    private void Start()
    {
        if (Transform == null)
            Transform = transform;
    }

	void Update()
	{
		var f = Mathf.Clamp((Planet.Instance.Temperature / 50),MinTemperaturSpeedFactor,1);
		var s = Rotations * f ;

		Transform.localEulerAngles = new Vector3(
			Transform.localEulerAngles.x,
			Transform.localEulerAngles.y,
			Chronos.Instance.NormalizedTime * s * 360f);
	}
}
