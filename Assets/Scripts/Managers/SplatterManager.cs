using System.Collections.Generic;
using UnityEngine;

public class SplatterManager : MonoBehaviour
{
	private static SplatterManager instance;
	public static SplatterManager Instance
	{
		get
		{
			if (instance == null)
				instance = GameObject.FindObjectOfType<SplatterManager>();
			if (instance == null)
				instance = new GameObject("SplatterManager").AddComponent<SplatterManager>();
			return instance;
		}
	}

	public void Splater(string tag, Vector3 source, int increase)
	{
		SectionPoint sp = null;
		var smallestDistance = float.MaxValue;
		foreach (var item in GameObject.FindObjectsOfType<SectionPoint>())
		{
			var d = Vector2.Distance(item.transform.position,source);
			if(d < smallestDistance){
				smallestDistance = d;
				sp = item;
			}
		}
		
		if(sp != null){
			sp.lavaLevel += increase;
		}
	}


	void Start()
	{

	}

	void Update()
	{
		var Todie = new List<SplatterComponent>();
		foreach (var lava in SplatterComponent.Splatters["Lava"])
		{
			foreach (var grass in SplatterComponent.Splatters["Grass"])
			{
				if (!grass.FireKillsMe)
					continue;
				var distance = Mathf.Abs((lava.transform.position - grass.transform.position).magnitude);

				if (distance < 0.4 * (grass.radiusEffect + lava.radiusEffect))
					Todie.Add(grass);
			}

			foreach (var fireable in Groups.Get<FireAbleObject>())
			{
				var distance = Mathf.Abs((lava.transform.position - fireable.transform.position).magnitude);

				if (distance < 0.4 * lava.radiusEffect)
					fireable.internalTemperature += Chronos.Instance.DeltaTime * 100;
			}

			foreach (var item in Groups.Get<Sea>())
			{
				if(item.Ratio <= 0.1f) continue;
				//var sqrDistance = item.SeaCollider.bounds.SqrDistance(lava.transform.position);
				if (item.SeaCollider.bounds.Contains(lava.transform.position))
					Todie.Add(lava);
			}
			foreach (var item in Walker.Walkers)
			{
				var distance = Mathf.Abs((lava.transform.position - item.transform.position).magnitude);

				if (distance < 0.4 * lava.radiusEffect)
					item.CatchFire();
			}
			foreach (var item in Groups.Get<Seedling>())
			{
				var distance = Mathf.Abs((lava.transform.position - item.transform.position).magnitude);

				if (distance < 0.4 * lava.radiusEffect)
					item.CatchFire();
			}
		}
		while (Todie.Count != 0)
		{
			var removeMe = Todie[0];
			Todie.RemoveAt(0);
			removeMe.DIE();
		}
	}

}