using System.Collections.Generic;
using UnityEngine;

public abstract class ParticleEmitterBase<T> : MonoBehaviour where T : Component
{
	public T[] Prefabs;

	readonly Stack<T> pool = new Stack<T>();

	public virtual T Spawn()
	{
		var particle = pool.Count > 0 ? pool.Pop() : Instantiate(Prefabs[Random.Range(0, Prefabs.Length)], transform);
		particle.gameObject.SetActive(true);

		return particle;
	}

	public virtual void Despawn(T particle)
	{
		particle.transform.parent = transform;
		particle.gameObject.SetActive(false);
		pool.Push(particle);
	}
}
