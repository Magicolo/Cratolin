using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPower : PowerBase {

    public WindSource Prefab;
    public LayerMask Mask;

    public override int RemainingUses
    {
        get
        {
            return -1;
        }
    }

    public override GameObject Create(Vector2 position)
    {
        if (!CanPlace(position)) 
            return null;

        var direction = position.normalized;
        var hit = Physics2D.Raycast(direction * 1000f, -direction, 1000f, Mask);

        if (hit)
        {
            var instance = Instantiate(Prefab, hit.point, Quaternion.FromToRotation(Vector2.up, direction), Planet.Instance.transform);
            return instance.gameObject;
        }
        else
            return null;

    }

    public override bool CanPlace(Vector2 position)
    {
        return true;
    }
}
