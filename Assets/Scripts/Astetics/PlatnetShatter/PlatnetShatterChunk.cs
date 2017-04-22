using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatnetShatterChunk : MonoBehaviour {

    public Vector2 Velocity;
    public float spin;

    void Start () {

    }

    void Update () {
        transform.Translate(Velocity * Time.deltaTime);
        transform.Rotate(new Vector3(0,0,spin) * Time.deltaTime);
    }

}