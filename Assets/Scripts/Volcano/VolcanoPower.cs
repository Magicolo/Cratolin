using UnityEngine;

public class VolcanoPower : PowerBase
{
	public int Uses = 4;
	public float Cooldown = 5f;
	public LayerMask Mask;
	public Volcano Prefab;
	public Transform Preview;
	public SpriteRenderer PreviewSpriteRenderer;
	public float PreviewSpriteAlpha = 0.5f;
	public float MinDistanceBetweenVolcanos;

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

			if( !allowedPosition(hit.point)){
				if(PreviewSpriteRenderer != null)
					PreviewSpriteRenderer.color = new Color(1,0.25f,0.25f,PreviewSpriteAlpha);
			}else{
				if(PreviewSpriteRenderer != null)
					PreviewSpriteRenderer.color = new Color(1,1,1,PreviewSpriteAlpha);
			}

			if (hit)
			{
				Preview.position = hit.point;
				Preview.rotation = Quaternion.FromToRotation(Vector2.up, direction);
			}
		}
	}

	bool allowedPosition(Vector2 position){

		foreach (var sea in Groups.Get<Sea>())
			if(sea.SeaCollider.bounds.Contains(position))
				return false;

		foreach (var volcano in Groups.Get<Volcano>()){
			if(volcano.GetComponentInChildren<Collider2D>().bounds.Contains(position) 
				|| Vector2.Distance(position, volcano.transform.position) < MinDistanceBetweenVolcanos)
				return false;
		}

		return true;
	}

	public override GameObject Create(Vector2 position)
	{
		Preview.gameObject.SetActive(false);

		if (!CanPlace(position))
			return null;

		var direction = position.normalized;
		var hit = Physics2D.Raycast(direction * 1000f, -direction, 1000f, Mask);

		if (hit && allowedPosition(hit.point))
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

	void OnEnable()
	{
		Groups.Add(this);
	}

	void OnDisable()
	{
		Groups.Remove(this);
	}
}
