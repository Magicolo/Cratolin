using System.Collections;
using UnityEngine;

public class WindPower : PowerBase
{
	public WindSource Prefab;
	public LayerMask Mask;
	public SpriteRenderer windPreview;
	public AudioClip audioWind;

	private bool inPreview;

	public override int RemainingUses { get { return -1; } }
	public override PowerManager.Powers Power { get { return PowerManager.Powers.Wind; } }

	public override GameObject Create(Vector2 position)
	{
		if (!CanPlace(position))
			return null;

		windPreview.gameObject.SetActive(false);
		inPreview = false;
		StartCoroutine("WindPreviewAnimate");

		var direction = position.normalized;
		var hit = Physics2D.Raycast(direction * 1000f, -direction, 1000f, Mask);

		if (hit)
		{
			var instance = Instantiate(Prefab, hit.point, Quaternion.FromToRotation(Vector2.up, direction), Planet.Instance.Root);
			return instance.gameObject;
		}
		else
			return null;

	}

	override public void Cancel()
	{
		StopCoroutine("WindPreviewAnimate");
		windPreview.gameObject.SetActive(false);
		inPreview = false;
	}

	private IEnumerator WindPreviewAnimate()
	{
		SoundManager.Instance.PlaySound(audioWind);
		yield return new WaitForSeconds(2);

		while (windPreview.color.a > 0)
		{
			windPreview.color = new Color(1, 1, 1, windPreview.color.a - Chronos.Instance.DeltaTime);
			yield return null;
		}

		windPreview.gameObject.SetActive(false);
	}

	public override bool CanPlace(Vector2 position)
	{
		return true;
	}

	override public void StartPlacing()
	{
		base.StartPlacing();

		StopCoroutine("WindPreviewAnimate");
		inPreview = true;
		windPreview.transform.parent = null;
		windPreview.gameObject.SetActive(true);
		windPreview.color = Color.white;
	}

	void Update()
	{
		if (inPreview)
		{
			Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			windPreview.transform.position = pos.normalized * 370;
		}
	}
}
