using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour {

    public float distanceToGround;
    public LayerMask WalkerCollision;
    public Transform Visual;

    private bool isGoingRight = false;
    private float timeSinceLastFear;

	// Use this for initialization
	void Start () {
		
	}

    public void Fear()
    {
        timeSinceLastFear = Time.time;
    }

    public float CurrentSpeed
    {
        get
        {
            float speed = 1;
            if (Time.time - timeSinceLastFear < 3)
                speed *= 3;

            return speed;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        // stick to ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 100000, WalkerCollision);
        if(hit.collider != null)
        {
            Collider2D col = hit.collider;
            transform.up = hit.normal;
            transform.position = hit.point + hit.normal * distanceToGround;
        }

        // walk in current direction, left or right
        RaycastHit2D hitLeftRight = Physics2D.Raycast(transform.position + transform.up * distanceToGround, transform.right, distanceToGround * 2, WalkerCollision);
        bool canGoRight = (hitLeftRight.collider == null);
        hitLeftRight = Physics2D.Raycast(transform.position + transform.up * distanceToGround, -transform.right, distanceToGround * 2, WalkerCollision);
        bool canGoLeft = (hitLeftRight.collider == null);


        if (isGoingRight && canGoRight)
            transform.position += transform.right * Time.deltaTime * CurrentSpeed;
        else if (isGoingRight && !canGoRight)
            isGoingRight = false;
        else if (!isGoingRight && canGoLeft)
            transform.position -= transform.right * Time.deltaTime * CurrentSpeed;
        else if (!isGoingRight && !canGoLeft)
            isGoingRight = true;

        UpdateSprite();
    }

    private void UpdateSprite()
    {
        Visual.transform.localScale = new Vector3((isGoingRight ? -1 : 1), 1, 1);
    }
}
