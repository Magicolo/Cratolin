using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAblePowerGiver : MonoBehaviour {

	public PowerManager.Powers Power;

	void Update () {
		//Debug.Log("Nb In Fire : " + FireAbleObject.nbInFire);
		if(PowerManager.Instance.HasPower(PowerManager.Powers.Fire)){
			Destroy(this);
			return;
		}
		
		if(FireAbleObject.nbInFire > GameConstants.Instance.FirePowerNBFirableObjectInFireNeeded){
			
			var p = FindObjectOfType<FireAbleObject>().transform.position;
			foreach (var item in FindObjectsOfType<FireAbleObject>())
			{
				if(item.IsOnFire){
					p = item.transform.position;
					break;
				}
			}
			
			p = p * 1.3f;
			PowerManager.Instance.TrySpawnPower(Power,p);
			Destroy(this);
		}
	}

}