using UnityEngine;

public class VolcanoPower : PowerBase
{
	public LayerMask Mask;
	public Volcano Prefab;

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
			var instance = Instantiate(Prefab, hit.point, Quaternion.FromToRotation(Vector2.up, direction), Planet.Instance.transform);
			return instance.gameObject;
		}
		else
			return null;

	}

	public override bool CanPlace(Vector2 position)
	{
		return true;
	}
}
