using UnityEngine;
using Random = UnityEngine.Random;

public class SplatterElementComponent : SplatterComponent
{
	private float startT;
	public float TimeToFullGrow;
	[TooltipAttribute("After the full growth, if not -1, will kill the component and make a decay splatter element.")]
	public float TimeToDieAfterGrow = -1;
	[TooltipAttribute("Time to die!")]
	public float TimeToDecay = 1;

	public float minGrow = 0;
	public float maxGrow = 1;
	public SplattersGroup[] SplattersRandom;
	private SplattersGroup CurrentSplatter;

	public override Sprite Splatter
	{
		get
		{
			if (CurrentSplatter == null)
				return null;

			var t = (Chronos.Instance.Time - startT) / TimeToFullGrow;
			t = t * (maxGrow - minGrow) + minGrow;
			t = Mathf.Clamp(t, minGrow, maxGrow);

			return CurrentSplatter.getSpatter(t);
		}
	}

	void Start()
	{
		startT = Chronos.Instance.Time;
		CurrentSplatter = SplattersRandom[Random.Range(0, SplattersRandom.Length - 1)];
	}

	void Update()
	{
		if (TimeToDieAfterGrow != -1 && Chronos.Instance.Time > TimeToDieAfterGrow + startT + TimeToFullGrow)
		{
			DIE();
		}
	}

	void DIE()
	{
		GameObject go = new GameObject("Dying Splater");
		go.transform.parent = this.transform.parent;
		go.transform.localPosition = this.transform.localPosition;
		go.tag = tag;

		var sed = go.AddComponent<SplatterElementDying>();
		sed.CurrentSplatter = CurrentSplatter;
		sed.TimeToDie = TimeToDecay;

		Destroy(gameObject);
	}

}