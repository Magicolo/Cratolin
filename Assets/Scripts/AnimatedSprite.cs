using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSprite : MonoBehaviour {

    public Sprite[] sprites;
    public float frameTimeInterval = 0.1f;

    private int index;
    private float timer;

	// Use this for initialization
	void Start () {
        index = Random.Range(0, sprites.Length);
        timer = 0;
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if(timer > frameTimeInterval)
        {
            timer = 0;
            index++;
            if (index >= sprites.Length)
                index = 0;

            GetComponent<SpriteRenderer>().sprite = sprites[index];
        }

    }
}
