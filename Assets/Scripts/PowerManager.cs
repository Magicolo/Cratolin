using System.Linq;
using UnityEngine;

public class PowerManager : MonoBehaviour
{
	public enum Powers { Volcano, Rain, Wind, Fire, Loupe }

	public static PowerManager Instance { get; private set; }

	const string data = "Data";

	public PowerCollectable[] Prefabs;

	void Awake()
	{
		Instance = this;
	}

	public bool TrySpawnPower(Powers power, Vector2 position)
	{
		var prefab = Prefabs.FirstOrDefault(p => p.Power == power);

		if (prefab == null || HasPower(prefab.Power)) return false;

		Instantiate(prefab, new Vector3(position.x, position.y, -100f), Quaternion.identity, transform);
		return true;
	}

	public bool HasPower(Powers power)
	{
		return power == Powers.Loupe || PlayerPrefs.GetInt(data + power, 0) > 0;
	}

	public void LockPower(Powers power)
	{
		PlayerPrefs.DeleteKey(data + power);
	}

	public void UnlockPower(Powers power)
	{
		PlayerPrefs.SetInt(data + power, 1);
	}
}
