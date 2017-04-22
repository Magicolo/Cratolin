using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SplattersGroup : ScriptableObject {

    public Sprite[] masks;

    public Sprite getSpatter(float t){
        if(masks.Length == 0){
            Debug.LogError(this.name + " a pas de Sprite you NOOB !!");
        }
        if(t<0)
            return null;
        if(t >= 1)
            return masks[masks.Length-1];
        else
            return masks[(int)(t*masks.Length)];
    }
}