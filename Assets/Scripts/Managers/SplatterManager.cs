using System.Linq;
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
			var d = Vector2.Distance(item.transform.position, source);
			if (d < smallestDistance)
			{
				smallestDistance = d;
				sp = item;
			}
		}

		if (sp != null)
		{
			sp.lavaLevel += increase;
		}
	}

	void Update()
	{
		foreach (var lava in SplatterComponent.Splatters["Lava"]
			.Where(l => Groups.Get<Sea>()
				.Any(s => s.Ratio > 0.1f && s.SeaCollider.bounds.Contains(l.transform.position)))
			.ToArray())
			lava.DIE();

		foreach (var grass in SplatterComponent.Splatters["Grass"]
			.Where(g => g.FireKillsMe && SplatterComponent.Splatters["Lava"]
				.Any(l => Vector2.Distance(l.transform.position, g.transform.position) < 0.4f * (g.radiusEffect + l.radiusEffect)))
			.ToArray())
			grass.DIE();

		foreach (var fireable in Groups.Get<FireAbleObject>()
			.Where(f => SplatterComponent.Splatters["Lava"]
				.Any(l => Vector2.Distance(l.transform.position, f.transform.position) < 0.4f * l.radiusEffect)))
			fireable.internalTemperature += Chronos.Instance.DeltaTime * 100f;

		foreach (var walker in Groups.Get<Walker>()
			.Where(w => !w.inFire && SplatterComponent.Splatters["Lava"]
				.Any(l => Vector2.Distance(l.transform.position, w.transform.position) < 0.4f * l.radiusEffect)))
			walker.CatchFire();

		foreach (var seedling in Groups.Get<Seedling>()
			.Where(s => !s.inFire && SplatterComponent.Splatters["Lava"]
				.Any(l => Vector2.Distance(l.transform.position, s.transform.position) < 0.4f * l.radiusEffect)))
			seedling.CatchFire();



		//foreach (var lava in SplatterComponent.Splatters["Lava"])
		//{
		//	//if (Groups.Get<Sea>().Any(s => s.Ratio > 0.1f && s.SeaCollider.bounds.Contains(lava.transform.position)))
		//	//{
		//	//	Todie.Add(lava);
		//	//	continue;
		//	//}



		//	//foreach (var grass in SplatterComponent.Splatters["Grass"])
		//	//{
		//	//	if (!grass.FireKillsMe)
		//	//		continue;
		//	//	var distance = Mathf.Abs((lava.transform.position - grass.transform.position).magnitude);

		//	//	if (distance < 0.4 * (grass.radiusEffect + lava.radiusEffect))
		//	//		Todie.Add(grass);
		//	//}

		//	//foreach (var fireable in Groups.Get<FireAbleObject>())
		//	//{
		//	//	var distance = Mathf.Abs((lava.transform.position - fireable.transform.position).magnitude);

		//	//	if (distance < 0.4 * lava.radiusEffect)
		//	//		fireable.internalTemperature += Chronos.Instance.DeltaTime * 100;
		//	//}

		//	foreach (var item in Groups.Get<Sea>())
		//	{
		//		if (item.Ratio <= 0.1f) continue;
		//		//var sqrDistance = item.SeaCollider.bounds.SqrDistance(lava.transform.position);
		//		if (item.SeaCollider.bounds.Contains(lava.transform.position))
		//			Todie.Add(lava);
		//	}
		//	foreach (var item in Walker.Walkers)
		//	{
		//		var distance = Mathf.Abs((lava.transform.position - item.transform.position).magnitude);

		//		if (distance < 0.4 * lava.radiusEffect)
		//			item.CatchFire();
		//	}
		//	foreach (var item in Groups.Get<Seedling>())
		//	{
		//		var distance = Mathf.Abs((lava.transform.position - item.transform.position).magnitude);

		//		if (distance < 0.4 * lava.radiusEffect)
		//			item.CatchFire();
		//	}
		//}
		//while (Todie.Count != 0)
		//{
		//	var removeMe = Todie[0];
		//	Todie.RemoveAt(0);
		//	removeMe.DIE();
		//}
	}

}