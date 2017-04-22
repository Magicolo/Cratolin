using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zTest : MonoBehaviour
{
	public float TimeScale = 1f;

	DateTime lastUpdate = DateTime.Now;

	void FixedUpdate()
	{
		Debug.Log((DateTime.Now - lastUpdate).TotalMilliseconds + " " + Time.fixedDeltaTime);
		lastUpdate = DateTime.Now;
	}

	void OnValidate()
	{
		Time.timeScale = TimeScale;
	}
}
