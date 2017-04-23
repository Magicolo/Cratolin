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
        }

        CanvasGroup group = GetComponent<CanvasGroup>();
        content.gameObject.SetActive(group.alpha > 0);

        float dest = IsShown ? 1 : 0;
        group.alpha = Mathf.Lerp(group.alpha, dest, Time.unscaledDeltaTime * 8);
    }

    public void GoMainMenu()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }
}
