public class InvaderEvent : TimelineEvent
{
	public Invader Prefab;

	public override void Trigger()
	{
		Instantiate(Prefab);
	}
}
