using UnityEngine;

public class VolcanoPower : PowerBase
{
	public int Uses = 4;
	public float Cooldown = 5f;
	public LayerMask Mask;
	public Volcano Prefab;
	public Transform Preview;

	public override bool CanUse { get { return base.CanUse && Chronos.Instance.Time - lastUse >= Cooldown; } }
	public override int RemainingUses { get { return Uses - uses; } }
	public override PowerManager.Powers Power { get { return PowerManager.Powers.Volcano; } }

	int uses;
	float lastUse = float.MinValue;

	void Update()
	{
		if (Preview.gameObject.activeSelf)
		{
			var direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized;
			var hit = Physics2D.Raycast(direction * 1000f, -direction, 1000f, Mask);

			if (hit)
			{
				Preview.position = hit.point;
				Preview.rotation = Quaternion.FromToRotation(Vector2.up, direction);
			}
		}
	}

	public override GameObject Create(Vector2 position)
	{
		if (!CanPlace(position))
			return null;

		Preview.gameObject.SetActive(false);
		var direction = position.normalized;
		var hit = Physics2D.Raycast(direction * 1000f, -direction, 1000f, Mask);

		if (hit)
		{
			uses++;
			lastUse = Chronos.Instance.Time;
			var instance = Instantiate(Prefab, hit.point, Quaternion.FromToRotation(Vector2.up, direction), Planet.Instance.Root);
			return instance.gameObject;
		}
		else
			return null;

	}

	public override void StartPlacing()
	{
		base.StartPlacing();

		Preview.gameObject.SetActive(true);
	}

	public override void Cancel()
	{
		base.Cancel();

		Preview.gameObject.SetActive(false);
	}
}
