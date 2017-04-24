using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionPoint : MonoBehaviour {

	public float lavaLevel;
	
	void OnDrawGizmos()
	{ 
		Gizmos.color = Color.magenta;
		Gizmos.DrawSphere(transform.position,5);

		/*
		int index = transform.GetSiblingIndex();
		var next = transform.parent.GetChild(0);
		if(index != transform.parent.childCount-1)
			next = transform.parent.GetChild(index + 1);

		var d = Vector2.Distance(transform.position, next.position);
		if( d>29 && d<31)
			Gizmos.color = Color.green;
		else
			Gizmos.color = Color.red;
		
		Gizmos.DrawLine(transform.position, next.position);
		*/
	}
	
	void Start () {
		//lavaLevel = Random.Range(0,0.9f);
	}

	void Update () {

	}

}