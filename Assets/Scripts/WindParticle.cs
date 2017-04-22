﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindParticle : MonoBehaviour {

    public LayerMask maskPlanet;
    public float distanceFormPlanetCenter;
    public float moveSpeed;
    public Tree TreePrefab;

    private float direction = 1;
    private float lifeTimer;
    private float waveAmplitude;
    private bool polenized = false;
    private bool inFire = false;
    private bool disabled = false;

    public float Direction { get { return direction; } set { direction = value; } }

	void Start () {
        lifeTimer = Random.Range(0f, 10f);
        waveAmplitude = Random.Range(0f, 15f);

        GetComponent<TrailRenderer>().sortingOrder = -5;
        GetComponent<TrailRenderer>().Clear();

        moveSpeed = moveSpeed * Random.Range(0.8f, 1.2f);

        //// Not all particles have trails
        //if (Random.Range(0, 5) > 0)
        //    Destroy(GetComponent<TrailRenderer>());
    }
	
	void Update () {

        if (disabled)
            return;

        // Destroy on hitting ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 1000f, maskPlanet);
        if(hit.collider != null)
        {
            // Small chance to spawn tree if polenized
            if(polenized)// && Random.Range(0, 5) == 0)
            {
                Tree tree = Instantiate(TreePrefab, transform.position, Quaternion.FromToRotation(Vector2.up, transform.position.normalized), Planet.Instance.transform);
            }  

            Destroy(gameObject, 5);
            disabled = true;
            return;
        }

        RaycastHit2D hitTree = Physics2D.Raycast(transform.position, Vector2.zero);
        if (hitTree.collider != null && hitTree.collider.GetComponentInParent<Tree>() != null)
        {
            if (inFire && hitTree.collider.GetComponentInParent<FireAbleObject>() != null && !hitTree.collider.GetComponentInParent<FireAbleObject>().IsOnFire)
            {
                // propagate fire particle on other tree;
                hitTree.collider.GetComponentInParent<FireAbleObject>().StartFire();

            }
            else if (hitTree.collider.GetComponentInParent<FireAbleObject>() != null && hitTree.collider.GetComponentInParent<FireAbleObject>().IsOnFire)
            {
                inFire = true;
                polenized = false;
            }  
            else if(!inFire)
                polenized = true;
        }

        lifeTimer += Time.deltaTime;

        transform.up = transform.position.normalized;
        transform.position = transform.up * (distanceFormPlanetCenter + Mathf.Sin(lifeTimer * 4) * waveAmplitude);

        transform.position += transform.right * Time.deltaTime * moveSpeed * Direction;

        // go more and more closer to the planet
        distanceFormPlanetCenter -= Time.deltaTime * 5;

    }
}