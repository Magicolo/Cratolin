using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

    public enum States { Spawning, Idle }

    public SpriteRenderer Sprite;
    public float ShakeAmplitude = 5f;
    public float SpawnSpeed = 3f;

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
        var position = Sprite.transform.localPosition;
        position.x = Random.Range(-ShakeAmplitude, ShakeAmplitude);
        position.y += SpawnSpeed * Chronos.Instance.DeltaTime;

        if (position.y >= 0f)
            position = Vector2.zero;

        Sprite.transform.localPosition = position;

        if (position.y >= 0f)
            SwitchState(States.Idle);

    }

    void UpdateIdle()
    {
        
    }

    void SwitchState(States state)
    {
        this.state = state;
    }
}
