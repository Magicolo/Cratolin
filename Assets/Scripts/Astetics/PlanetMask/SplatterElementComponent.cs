
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatterElementComponent : MonoBehaviour {
    private float startT;
    public float TimeToFullGrow;
    public float minGrow = 0;
    public float maxGrow = 1;
    public SplattersGroup[] SplattersRandom;
    private SplattersGroup CurrentSplatter;

    public Sprite getSplatter(){
        var t = (Chronos.Instance.Time - startT) / TimeToFullGrow;
        t = t * (maxGrow - minGrow) + minGrow;
        t = Mathf.Clamp(t,minGrow,maxGrow);
        
        return CurrentSplatter.getSpatter(t);
    }

    void Start () {
        startT = Chronos.Instance.Time;
        CurrentSplatter = SplattersRandom[Random.Range(0,SplattersRandom.Length-1)];
    }

    void Update () {

    }

}