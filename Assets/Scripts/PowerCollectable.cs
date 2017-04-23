using UnityEngine;

public class PowerCollectable : MonoBehaviour
{
	public PowerManager.Powers Power;

	void OnMouseDown()
	{
		Debug.Log("T'AS PESÉ...");
		PowerManager.Instance.UnlockPower(Power);
	}
}
