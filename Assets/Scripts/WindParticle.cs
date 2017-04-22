using System.Collections;
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

    public float Direction { get { return direction; } set { direction = value; } }

	// Use this for initialization
	void Start () {
        lifeTimer = Random.Range(0f, 10f);
        waveAmplitude = Random.Range(0f, 25f);

        GetComponent<TrailRenderer>().sortingOrder = -5;
        GetComponent<TrailRenderer>().Clear();

        // Not all particles have trails
        if (Random.Range(0, 5) > 0)
            Destroy(GetComponent<TrailRenderer>());
    }
	
	// Update is called once per frame
	void Update () {

        // Destroy on hitting ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 1000f, maskPlanet);
        if(hit.collider != null)
        {
            // Small chance to spawn tree if polenized
            if(polenized && Random.Range(0, 5) == 0)
            {
                Tree tree = Instantiate(TreePrefab, transform.position, Quaternion.FromToRotation(Vector2.up, transform.position.normalized), Planet.Instance.transform);
            }  

            Destroy(gameObject);
            return;
        }

        RaycastHit2D hitTree = Physics2D.Raycast(transform.position, Vector2.zero);
        if (hitTree.collider != null && hitTree.collider.GetComponentInParent<Tree>() != null)
        {
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
