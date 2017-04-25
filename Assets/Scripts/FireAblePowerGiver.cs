using System.Linq;
using UnityEngine;

public class FireAblePowerGiver : MonoBehaviour
{

	public PowerManager.Powers Power;

	void Update()
	{
		//Debug.Log("Nb In Fire : " + FireAbleObject.nbInFire);
		if (PowerManager.Instance.HasPower(PowerManager.Powers.Fire))
		{
			Destroy(this);
			return;
		}

		var fireTransforms = Groups.Get<Seedling>()
			.Where(s => s.inFire)
			.Select(s => s.transform)
			.Concat(Groups.Get<FireAbleObject>()
				.Where(f => f.IsOnFire)
				.Select(f => f.transform)
				.Concat(
					Groups.Get<Walker>()
						.Where(w => w.inFire)
						.Select(w => w.transform)))
			.ToArray();
		if (fireTransforms.Length > GameConstants.Instance.FirePowerNBFirableObjectInFireNeeded)
		{
			PowerManager.Instance.TrySpawnPower(Power, fireTransforms.FirstOrDefault().position * 1.3f);
			Destroy(this);
		}
	}

}