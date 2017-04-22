using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainCloud : MonoBehaviour {

    public float distanceToGround;

	// Use this for initialization
	void Start () {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.zero - transform.position);

        Vector2 v2 = (Vector2)transform.position - Vector2.zero;
        float angle = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;

        transform.localEulerAngles = new Vector3(0, 0, angle - 90);

        transform.position = (Vector3)hit.point + transform.up * distanceToGround;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
