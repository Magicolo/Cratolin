using UnityEngine;

public class VolcanoPower : PowerBase
{
	public int Uses = 4;
	public float Cooldown = 5f;
	public LayerMask Mask;
	public Volcano Prefab;

	public override bool CanUse
	{
		get { return base.CanUse && Chronos.Instance.CurrentTime - lastUse >= Cooldown; }
	}
	public override int RemainingUses
	{
		get { return Uses = uses; }
	}

	int uses;
	float lastUse = float.MinValue;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			Create(Camera.main.ScreenToWorldPoint(Input.mousePosition));
	}

	public override GameObject Create(Vector2 position)
	{
		if (!CanPlace(position))
			return null;

		var direction = position.normalized;
		var hit = Physics2D.Raycast(direction * 1000f, -direction, 1000f, Mask);

		if (hit)
		{
			uses++;
			lastUse = Chronos.Instance.CurrentTime;
			var instance = Instantiate(Prefab, hit.point, Quaternion.FromToRotation(Vector2.up, direction), Planet.Instance.transform);
			return instance.gameObject;
		}
		else
			return null;

	}
}
