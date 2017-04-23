﻿using UnityEngine;
using UnityEngine.UI;

public class UIRetry : MonoBehaviour
{
	public Button Button;

	void FixedUpdate()
	{
		Button.gameObject.SetActive(!Planet.Instance.gameObject.activeSelf);
	}

	public void OnClick()
	{
		Game.Instance.Reload();
	}
}