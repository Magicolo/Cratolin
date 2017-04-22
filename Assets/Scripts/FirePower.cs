using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePower : PowerBase {

    public float Cooldown;

    public override bool CanUse
    {
        get { return base.CanUse && Chronos.Instance.Time - lastUse >= Cooldown; }
    }
    public override int RemainingUses
    {
        get { return -1; }
    }

    float lastUse = float.MinValue;

    public override GameObject Create(Vector2 position)
    {
        if (!CanPlace(position))
            return null;

        RaycastHit2D hit = Physics2D.Raycast(position, -position.normalized);

        FireAbleObject[] fireObjects = GameObject.FindObjectsOfType<FireAbleObject>();
        foreach(FireAbleObject fire in fireObjects)
        {
            if(Vector2.Distance(fire.transform.position, hit.point) < 100)
                fire.StartFire();
        }

        return null;
    }
}
