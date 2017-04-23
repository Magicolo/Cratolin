using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfPrefab : MonoBehaviour {

	public GameObject[] ListOfPrefabs;

	public GameObject getRandom(){
		return ListOfPrefabs[(int)Random.Range(0,ListOfPrefabs.Length-1)];
	}

	public T getRandom<T> () where T : Component{
		var go = getRandom();
		return go.GetComponent<T>();
	}
}


