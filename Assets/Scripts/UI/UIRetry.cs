﻿using UnityEngine;
using UnityEngine.UI;

public class UIRetry : MonoBehaviour
{
	public Button Button;

	void FixedUpdate()
	{
		Button.gameObject.SetActive(!Planet.Instance.gameObject.activeSelf && FindObjectOfType<PowerCollectable>() == null);
	}

	public void OnClick()
	{
        Score.Instance.nbCycle++;
		Game.Instance.Reload();
	}
}
