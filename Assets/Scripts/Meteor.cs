using UnityEngine;

public class Meteor : MonoBehaviour
{
	public Rigidbody2D Body;

	Vector2 direction;
	float speed;

	public void Initialize(Vector2 position, Vector2 direction, float speed)
	{
		transform.position = new Vector3(position.x, position.y, transform.position.z);
		this.direction = direction;
		this.speed = speed;
	}

	void FixedUpdate()
	{
		Body.position += direction * speed;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		Destroy(gameObject);
	}
}
