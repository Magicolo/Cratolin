using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainPower : PowerBase {

    public LayerMask Mask;
    public RainCloud Prefab;

    public override GameObject Create(Vector2 position)
    {
        if (!CanPlace(position))
            return null;

        RainCloud objRainCloud = Instantiate(Prefab);
        objRainCloud.transform.position = position;

        RaycastHit2D hit = Physics2D.Raycast(objRainCloud.transform.position, Vector3.zero - objRainCloud.transform.position, 10000, Mask);

        Vector2 v2 = (Vector2)objRainCloud.transform.position - Vector2.zero;
        float angle = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;

        objRainCloud.transform.localEulerAngles = new Vector3(0, 0, angle - 90);

        objRainCloud.transform.position = (Vector3)hit.point + objRainCloud.transform.up * objRainCloud.distanceToGround;

        return objRainCloud.gameObject;
    }

    public override bool CanPlace(Vector2 position)
    {
        return true;
    }
}
