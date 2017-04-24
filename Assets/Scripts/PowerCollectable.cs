using System.Collections;
using UnityEngine;

public class PowerCollectable : MonoBehaviour
{
	public PowerManager.Powers Power;

	void OnEnable()
	{
		StartCoroutine(GrowRoutine(0.25f));
		Groups.Add(this);
	}

	void OnDisable()
	{
		Groups.Remove(this);
	}

	void OnMouseDown()
	{
		StartCoroutine(ShrinkRoutine(0.25f));
		SoundManager.Instance.PlaySound("Powerup2");
	}

	IEnumerator GrowRoutine(float duration)
	{
		for (float i = 0; i < duration; i += Chronos.Instance.DeltaTime)
		{
			transform.localScale = Vector3.one * (i / duration);
			yield return null;
		}
	}

	IEnumerator ShrinkRoutine(float duration)
	{
		for (float i = 0; i < duration; i += Chronos.Instance.DeltaTime)
		{
			transform.localScale = Vector3.one * (1f - i / duration);
			yield return null;
		}

		PowerManager.Instance.UnlockPower(Power);
		Destroy(gameObject);
	}
}
