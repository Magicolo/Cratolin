using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatterManager : MonoBehaviour {

	private static SplatterManager instance;
	public static SplatterManager Instance {
		get{
			if(instance == null)
				instance = GameObject.FindObjectOfType<SplatterManager>();
			if(instance == null)
				instance = new GameObject("SplatterManager").AddComponent<SplatterManager>();
			return instance;
		}  
	}
	void Start () {

	}

	void Update () {
		List<SplatterComponent> Todie = new List<SplatterComponent>();
		foreach (var lava in SplatterComponent.Splatters["Lava"])
		{
			foreach (var grass in SplatterComponent.Splatters["Grass"])
			{
				var distance = Mathf.Abs((lava.transform.position - grass.transform.position).magnitude);
				
				if(distance < 0.4 * (grass.radiusEffect + lava.radiusEffect)){
					Todie.Add(grass);
				}
			}
			foreach (var fireable in FireAbleObject.FireAbles)
			{
				var distance = Mathf.Abs((lava.transform.position - fireable.transform.position).magnitude);
				
				if(distance < 0.4 * lava.radiusEffect){
					fireable.internalTemperature += Chronos.Instance.DeltaTime * 100;
				}
			}

			foreach (var item in Walker.Walkers)
			{
				var distance = Mathf.Abs((lava.transform.position - item.transform.position).magnitude);
				
				if(distance < 0.4 * lava.radiusEffect){
					item.CatchFire();
				}
			}
		}
		while(Todie.Count != 0){
			var removeMe = Todie[0];
			Todie.RemoveAt(0);
			removeMe.DIE();
		}


		/*foreach (var lava in SplatterComponent.Splatters["Lava"])
		{
			
		}*/
	}

}