using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPower : PowerBase {

    public WindSource Prefab;
    public LayerMask Mask;
    public SpriteRenderer windPreview;

    private bool inPreview;

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

        inPreview = false;
        StartCoroutine("WindPreviewAnimate");

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

    private IEnumerator WindPreviewAnimate()
    {
        yield return new WaitForSeconds(2);

        while(windPreview.color.a > 0)
        {
            windPreview.color = new Color(1, 1, 1, windPreview.color.a - Time.deltaTime);
            yield return null;
        }

        windPreview.gameObject.SetActive(false);
    }

    public override bool CanPlace(Vector2 position)
    {
        return true;
    }

    override public void StartPlacing()
    {
        base.StartPlacing();

        StopCoroutine("WindPreviewAnimate");
        inPreview = true;
        windPreview.transform.parent = null;
        windPreview.gameObject.SetActive(true);
        windPreview.color = Color.white;
    }

    void Update()
    {
        if (inPreview)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            windPreview.transform.position = pos.normalized * 370;
        }

    }
}
