using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelPowerItem : MonoBehaviour {

    public GameObject grayedOutSprite;

    private float currentAlpha = 1;
    private float destAlpha = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.eulerAngles = Vector3.zero;

        currentAlpha = Mathf.Lerp(currentAlpha, destAlpha, Time.unscaledDeltaTime * 8);
        Renderer[] sprites = GetComponentsInChildren<Renderer>();
        foreach (Renderer sprite in sprites)
        {
            sprite.material.color = new Color(sprite.material.color.r, sprite.material.color.g, sprite.material.color.b, currentAlpha);
        }

        int uses = GetComponent<PowerBase>().RemainingUses;
        GetComponentInChildren<TextMesh>().text = uses == -1 ? "" : uses.ToString();

        grayedOutSprite.SetActive(!GetComponent<PowerBase>().CanUse);
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
