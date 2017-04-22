using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Timeline : UIBehaviour
{
	public Slider Slider;
	public Slider GhostSlider;

	bool isGhostDragging;

	void FixedUpdate()
	{
		Slider.value = Chronos.Instance.CurrentTime / Chronos.Instance.LifeTime;

		if (isGhostDragging)
			GhostSlider.value = Mathf.Max(GhostSlider.value, Slider.value);
		else
			GhostSlider.value = Slider.value;

		float distance = Mathf.Max(GhostSlider.value - Slider.value, 0f);
		Chronos.Instance.TimeScale = 1f + distance * 10f;

		Debug.Log(Chronos.Instance.TimeScale + " " + distance);
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
