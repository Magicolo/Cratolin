using UnityEngine;

public class Planet : MonoBehaviour
{
	public static Planet Instance { get; private set; }

	void Awake()
	{
		Instance = this;
	}
}
