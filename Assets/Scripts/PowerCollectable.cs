using System.Collections;
using UnityEngine;

public class PowerCollectable : MonoBehaviour
{
	public PowerManager.Powers Power;

	void OnMouseDown()
	{
		StartCoroutine(ShrinkRoutine(0.25f));
	}

	IEnumerator ShrinkRoutine(float shrinkDuration)
	{
		for (float i = 0; i < shrinkDuration; i += Chronos.Instance.DeltaTime)
		{
			transform.localScale = Vector3.one * (1f - i / shrinkDuration);
			yield return null;
		}

		PowerManager.Instance.UnlockPower(Power);
		Destroy(gameObject);
	}
}
