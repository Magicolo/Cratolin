using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public Tree[] treePrefab;
    public Transform parentTreeSpawnPoint;
    public LayerMask planetMask;

    bool loading = false;

	// Use this for initialization
	void Start () {
        PlayerPrefs.DeleteAll();

        for(int i = 0; i < parentTreeSpawnPoint.childCount; i++)
        {
            Transform tr = parentTreeSpawnPoint.GetChild(i);
            var hit = Physics2D.Raycast(tr.position * 2, -tr.position.normalized, 100000f, planetMask);

            
            Tree newTree = Instantiate(treePrefab[Random.Range(0, treePrefab.Length)], hit.point, Quaternion.FromToRotation(Vector2.up, tr.position.normalized), Planet.Instance.Root);
        }

        
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetMouseButtonDown(0) && loading == false)
        {
            loading = true;
            
            Score.Instance.Reset();
            Game.Instance.GoInGame();
            //SceneManager.LoadScene(1);
        }
	}
}
