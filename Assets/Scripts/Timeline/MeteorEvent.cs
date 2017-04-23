using UnityEngine;

public class MeteorEvent : TimelineEvent
{
	public Vector2 Position;
	public Vector2 Direction = Vector2.right;
	public float Speed = 5f;
	public GameObject Prefab;

	public override void Trigger()
	{
		//var meteor = Instantiate(Prefab, new Vector3(Position.x, Position.y, Prefab.transform.position.z), Quaternion.identity);
		//meteor.Initialize(Position, Direction, Speed);
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.DrawLine(Position, Position + Direction * Speed * 100f);
	}

	protected override void OnValidate()
	{
		base.OnValidate();

		Direction = Direction == Vector2.zero ? Vector2.right : Direction.normalized;
	}
}
