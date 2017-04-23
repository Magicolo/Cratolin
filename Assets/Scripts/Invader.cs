using UnityEngine;

public class Invader : MonoBehaviour
{
	public enum States { Arriving, Charging, Beaming, Waiting, Leaving, Winning }

	public float MoveSpeed = 2f;
	public float StopDistance = 200f;
	public SpriteRenderer NormalRenderer;
	public SpriteRenderer ChargedRenderer;
	public SpriteRenderer BeamRenderer;

	States state;
	float stateTime;
	Vector3 startPosition;
	Vector3 cameraPosition;

	void Awake()
	{
		startPosition = transform.position;
		cameraPosition = Camera.main.transform.position;

		SwitchState(States.Arriving);
	}

	void FixedUpdate()
	{
		stateTime += Chronos.Instance.DeltaTime;
		switch (state)
		{
			case States.Arriving: UpdateArriving(); break;
			case States.Charging: UpdateCharging(); break;
			case States.Beaming: UpdateBeaming(); break;
			case States.Waiting: UpdateWaiting(); break;
			case States.Leaving: UpdateLeaving(); break;
            case States.Winning: UpdateWinning(); break;
        }
	}

	void UpdateArriving()
	{
		var difference = Planet.Instance.Root.position - transform.position;
		var distance = difference.magnitude;

		if (distance <= StopDistance)
        {
            Planet.Instance.Root.GetComponent<Rotator>().enabled = false;
            SwitchState(States.Charging);
        }
			
		else
		{
			transform.position += (difference / distance) * MoveSpeed * Chronos.Instance.DeltaTime;
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(Vector3.left, difference), Chronos.Instance.DeltaTime);
		}
	}

	void UpdateCharging()
	{
		const float duration = 4f;

        if (!Monolith.Instance.IsCompleted)
            Shake(stateTime);

		if (stateTime > duration)
		{
			ChargedRenderer.color = new Color(ChargedRenderer.color.r, ChargedRenderer.color.g, ChargedRenderer.color.b, 1f);
			//BeamRenderer.transform.localScale = new Vector3(BeamRenderer.transform.localScale.x, StopDistance - 65f, BeamRenderer.transform.localScale.z);
            if (Monolith.Instance.IsCompleted)
            {
                Monolith.Instance.spriteRenderer.sprite = Monolith.Instance.win;
                SwitchState(States.Winning);
            }
            else
                SwitchState(States.Beaming);
		}
		else
			ChargedRenderer.color = new Color(ChargedRenderer.color.r, ChargedRenderer.color.g, ChargedRenderer.color.b, stateTime / duration);
	}

	void UpdateBeaming()
	{
		const float duration = 6f;
		Shake(stateTime * duration + 4f);

		if (stateTime > duration)
		{
			WhiteScreen.Instance.Fade(1f);
			Planet.Instance.Destroy();
			SwitchState(States.Waiting);
		}
		else
			WhiteScreen.Instance.Fade(stateTime / duration);
	}

	void UpdateWaiting()
	{
		const float duration = 8f;

		if (stateTime > duration)
		{
			WhiteScreen.Instance.Fade(0f);
			ChargedRenderer.color = new Color(ChargedRenderer.color.r, ChargedRenderer.color.g, ChargedRenderer.color.b, 0f);
			SwitchState(States.Leaving);
		}
		else if (stateTime > duration / 2f)
		{
			float ratio = (stateTime - duration / 2f) / (duration / 2f);
			WhiteScreen.Instance.Fade(1f - ratio);

			if (ratio > 0.5f)
				ChargedRenderer.color = new Color(ChargedRenderer.color.r, ChargedRenderer.color.g, ChargedRenderer.color.b, 1f - ((ratio - 0.5f) * 2f));
		}
	}

	void UpdateLeaving()
	{
		var direction = (startPosition - transform.position).normalized;
		transform.position += direction * MoveSpeed * Chronos.Instance.DeltaTime;
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(Vector3.left, direction), Chronos.Instance.DeltaTime);
	}

	void Shake(float amplitude)
	{
		Camera.main.transform.position = cameraPosition + new Vector3(Random.Range(-amplitude, amplitude), Random.Range(-amplitude, amplitude));
	}

    void UpdateWinning()
    {
        Monolith.Instance.magic.gameObject.SetActive(true);
        Monolith.Instance.magic.position = Vector3.MoveTowards(Monolith.Instance.magic.position, transform.position, Time.deltaTime * 40);

        if (stateTime > 4)
        {
            WinMenu.Instance.IsShown = true;
        }
    }


    void SwitchState(States state)
	{
		this.state = state;

		stateTime = 0f;
		Camera.main.transform.position = cameraPosition;
		BeamRenderer.gameObject.SetActive(state == States.Beaming);
	}
}
