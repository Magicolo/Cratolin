﻿using UnityEngine;

public class MeteorParticle : ParticleBase
{
	public SplatterComponent Splater;

	MeteorEmitter emitter;
	public GameObject SubEmitterPrefab;

	public void Initialize(MeteorEmitter emitter, Vector3 position, Vector2 velocity)
	{
		base.Initialize(position, velocity, 1f, 30f, 0.1f, 1f);

		this.emitter = emitter;
	}

	protected override void Despawn()
	{
		if (Splater != null)
		{
			var splat = Instantiate(Splater, transform.position, transform.rotation);
			splat.transform.parent = Planet.Instance.Root;
		}
		if(SubEmitterPrefab != null){
			var splat = Instantiate(SubEmitterPrefab, transform.position, transform.rotation);
			splat.transform.parent = Planet.Instance.Root;
		}

		emitter.Despawn(this);
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		Despawn();

        //if(collision.gameObject.GetComponent<Walker>() != null)
        //{
        //    collision.gameObject.GetComponent<Walker>().CatchFire();
        //}

        Walker[] walkers = FindObjectsOfType<Walker>();
        foreach (Walker walker in walkers)
        {
            if (Vector2.Distance(walker.transform.position, transform.position) < 15)
            {
                walker.CatchFire();
            }
        }
    }

    void Update()
    {
        if(WheelPower.Instance.IsPlacingPower)
        {
            float angleThis = Mathf.Rad2Deg * Mathf.Atan2(transform.position.y, transform.position.x);

            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float angleMouse = Mathf.Rad2Deg * Mathf.Atan2(worldPos.y, worldPos.x);

            if (Mathf.Abs(angleThis - angleMouse) < 1)
                Despawn();
        }
        
    }
}
