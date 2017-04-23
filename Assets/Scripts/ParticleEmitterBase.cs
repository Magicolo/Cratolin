using System.Collections.Generic;
using UnityEngine;

public abstract class ParticleEmitterBase<T> : MonoBehaviour where T : Component
{
	public T[] Prefabs;

	public IEnumerable<T> Particles { get { return particles; } }

	readonly List<T> particles = new List<T>();
	public int ParticleCount{get{return particles.Count;}}
	readonly Stack<T> pool = new Stack<T>();

	public virtual T Spawn()
	{
		var particle = pool.Count > 0 ? pool.Pop() : Instantiate(Prefabs[Random.Range(0, Prefabs.Length)], transform);
		particle.gameObject.SetActive(true);
		particles.Add(particle);

		return particle;
	}

	public virtual void Despawn(T particle)
	{
		particle.transform.parent = transform;
		particle.gameObject.SetActive(false);
		particles.Remove(particle);
		pool.Push(particle);
	}
}
