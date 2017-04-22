using System.Collections.Generic;
using UnityEngine;

public abstract class ParticleEmitter<T> : MonoBehaviour where T : Component
{
	public T Prefab;

	readonly Stack<T> pool = new Stack<T>();

	public virtual T Spawn()
	{
		var particle = pool.Count > 0 ? pool.Pop() : Instantiate(Prefab, transform);
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
