using UnityEngine;

public class RainPower : PowerBase
{
	public float Cooldown = 1f;
	public LayerMask Mask;
	public RainCloud Prefab;
	public Transform Preview;

	public override bool CanUse { get { return base.CanUse && Chronos.Instance.Time - lastUse >= Cooldown; } }
	public override int RemainingUses { get { return -1; } }
	public override PowerManager.Powers Power { get { return PowerManager.Powers.Rain; } }

	float lastUse = float.MinValue;

	void Update()
	{
		if (Preview.gameObject.activeSelf)
		{
			var direction = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized;
			var hit = Physics2D.Raycast(direction * 1000f, -direction, 1000f, Mask);

			if (hit)
			{
				Preview.position = hit.point + direction * 250f;
				Preview.rotation = Quaternion.FromToRotation(Vector2.up, direction);
			}
		}
	}

	public override GameObject Create(Vector2 position)
	{
		Preview.gameObject.SetActive(false);

		if (!CanPlace(position))
			return null;

		var direction = position.normalized;
		var hit = Physics2D.Raycast(direction * 1000f, -direction, 1000f, Mask);

		if (hit)
		{
			lastUse = Chronos.Instance.Time;
			var instance = Instantiate(Prefab, hit.point + direction * 100f, Quaternion.FromToRotation(Vector2.up, direction), Planet.Instance.Root);
			return instance.gameObject;
		}
		else
			return null;




		//RainCloud objRainCloud = Instantiate(Prefab, Planet.Instance.Root);
		//objRainCloud.transform.position = position;

		//RaycastHit2D hit = Physics2D.Raycast(objRainCloud.transform.position, Vector3.zero - objRainCloud.transform.position, 10000, Mask);

		//Vector2 v2 = (Vector2)objRainCloud.transform.position - Vector2.zero;
		//float angle = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;

		//objRainCloud.transform.localEulerAngles = new Vector3(0, 0, angle - 90);

		//objRainCloud.transform.position = (Vector3)hit.point + objRainCloud.transform.up * objRainCloud.distanceToGround;
		//lastUse = Chronos.Instance.Time;

		//return objRainCloud.gameObject;
	}

	public override void StartPlacing()
	{
		base.StartPlacing();

		Preview.gameObject.SetActive(true);
		Update();
	}

	public override void Cancel()
	{
		base.Cancel();

		Preview.gameObject.SetActive(false);
	}
}
