﻿using UnityEngine;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour
{
	public static WinMenu Instance { get; private set; }

	public Image content;
	public Text LabelScore;
	public Text LabelNbCycle;

	public bool IsShown { get; set; }

	private bool wasShown = false;

	void Start()
	{
		Instance = this;
		int t = Score.Instance.nbCycle;
	}

	void Update()
	{
		if (!wasShown && IsShown)
		{
			LabelScore.text = "Time: " + Score.Instance.FormattedTime();
			LabelNbCycle.text = "Cycles: " + Score.Instance.nbCycle;
		}

		CanvasGroup group = GetComponent<CanvasGroup>();

		float dest = IsShown ? 1 : 0;
		group.alpha = Mathf.Lerp(group.alpha, dest, Time.unscaledDeltaTime * 8);


		content.gameObject.SetActive(group.alpha > 0.05f);

		wasShown = IsShown;
	}

	public void Continue()
	{
		SoundManager.Instance.PlayGeneralClick();
		Game.Instance.IsSandbox = true;
		IsShown = false;
	}

	public void Restart()
	{
		SoundManager.Instance.PlayGeneralClick();
		Score.Instance.Reset();
		Game.Instance.Reload();
	}

	public void ExitGame()
	{
		SoundManager.Instance.PlayGeneralClick();
		Application.Quit();
	}
}
