﻿using System.Collections.Generic;
using UnityEngine;

public class SmokeCloudParticle : ParticleBase
{
	public static List<SmokeCloudParticle> Clouds = new List<SmokeCloudParticle>();

	SmokeCloudEmitter emitter;
	float moveSpeed;

	public void Initialize(SmokeCloudEmitter emitter, Vector3 position, float moveSpeed, float lifeTime = 75f, float fadeIn = 5f, float fadeOut = 4f)
	{
		base.Initialize(position, Vector2.zero, 1f, lifeTime, fadeIn, fadeOut);

		this.emitter = emitter;
		this.fadeIn = fadeIn;
		this.moveSpeed = moveSpeed;
		Clouds.Add(this);
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		if (hasFaded)
			transform.RotateAround(Planet.Instance.Root.position, Vector3.forward, moveSpeed * Chronos.Instance.DeltaTime);
	}

	protected override void Despawn()
	{
		Clouds.Remove(this);
		emitter.Despawn(this);
	}
}
