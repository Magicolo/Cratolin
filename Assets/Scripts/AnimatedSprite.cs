using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{

	public Sprite[] sprites;
	public float frameTimeInterval = 0.1f;

	private int index;
	private float timer;

	void Start()
	{
		index = Random.Range(0, sprites.Length);
		timer = 0;
	}

	void FixedUpdate()
	{
		timer += Chronos.Instance.DeltaTime;

		if (timer > frameTimeInterval)
		{
			timer = 0;
			index++;
			if (index >= sprites.Length)
				index = 0;

			GetComponent<SpriteRenderer>().sprite = sprites[index];
		}

	}
}
