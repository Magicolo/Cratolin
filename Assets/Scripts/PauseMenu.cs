using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
	public Image content;
	public Button RestartButton;
	public Button Escape;

	public bool IsShown { get; set; }

	void Update()
	{
		Escape.gameObject.SetActive(!IsShown);
		RestartButton.gameObject.SetActive(!IsShown);

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			IsShown = !IsShown;
			SoundManager.Instance.PlayGeneralClick();
		}

		CanvasGroup group = GetComponent<CanvasGroup>();

		float dest = IsShown ? 1 : 0;
		group.alpha = Mathf.Lerp(group.alpha, dest, Time.unscaledDeltaTime * 8);


		content.gameObject.SetActive(group.alpha > 0.05f);
	}

	public void Show()
	{
		IsShown = true;
		SoundManager.Instance.PlayGeneralClick();
	}

	public void GoMainMenu()
	{
		SoundManager.Instance.PlayGeneralClick();
		Game.Instance.GoMainMenu();
	}

	public void Restart()
	{
		SoundManager.Instance.PlayGeneralClick();
		Game.Instance.Reload();
	}

	public void Exit()
	{
		SoundManager.Instance.PlayGeneralClick();
		Application.Quit();
	}

	public void Resume()
	{
		IsShown = !IsShown;
		SoundManager.Instance.PlayGeneralClick();
	}
}
