using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatnetShatter : MonoBehaviour {
public PlatnetShatterChunk[] ShaderedPrefabs;

	public int ShatterAmountMin;
	public int ShatterAmountMax;

	public bool testDestroy;	
	void Start () {
		
	}
	
	void Update () {
		if(testDestroy){
			testDestroy = false;
			Shatter();
		}
	}
    public void Shatter()
    {
		int amount =  Random.Range(ShatterAmountMin,ShatterAmountMax);
		for (int i = 0; i < amount; i++)
		{
			SpawnPiece();
		}

        Destroy(this.gameObject);
    }

    private void SpawnPiece()
    {
		var newPiece = GameObject.Instantiate(ShaderedPrefabs[Random.Range(0,ShaderedPrefabs.Length-1)]);
		var newPieceGo = newPiece.gameObject;

		Vector3 randomDirection = new Vector3(Random.value, Random.value, Random.value);
		newPiece.Velocity = randomDirection * Random.Range(40,90);
		newPiece.spin = Random.Range(-60f,60f);
		
    }

}