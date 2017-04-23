using System.Collections;
using UnityEngine;

public class WheelPowerItem : MonoBehaviour
{
	public SpriteRenderer auraRenderer;
	public Sprite spriteToUseAsBeam;

	private float currentAlpha = 1;
	private float destAlpha = 1;

	void OnEnable()
	{
		StartCoroutine(GrowRoutine(0.25f));
	}

	IEnumerator GrowRoutine(float duration)
	{
		for (float i = 0; i < duration; i += Chronos.Instance.DeltaTime)
		{
			transform.localScale = Vector3.one * (i / duration);
			yield return null;
		}
	}

	void LateUpdate()
	{
		transform.eulerAngles = Vector3.zero;

		currentAlpha = Mathf.Lerp(currentAlpha, destAlpha, Time.unscaledDeltaTime * 8);
		Renderer[] sprites = GetComponentsInChildren<Renderer>();
		foreach (Renderer sprite in sprites)
		{
			sprite.material.color = new Color(sprite.material.color.r, sprite.material.color.g, sprite.material.color.b, currentAlpha);
		}

		int uses = GetComponent<PowerBase>().RemainingUses;
		GetComponentInChildren<TextMesh>().text = uses == -1 ? "" : uses.ToString();

		bool disabled = !GetComponent<PowerBase>().CanUse;
		if (disabled)
			GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.3f);
		else
			GetComponent<SpriteRenderer>().color = Color.white;
	}

	public void Spawn(Vector3 pPosition)
	{
		PowerBase basePower = GetComponent<PowerBase>();

		if (basePower != null)
			basePower.Create(pPosition);
	}

	public void Cancel()
	{
		PowerBase basePower = GetComponent<PowerBase>();

		if (basePower != null)
			basePower.Cancel();
	}

	public void SetDestinationAlpha(int value)
	{
		if (value == 1)
			auraRenderer.sprite = spriteToUseAsBeam;
		destAlpha = value;
	}
}
