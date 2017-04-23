using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Timeline : UIBehaviour
{
	public Slider Slider;
	public Slider GhostSlider;

	bool isGhostDragging;
	readonly Queue<TimelineEvent> events = new Queue<TimelineEvent>();

	protected override void Awake()
	{
		foreach (var @event in new Queue<TimelineEvent>(GetComponentsInChildren<TimelineEvent>().OrderBy(e => e.Time)))
			events.Enqueue(@event);
	}

	void FixedUpdate()
	{
		Slider.value = Chronos.Instance.NormalizedTime;

		if (isGhostDragging)
			GhostSlider.value = Mathf.Max(GhostSlider.value, Slider.value);
		else
			GhostSlider.value = Slider.value;

		float distance = Mathf.Max(GhostSlider.value - Slider.value, 0f);
		Chronos.Instance.TimeScale = Mathf.Min(1f + distance * 50f, 100f);

		if (events.Count > 0 && Slider.value >= events.Peek().Time)
			events.Dequeue().Trigger();
	}

	public void OnGhostBeginDrag()
	{
		isGhostDragging = true;
	}

	public void OnGhostEndDrag()
	{
		isGhostDragging = false;
	}
}
