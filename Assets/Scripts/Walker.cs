using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour {

    public float distanceToGround;
    public LayerMask WalkerCollision;
    public Transform Visual;
    public GameObject walkBase;
    public GameObject walkEvolved;
    public GameObject scared;
    public GameObject idle;
    public GameObject lookAround;

    private bool isGoingRight = true;
    private float timeSinceLastFear = float.MinValue;
    private float timeSpawn;
    private bool isWalking = true;

    private float lookAroundTimer = 1000;

    public bool IsEvolved { get { return Time.time - timeSpawn > 2; } }
    public bool ReachedGoal { get; set; }

    // Use this for initialization
    void Start () {
        transform.parent = Planet.Instance.transform;
        timeSpawn = Time.time;

        lookAroundTimer = Random.Range(0f, 5f);

    }

    public void Fear(Vector3 pPosition)
    {
        if(!ReachedGoal)
        {
            timeSinceLastFear = Time.time;

            bool fearLeftOf = (Vector2.Angle(transform.right, pPosition)) > 90 && (Vector2.Angle(transform.right, pPosition)) < 180;

            if (fearLeftOf)
                isGoingRight = false;
            else
                isGoingRight = true;
        }
    }

    public float CurrentSpeed
    {
        get
        {
            float speed = 20;

            //to compensate for planet rotation
            if (isGoingRight)
                speed *= 2;

            if (Time.time - timeSinceLastFear < 3)
                speed *= 3;

            return speed;
        }
    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        isWalking = !ReachedGoal;

        

        // stick to ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 100000, WalkerCollision);
        if (hit.collider != null)
        {
            Collider2D col = hit.collider;
            transform.up = hit.normal;
            transform.position = hit.point + hit.normal * distanceToGround;
        }

        // walk in current direction, left or right
        if (isWalking)
        {
            

            RaycastHit2D hitLeftRight = Physics2D.Raycast(transform.position + transform.up * distanceToGround, transform.right, distanceToGround * 2, WalkerCollision);
            bool canGoRight = (hitLeftRight.collider == null);
            hitLeftRight = Physics2D.Raycast(transform.position + transform.up * distanceToGround, -transform.right, distanceToGround * 2, WalkerCollision);
            bool canGoLeft = (hitLeftRight.collider == null);


            if (isGoingRight && canGoRight)
                transform.localPosition += transform.right * Time.deltaTime * CurrentSpeed;
            else if (isGoingRight && !canGoRight)
                isGoingRight = false;
            else if (!isGoingRight && canGoLeft)
                transform.localPosition -= transform.right * Time.deltaTime * CurrentSpeed;
            else if (!isGoingRight && !canGoLeft)
                isGoingRight = true;
        }
        

        UpdateSprite();

        if(IsEvolved)
        {
            Monolith.Instance.Emerge();

            if(!ReachedGoal && !Monolith.Instance.IsCompleted && Vector2.Distance(Monolith.Instance.transform.position, transform.position) < Monolith.Instance.AttrackDistance)
            {
                ReachedGoal = true;
                transform.parent = Planet.Instance.Root;
                Monolith.Instance.RegisterWalker(this);
            }
        }
    }

    private void UpdateSprite()
    {
        Visual.transform.localScale = new Vector3((isGoingRight ? -1 : 1), 1, 1);

        if (ReachedGoal)
        {
            lookAroundTimer -= Time.deltaTime;

            if (lookAroundTimer < 0)
                lookAroundTimer = Random.Range(4f, 10f);
        }

        walkBase.SetActive(!IsEvolved);
        walkEvolved.SetActive(IsEvolved && isWalking);
        
        lookAround.SetActive(IsEvolved && !isWalking && lookAroundTimer < 1f);
        idle.SetActive(IsEvolved && !isWalking && !lookAround.activeInHierarchy);
    }
}
