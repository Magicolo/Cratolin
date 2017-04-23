using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour {

    public static WinMenu Instance { get; private set; }

    public Image content;

    public bool IsShown { get; set; }

    void Start()
    {
        Instance = this;
    }

    void Update()
    {

        CanvasGroup group = GetComponent<CanvasGroup>();

        float dest = IsShown ? 1 : 0;
        group.alpha = Mathf.Lerp(group.alpha, dest, Time.unscaledDeltaTime * 8);


        content.gameObject.SetActive(group.alpha > 0.05f);
    }

    public void Restart()
    {
        Game.Instance.Reload();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
