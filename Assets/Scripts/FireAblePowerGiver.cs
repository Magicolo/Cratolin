using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAblePowerGiver : MonoBehaviour {

	public GameObject PowerPrefab;

	void Update () {
		/*if(PowerManager.Instance.HasPower(PowerManager.Powers.Fire)){
			Destroy(this);
			return;
		}*/
		
		if(FireAbleObject.nbInFire > GameConstants.Instance.FirePowerNBFirableObjectInFireNeeded){
			var newPower = Instantiate(PowerPrefab);
			var p = FindObjectOfType<FireAbleObject>().transform.position;
			foreach (var item in FindObjectsOfType<FireAbleObject>())
			{
				if(item.IsOnFire){
					p = item.transform.position;
					break;
				}
			}
			
			newPower.transform.position = p * 1.3f;
			Destroy(this);
		}
	}

}