using UnityEngine;

public class SpinInput : MonoBehaviour
{

	bool clicking;
	Vector3 lastPosition;

	void Start()
	{

	}

	int skip = 0;
	void Update()
	{

		if (!Input.GetMouseButton(0))
		{
			return;
		}
		else if (Input.GetMouseButtonDown(0))
		{
			lastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			return;
		}

		if (skip++ > 4)
			skip = 0;
		else
			return;
		var p = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		var sign = Mathf.Sign(Vector2.Dot(lastPosition, p));
		//var a = Vector3.Angle(lastPosition,p);
		var a2 = Vector2.Angle(new Vector2(p.x, p.y), new Vector2(lastPosition.x, lastPosition.y));
		Debug.Log(a2 * sign);


		lastPosition = p;
	}

}