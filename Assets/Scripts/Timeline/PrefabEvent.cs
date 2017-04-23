using UnityEngine;

public class PrefabEvent : TimelineEvent
{
	public GameObject Prefab;

	public override void Trigger()
	{
		Instantiate(Prefab);
	}
}
