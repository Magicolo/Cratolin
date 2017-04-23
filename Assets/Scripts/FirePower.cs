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
		if (!CanPlace(position))
			return null;

		RaycastHit2D hit = Physics2D.Raycast(position, -position.normalized);

        SoundManager.Instance.PlaySound(audioFire);
        
        particles.Play();

        FireAbleObject[] fireObjects = GameObject.FindObjectsOfType<FireAbleObject>();
		foreach (FireAbleObject fire in fireObjects)
		{
			if (Vector2.Distance(fire.transform.position, hit.point) < 100)
				fire.StartFire();
		}

		var splatterObjects = GameObject.FindObjectsOfType<SplatterElementComponent>();
		foreach (SplatterElementComponent splatter in splatterObjects)
		{
			if (splatter.FireKillsMe && Vector2.Distance(splatter.transform.position, hit.point) < 100)
				splatter.DIE(0.2f);
		}

        Walker[] walkers = GameObject.FindObjectsOfType<Walker>();
        foreach (Walker walker in walkers)
        {
            if (Vector2.Distance(walker.transform.position, hit.point) < 100)
            {
                walker.Fear(position * 2);
            }

        }

        firePreview.gameObject.SetActive(false);

		return null;
	}

	override public void StartPlacing()
	{
		base.StartPlacing();

		firePreview.transform.parent = null;
		firePreview.gameObject.SetActive(true);
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