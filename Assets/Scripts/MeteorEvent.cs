using UnityEngine;

public class MeteorEvent : TimelineEvent
{
	public GameObject Prefab;

	public override void Trigger()
	{
		Debug.Log("TRIGGER");
	}
}
