using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sea : MonoBehaviour {

    public float increment;
    public SpriteRenderer sprite;
    public Tree treePrefab;
    public float timeBetweenTreeSpawn;
    public int maxTreeCountEachSide;
    public LayerMask planetMask;

    private float ratio;
    private float lastTimeSpawnedTree;
    public List<Tree> lstTreesSpawnedLeft = new List<Tree>();
    public List<Tree> lstTreesSpawnedRight = new List<Tree>();

    // Use this for initialization
    void Start () {
        ratio = 0;
        RefreshRatio();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.O))
            IncreaseWater();
        if (Input.GetKeyDown(KeyCode.P))
            ReduceWater();

        if(ratio > 0.5f && Time.time - lastTimeSpawnedTree > timeBetweenTreeSpawn)
        {
            lastTimeSpawnedTree = Time.time;

            bool spawnLeft = Random.Range(0, 2) == 0;
            List<Tree> lstListToSpawnIn = null;
            Vector3 spawnPos = transform.position;
            if(spawnLeft && lstTreesSpawnedLeft.Count < maxTreeCountEachSide)
            {
                lstListToSpawnIn = lstTreesSpawnedLeft;
                spawnPos = new Vector3(GetComponent<BoxCollider2D>().bounds.min.x, GetComponent<BoxCollider2D>().bounds.max.y);
                spawnPos += -transform.right * Random.Range(40, 60) * lstListToSpawnIn.Count;
            }
            else if (!spawnLeft && lstTreesSpawnedRight.Count < maxTreeCountEachSide)
            {
                lstListToSpawnIn = lstTreesSpawnedRight;
                spawnPos = new Vector3(GetComponent<BoxCollider2D>().bounds.max.x, GetComponent<BoxCollider2D>().bounds.max.y);
                spawnPos += transform.right * Random.Range(40, 60) * lstListToSpawnIn.Count;
            }

            if(lstListToSpawnIn != null)
            {
                var direction = spawnPos.normalized;
                var hit = Physics2D.Raycast(direction * 1000f, -direction, 1000f, planetMask);

                if (hit)
                {
                    Tree newTree = Instantiate(treePrefab, hit.point, Quaternion.FromToRotation(Vector2.up, direction), Planet.Instance.transform);
                    lstListToSpawnIn.Add(newTree);
                }
            }
        }
        
    }

    public void IncreaseWater()
    {
        ratio += increment;
        
        RefreshRatio();
    }

    public void ReduceWater()
    {
        ratio -= increment;

        RefreshRatio();
    }

    private void RefreshRatio()
    {
        ratio = Mathf.Clamp(ratio, 0f, 1f);
        sprite.transform.localScale = new Vector3(sprite.transform.localScale.x, ratio, 1);
    }
}
