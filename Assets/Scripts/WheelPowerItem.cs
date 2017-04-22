using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelPowerItem : MonoBehaviour {

    private float currentAlpha = 1;
    private float destAlpha = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.eulerAngles = Vector3.zero;

        currentAlpha = Mathf.Lerp(currentAlpha, destAlpha, Time.unscaledDeltaTime * 8);
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, currentAlpha);
        }
    }

    public void Spawn(Vector3 pPosition)
    {
        PowerBase basePower = GetComponent<PowerBase>();

        if(basePower != null)
            basePower.Create(pPosition);
    }

    public void SetDestinationAlpha(int value)
    {
        destAlpha = value;
    }
}
