using UnityEngine;
using UnityEngine.UI;

public class UIDragMe : MonoBehaviour
{
	public Text Text;

	void FixedUpdate()
	{
		Text.gameObject.SetActive(!PowerManager.Instance.HasPower(PowerManager.Powers.Volcano) && Chronos.Instance.NormalizedTime < 1f);
	}
}
