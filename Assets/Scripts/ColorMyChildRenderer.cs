using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColorMyChildRenderer : MonoBehaviour
{

	public Color Color;
	public bool apply;

	void Update () {
		if(apply){
			foreach (var child in transform.GetComponentsInChildren<SpriteRenderer>())
			{
				child.color = Color;
			}
		}
	}

}