﻿using UnityEngine;

public class LoupePower : PowerBase
{

	public GameObject RenderTarget;
	public Transform cameraTransform;

	private bool inLoupe;

	public override bool CanUse { get { return base.CanUse; } }
	public override int RemainingUses { get { return -1; } }
	public override PowerManager.Powers Power { get { return PowerManager.Powers.Loupe; } }

	public override GameObject Create(Vector2 position)
	{
		if (!CanPlace(position))
			return null;



		return null;
	}

	override public void StartPlacing()
	{
		inLoupe = true;
		RenderTarget.SetActive(true);
	}

	void Update()
	{
		if (inLoupe)
		{
			cameraTransform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			if (Input.GetMouseButtonUp(0))
			{
				inLoupe = false;
				RenderTarget.SetActive(false);
			}
		}
	}
}
