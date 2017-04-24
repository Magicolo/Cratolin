using System;
using System.Linq;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
	public bool active = false;

#if UNITY_EDITOR
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			active = !active;
			var mod = FindObjectOfType<CheatModImage>();
			if (mod != null) mod.SetVisibility(active);
		}

		if (!active) return;


		if (Input.GetKey(KeyCode.T))
			Planet.Instance.Temperature += Chronos.Instance.DeltaTime * 50;
		if (Input.GetKey(KeyCode.Y))
			Planet.Instance.Temperature -= Chronos.Instance.DeltaTime * 50;
		if (Input.GetKey(KeyCode.U))
		{
			foreach (var power in Enum.GetValues(typeof(PowerManager.Powers)).Cast<PowerManager.Powers>())
				PowerManager.Instance.UnlockPower(power);
		}
		if (Input.GetKey(KeyCode.L))
		{
			foreach (var power in Enum.GetValues(typeof(PowerManager.Powers)).Cast<PowerManager.Powers>())
				PowerManager.Instance.LockPower(power);
		}

		if (Input.GetKey(KeyCode.Alpha1))
			Chronos.Instance.TimeScale = 1;
		if (Input.GetKey(KeyCode.Alpha2))
			Chronos.Instance.TimeScale = 16;
		if (Input.GetKey(KeyCode.Alpha3))
			Chronos.Instance.TimeScale = 50;

		if (Input.GetKey(KeyCode.O))
			foreach (var item in FindObjectsOfType<Sea>())
				item.IncreaseWater();
		if (Input.GetKey(KeyCode.P))
			foreach (var item in FindObjectsOfType<Sea>())
				item.ReduceWater();



		if (Input.GetKey(KeyCode.X))
		{
			foreach (var item in GameObject.FindObjectsOfType<Sea>())
			{
				item.CanSpawnLifeAround = true;
			}
		}

	}
#endif
}