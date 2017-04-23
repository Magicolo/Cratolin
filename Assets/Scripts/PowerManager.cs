using UnityEngine;

public class PowerManager : MonoBehaviour
{
	public static PowerManager Instance { get; private set; }

	const string data = "Data";

	public enum Powers { Volcano, Rain, Wind, Fire, Loupe }

	void Awake()
	{
		Instance = this;
	}

	public bool HasPower(Powers power)
	{
		return PlayerPrefs.GetInt(data + power, 0) > 0;
	}

	public void UnlockPower(Powers power)
	{
		PlayerPrefs.SetInt(data + power, 1);
	}
}
