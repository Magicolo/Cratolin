using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
	public static Game Instance { get; private set; }

	void Awake()
	{
		Instance = this;
	}

	public void Reload()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
