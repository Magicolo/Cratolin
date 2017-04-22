using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatterElementDying : SplatterComponent {
  private float startT;
    public float TimeToDie;

	public float minGrow = 0;
    public float maxGrow = 1;
    public SplattersGroup CurrentSplatter;

    public override Sprite getSplatter(){
        var t = 1 - (Chronos.Instance.Time - startT) / TimeToDie;
        t = t * (maxGrow - minGrow) + minGrow;
        t = Mathf.Clamp(t,minGrow,maxGrow);
        
        return CurrentSplatter.getSpatter(t);
    }

	void Start () {
        startT = Chronos.Instance.Time;
	}

	void Update () {
		if(TimeToDie != -1 && Chronos.Instance.Time > TimeToDie + startT){
			Destroy(this.gameObject);
		}
	}

}