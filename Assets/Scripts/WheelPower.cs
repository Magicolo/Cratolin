using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelPower : MonoBehaviour {

    public Transform aimTransform;

    private WheelPowerItem[] items;
    private WheelPowerItem currentItem;
    private float angleOffset = 0;

	// Use this for initialization
	void Start () {
        items = GetComponentsInChildren<WheelPowerItem>();
        aimTransform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(worldPos, Vector2.zero);
            foreach (RaycastHit2D hit in hits)
            {
                WheelPowerItem item = hit.collider.GetComponent<WheelPowerItem>();
                if (item != null && item.GetComponent<PowerBase>().CanUse)
                {
                    currentItem = item;

                    if(currentItem.GetComponent<LoupePower>())
                    {
                        currentItem.GetComponent<LoupePower>().GoInLoop();
                    }

                    foreach(WheelPowerItem powerItem in items)
                    {
                        powerItem.SetDestinationAlpha(powerItem == currentItem ? 1 : 0);
                    }

                    aimTransform.gameObject.SetActive(true);

                    Vector2 v2 = (Vector2)worldPos - Vector2.zero;
                    float angle = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
                    angleOffset = angle;
                }
            }
        }

        if (Input.GetMouseButton(0) && currentItem != null)
        {
            Vector2 v2 = (Vector2)worldPos - Vector2.zero;
            float angle = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
            transform.localEulerAngles = new Vector3(0, 0, angle - angleOffset);

            aimTransform.localEulerAngles = new Vector3(0, 0, angle - 90);

            float wD = Vector2.Distance(worldPos, Vector2.zero);
            float iD = Vector2.Distance(currentItem.transform.position, Vector2.zero);
            float alphaAim = 0f;
            if (wD > iD)
                alphaAim = 0.4f * ((wD - iD) / iD);
            aimTransform.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, alphaAim);
        }

        if (Input.GetMouseButtonUp(0) && currentItem != null)
        {
            if(aimTransform.GetComponentInChildren<SpriteRenderer>().color.a > 0.5f)
            {
                //TODO do power item effect if far away from center
                currentItem.Spawn((worldPos - Vector3.zero) * 10);
            }

            Stop();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //Cancel
            Stop();
        }
    }

    private void Stop()
    {
        currentItem = null;
        transform.localEulerAngles = Vector3.zero;
        aimTransform.gameObject.SetActive(false);

        foreach (WheelPowerItem powerItem in items)
        {
            powerItem.SetDestinationAlpha(1);
        }
    }
}
