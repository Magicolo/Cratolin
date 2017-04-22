using UnityEngine;
using UnityEngine.UI;

public abstract class TimelineEvent : MonoBehaviour
{
	[Range(0f, 1f)]
	public float Time;
	public Timeline Timeline;
	public Slider Slider;

	public abstract void Trigger();

	protected virtual void OnValidate()
	{
		if (Timeline == null || Slider == null)
			return;

		Slider.value = Time;
	}
}
