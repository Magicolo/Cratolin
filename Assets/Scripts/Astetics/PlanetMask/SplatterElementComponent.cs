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
	private SplattersGroup splatterGroup;
	private Sprite CurrentSplatter;

	private BoxCollider2D triggerCollider = null;

	public bool RainKillsMe;
	public bool FireKillsMe;

	public override Sprite Splatter
	{
		get{ return CurrentSplatter; }
	}

	void Awake(){
		/*triggerCollider = gameObject.GetComponent<BoxCollider2D>();
		if(triggerCollider == null)
			triggerCollider = gameObject.AddComponent<BoxCollider2D>();
			triggerCollider.isTrigger = true;*/
	}

	void Start()
	{
		startT = Chronos.Instance.Time;
		splatterGroup = SplattersRandom[Random.Range(0, SplattersRandom.Length - 1)];
	}

	Sprite lastSplatterUpdate;

	void Update()
	{

		if (splatterGroup != null){
			var t = (Chronos.Instance.Time - startT) / TimeToFullGrow;
			t = t * (maxGrow - minGrow) + minGrow;
			t = Mathf.Clamp(t, minGrow, maxGrow);

			CurrentSplatter = splatterGroup.getSpatter(t);
		}

		if (triggerCollider != null && CurrentSplatter != lastSplatterUpdate && CurrentSplatter != null)
			triggerCollider.size = CurrentSplatter.bounds.size;

		lastSplatterUpdate = CurrentSplatter;

		if (TimeToDieAfterGrow != -1 && Chronos.Instance.Time > TimeToDieAfterGrow + startT + TimeToFullGrow)
		{
			DIE();
		}
	}

	public void DIE(float timeToDecayFactor = 1)
	{
		GameObject go = new GameObject("Dying Splater");
		go.transform.parent = this.transform.parent;
		go.transform.localPosition = this.transform.localPosition;
		go.tag = tag;

		var sed = go.AddComponent<SplatterElementDying>();
		sed.CurrentSplatter = splatterGroup;
		sed.TimeToDie = TimeToDecay * timeToDecayFactor;

		Destroy(gameObject);
	}

}