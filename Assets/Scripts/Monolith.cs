using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monolith : MonoBehaviour {

    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteSteps;
    public Sprite completed;

    public static Monolith Instance { get; private set; }

    private bool hasEmerged = false;
    private List<Walker> registeredWalker = new List<Walker>();

    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update () {
		if(hasEmerged && spriteRenderer.transform.localPosition.y < 0)
        {
            spriteRenderer.transform.localPosition += new Vector3(0, Time.deltaTime * 4);

            if (spriteRenderer.transform.localPosition.y > 0)
                spriteRenderer.transform.localPosition = Vector3.zero;
        }
	}

    public void Emerge()
    {
        if(!hasEmerged)
        {
            hasEmerged = true;
        }
    }

    public bool HasRegisteredWalker(Walker walker)
    {
        return registeredWalker.Contains(walker);
    }

    public bool IsCompleted
    {
        get { return registeredWalker.Count >= spriteSteps.Length; }
    }

    public void RegisterWalker(Walker walker)
    {
        registeredWalker.Add(walker);

        if (IsCompleted)
            spriteRenderer.sprite = completed;
        else
            spriteRenderer.sprite = spriteSteps[registeredWalker.Count];
    }

    public float AttrackDistance
    {
        get { return (registeredWalker.Count + 1) * 15; }
    }
}
