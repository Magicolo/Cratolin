using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoupePower : PowerBase {

    public GameObject RenderTarget;
    public Transform cameraTransform;

    private bool inLoupe;

    public override bool CanUse
    {
        get { return base.CanUse; }
    }
    public override int RemainingUses
    {
        get { return -1; }
    }

    public override GameObject Create(Vector2 position)
    {
        if (!CanPlace(position))
            return null;

        inLoupe = false;
        RenderTarget.SetActive(false);

        return null;
    }

    public void GoInLoop()
    {
        inLoupe = true;
        RenderTarget.SetActive(true);
    }

    void Update()
    {
        if(inLoupe)
        {
            cameraTransform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
