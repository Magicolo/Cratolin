using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSpriteOnAwake : MonoBehaviour {
	
	public SpriteRenderer SpriteRenderer;
	public Sprite[] spriteChoices;
	 
	void Awake(){
		SpriteRenderer.sprite = spriteChoices[Random.Range(0,spriteChoices.Length-1)];
		this.enabled = false;
	}



}