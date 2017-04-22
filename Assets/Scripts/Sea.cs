using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sea : MonoBehaviour {

    public float increment;
    public SpriteRenderer sprite;

    private float ratio;

	// Use this for initialization
	void Start () {
        ratio = 0;
        RefreshRatio();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.O))
            IncreaseWater();
        if (Input.GetKeyDown(KeyCode.P))
            ReduceWater();
    }

    public void IncreaseWater()
    {
        ratio += increment;
        
        RefreshRatio();
    }

    public void ReduceWater()
    {
        ratio -= increment;

        RefreshRatio();
    }

    private void RefreshRatio()
    {
        print(ratio);
        ratio = Mathf.Clamp(ratio, 0f, 1f);
        sprite.transform.localScale = new Vector3(sprite.transform.localScale.x, ratio, 1);
    }
}
