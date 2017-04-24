using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testo : MonoBehaviour
{

	public LayerMask mask;

	void Start () {

	}

	void Update () {
		var hit = Physics2D.Raycast(transform.position, -transform.position, Mathf.Infinity, mask);
		if(hit.collider == null)
			Debug.Log("No hit : ");
		else
			Debug.Log("hit : " + hit.collider.name);
	}

}