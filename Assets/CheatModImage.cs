using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CheatModImage : MonoBehaviour {

	public Image Image;
	public Text controls;


	void Awake()
	{
		SetVisibility(false);
	}
	
	public void SetVisibility(bool visible){
		Image.enabled = visible;
		controls.enabled = visible;
	}
}
