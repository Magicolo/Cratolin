using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
	public static Game Instance { get; private set; }

	public bool IsSandbox { get; set; }

	void Awake()
	{
		if (Instance != null)
			Destroy(gameObject);
		else
		{
			transform.parent = null;
			DontDestroyOnLoad(gameObject);
			Instance = this;
		}
	}

	public void Reload()
	{
		StartCoroutine(LoadRoutine(SceneManager.GetActiveScene().name));
	}

	public void GoInGame()
	{
		StartCoroutine(LoadRoutine("Game"));
	}

	public void GoMainMenu()
	{
		StartCoroutine(LoadRoutine("MainMenu"));
	}

	IEnumerator LoadRoutine(string name)
	{
		const float fadeIn = 0.25f;
		const float fadeOut = 0.25f;

		for (float i = 0; i < fadeIn; i += Chronos.Instance.DeltaTime)
		{
			WhiteScreen.Instance.Fade(i / fadeIn);
			yield return null;
		}

		WhiteScreen.Instance.Fade(1f);
		SceneManager.LoadScene(name);

		for (float i = 0; i < fadeOut; i += Chronos.Instance.DeltaTime)
		{
			WhiteScreen.Instance.Fade(1f - i / fadeIn);
			yield return null;
		}

		WhiteScreen.Instance.Fade(0f);
	}
}
