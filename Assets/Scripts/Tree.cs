using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

    public enum States { Spawning, Idle }

    public SpriteRenderer[] Sprites;
    public float SpawnSpeed = 3f;
    public float spawnDuration = 10f;

    private float fadeTimer = 0;

    States state;

    void Awake()
    {
        SwitchState(States.Spawning);
    }

    void FixedUpdate()
    {
        switch (state)
        {
            case States.Spawning: UpdateSpawning(); break;
            case States.Idle: UpdateIdle(); break;
        }
    }

    void UpdateSpawning()
    {
        var position = Sprites[0].transform.localPosition;
        position.y += SpawnSpeed * Chronos.Instance.DeltaTime;

        if (position.y >= 0f)
            position = Vector2.zero;

        Sprites[0].transform.localPosition = position;

        if (position.y >= 0f)
            SwitchState(States.Idle);

    }

    void UpdateIdle()
    {
        fadeTimer += Time.deltaTime;
        if (fadeTimer < spawnDuration)
        {
            for(int i = 1; i < Sprites.Length; i++)
            {
                int j = i - 1;

                float step = spawnDuration / (float)(Sprites.Length - 1);

                Sprites[i].gameObject.SetActive(fadeTimer > (step * j));
                if(Sprites[i].gameObject.activeInHierarchy)
                {
                    Sprites[i].color = new Color(1, 1, 1, fadeTimer - (step * j) / step);
                }
            }
        }
    }

    void SwitchState(States state)
    {
        this.state = state;
    }
}
