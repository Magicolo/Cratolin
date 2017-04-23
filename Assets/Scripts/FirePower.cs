using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePower : PowerBase {

    public float Cooldown;
    public SpriteRenderer firePreview;

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

        firePreview.gameObject.SetActive(false);

        return null;
    }

    override public void StartPlacing()
    {
        base.StartPlacing();

        firePreview.transform.parent = null;
        firePreview.gameObject.SetActive(true);
    }

    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            firePreview.gameObject.SetActive(false);
        }

        if(firePreview.gameObject.activeInHierarchy)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            firePreview.transform.position = pos.normalized * 365;
        }
        
    }
}
