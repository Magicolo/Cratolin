using UnityEngine;

public class FirePower : PowerBase
{
	public float Cooldown;
	public SpriteRenderer firePreview;
	public AudioClip audioFire;
	public ParticleSystem particles;

	public override bool CanUse { get { return base.CanUse && Chronos.Instance.Time - lastUse >= Cooldown; } }
	public override int RemainingUses { get { return -1; } }
	public override PowerManager.Powers Power { get { return PowerManager.Powers.Fire; } }

	float lastUse = float.MinValue;

	public override GameObject Create(Vector2 position)
	{
		firePreview.gameObject.SetActive(false);

		if (!CanPlace(position))
			return null;

		RaycastHit2D hit = Physics2D.Raycast(position, -position.normalized);

		SoundManager.Instance.PlaySound(audioFire);

		particles.Play();

		foreach (FireAbleObject fire in Groups.Get<FireAbleObject>())
		{
			if (Vector2.Distance(fire.transform.position, hit.point) < 100)
				fire.StartFire();
		}

		foreach (SplatterElementComponent splatter in FindObjectsOfType<SplatterElementComponent>())
		{
			if (splatter.FireKillsMe && Vector2.Distance(splatter.transform.position, hit.point) < 100)
				splatter.DIE(0.2f);
		}

		foreach (var volcano in Groups.Get<Volcano>())
		{
			if (Vector2.Distance(volcano.transform.position, hit.point) < 100)
				volcano.Heat += 20;
		}

		foreach (var sea in Groups.Get<Sea>())
		{
			if (Vector2.Distance(sea.transform.position, hit.point) < 100)
				sea.BurnWater();
		}

		foreach (Walker walker in Walker.Walkers)
		{
			if (Vector2.Distance(walker.transform.position, hit.point) < 100)
				walker.Fear(position * 2);
		}

		foreach (var seedling in Groups.Get<Seedling>())
		{
			if (Vector2.Distance(seedling.transform.position, hit.point) < 100)
				seedling.CatchFire();

		}

		return null;
	}

	override public void StartPlacing()
	{
		base.StartPlacing();

		firePreview.transform.parent = null;
		firePreview.gameObject.SetActive(true);
		Update();
	}

	override public void Cancel()
	{
		firePreview.gameObject.SetActive(false);
	}

	void Update()
	{
		particles.transform.position = firePreview.transform.position;

		if (firePreview.gameObject.activeInHierarchy)
		{
			Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			firePreview.transform.position = pos.normalized * 365;
		}

	}
}