﻿using UnityEngine;

public class Oscillator : MonoBehaviour
{
	public Vector2 Amplitude;
	public Vector2 Frequency;
	public Vector2 Center;
	public Vector2 Offset;
	public Transform Transform;

	void Update()
	{
		var position = Transform.localPosition;
		position.x = Mathf.Sin(Frequency.x * Chronos.Instance.Time + Offset.x) * Amplitude.x + Center.x;
		position.y = Mathf.Sin(Frequency.y * Chronos.Instance.Time + Offset.y) * Amplitude.y + Center.y;
		Transform.localPosition = position;
	}
}
