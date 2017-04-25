using UnityEngine;

public class TreePower : PowerBase
{
	public LayerMask Mask;
	public Transform Preview;
	public ListOfPrefab Prefabs;

	public override int RemainingUses { get { return -1; } }
	public override PowerManager.Powers Power { get { return PowerManager.Powers.Tree; } }

	public override GameObject Create(Vector2 position)
	{
		Preview.gameObject.SetActive(false);

		if (!CanPlace(position))
			return null;

		var direction = position.normalized;
		var hit = Physics2D.Raycast(direction * 1000f, -direction, 1000f, Mask);

		if (hit)
		{
			var instance = Instantiate(Prefabs.getRandom(), hit.point, Quaternion.FromToRotation(Vector2.up, direction), Planet.Instance.Root);
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
}