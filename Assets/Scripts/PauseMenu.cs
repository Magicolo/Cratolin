using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public Image content;

    public bool IsShown { get; set; }

    void Start () {
		
	}
	
	void Update () {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            IsShown = !IsShown;
            SoundManager.Instance.PlayGeneralClick();
        }

        CanvasGroup group = GetComponent<CanvasGroup>();

        float dest = IsShown ? 1 : 0;
        group.alpha = Mathf.Lerp(group.alpha, dest, Time.unscaledDeltaTime * 8);

        
        content.gameObject.SetActive(group.alpha > 0.05f);
    }

    public void GoMainMenu()
    {
        SoundManager.Instance.PlayGeneralClick();
        Game.Instance.GoMainMenu();
        //UnityEngine.SceneManagement.SceneManager.LoadScene(0);
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
