using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour {

    public float distanceToGround;

    private bool isGoingRight = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //transform.position += transform.right * Time.deltaTime;

        // stick to ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up /*Vector3.zero - transform.position*/);
        if(hit.collider != null)
        {
            Collider2D col = hit.collider;
            transform.up = hit.normal;
            transform.position = hit.point + hit.normal * distanceToGround;
        }

        // walk in current direction, left or right
        RaycastHit2D hitLeftRight = Physics2D.Raycast(transform.position + transform.up * distanceToGround, transform.right, distanceToGround * 2);
        bool canGoRight = (hitLeftRight.collider == null);
        hitLeftRight = Physics2D.Raycast(transform.position + transform.up * distanceToGround, -transform.right, distanceToGround * 2);
        bool canGoLeft = (hitLeftRight.collider == null);


        if (isGoingRight && canGoRight)
            transform.position += transform.right * Time.deltaTime;
        else if (isGoingRight && !canGoRight)
            isGoingRight = false;
        else if (!isGoingRight && canGoLeft)
            transform.position -= transform.right * Time.deltaTime;
        else if (!isGoingRight && !canGoLeft)
            isGoingRight = true;
    }
}
