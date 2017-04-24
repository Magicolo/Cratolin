using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSeparator : MonoBehaviour {

	public float distanceBetweenPoints;

	void Start () {
		PolygonCollider2D p2d = GetComponent<PolygonCollider2D>();

		Spawn(p2d.points[0]);
		var distanceToDo = distanceBetweenPoints;

		int ptd = 150;
		var lastP = p2d.points[0];
		foreach (var nextP in p2d.points)
		{
			var d = Vector2.Distance(nextP,lastP);
			if(distanceToDo - d > 0){
				distanceToDo -= d;
			}else{
				while(distanceToDo - d <= 0){
					var midT = distanceToDo / Vector2.Distance(lastP,nextP);
					
					var midP = lastP + (nextP - lastP).normalized * distanceToDo;
					
					Spawn(midP);
					distanceToDo = distanceBetweenPoints;
					lastP = midP;
					
					d = Vector2.Distance(nextP,lastP) ;
					if(	ptd-- <= 0 )
						return;
					
				}
				distanceToDo -= d;
			}
			
			lastP = nextP;
		}
	}

	private void Spawn(Vector2 p)
	{
		var go = new GameObject("Point");
		go.transform.position = p;
		go.AddComponent<SectionPoint>();
		go.transform.parent = transform;
	}

	void Update () {

	}

}