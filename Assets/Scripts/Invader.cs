using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
	public enum States { Arriving, Charging, Beaming, Waiting, Leaving, Winning, Idle }

	public float MoveSpeed = 2f;
	public float StopDistance = 200f;
	public SpriteRenderer NormalRenderer;
	public SpriteRenderer ChargedRenderer;
	public SpriteRenderer BeamRenderer;
	public GameObject MonolothMagicParticlesPrefab;
	public GameObject chunksParent;
	public ParticleSystem particleSystemBossDies;
	public AudioClip exploseAudio;
	public AudioClip laserAudio;

	States state;
	float stateTime;
	Vector3 startPosition;
	Vector3 cameraPosition;

	private float lastTimeSpawnMagic;

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
			case States.Idle: UpdatIdle(); break;
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
		const float duration = 2f;

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
			{
				SwitchState(States.Beaming);
				SoundManager.Instance.PlaySound(laserAudio);
			}

		}
		else
			ChargedRenderer.color = new Color(ChargedRenderer.color.r, ChargedRenderer.color.g, ChargedRenderer.color.b, stateTime / duration);
	}

	void UpdateBeaming()
	{
		const float duration = 3f;
		Shake(stateTime * duration + 2f);

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
		const float duration = 3f;

		if (stateTime > duration)
		{
			WhiteScreen.Instance.Fade(0f);
			ChargedRenderer.color = new Color(ChargedRenderer.color.r, ChargedRenderer.color.g, ChargedRenderer.color.b, 0f);
			SwitchState(States.Idle);
		}
		else if (stateTime > duration / 2f)
		{
			float ratio = (stateTime - duration / 2f) / (duration / 2f);
			WhiteScreen.Instance.Fade(1f - ratio);

			if (ratio > 0.5f)
				ChargedRenderer.color = new Color(ChargedRenderer.color.r, ChargedRenderer.color.g, ChargedRenderer.color.b, 1f - ((ratio - 0.5f) * 2f));
		}
	}

	void UpdatIdle(){
		
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

	int countParticles = 0;
	bool explosed = false;
	void UpdateWinning()
	{
		Monolith.Instance.magic.gameObject.SetActive(true);
		Monolith.Instance.magic.position = Vector3.MoveTowards(Monolith.Instance.magic.position, transform.position, Chronos.Instance.DeltaTime * 40);

		float RandomAmp = Random.Range(7f, 12f);
		if (Chronos.Instance.Time - lastTimeSpawnMagic > 0.3f)
		{
			countParticles++;
			lastTimeSpawnMagic = Chronos.Instance.Time;
			GameObject obj = Instantiate(MonolothMagicParticlesPrefab);
			obj.transform.position = Monolith.Instance.magic.position;
			obj.gameObject.SetActive(true);

			List<Vector3> lstPoints = new List<Vector3>();
			Vector3 source = Monolith.Instance.magic.position;
			Vector3 dest = transform.position;
			for (int i = 1; i < 6; i++)
			{
				Vector3 basePos = source + i * ((dest - source) / 7);
				lstPoints.Add(basePos + Monolith.Instance.transform.right * ((i % 2) * 2 - 1) * ((countParticles % 2) * 2 - 1) * RandomAmp);
			}
			lstPoints.Add(dest);

			iTween.MoveTo(obj, iTween.Hash("path", lstPoints.ToArray(), "time", 3f));
		}

		if (stateTime > 4 && !explosed)
		{
			// explose boss
			explosed = true;
			SoundManager.Instance.PlaySound(exploseAudio);
			particleSystemBossDies.Play();
			NormalRenderer.gameObject.SetActive(false);
			ChargedRenderer.gameObject.SetActive(false);
			chunksParent.SetActive(true);
			for (int i = 0; i < chunksParent.transform.childCount; i++)
			{
				Transform tr = chunksParent.transform.GetChild(i);
				Vector3 pos = tr.position - (transform.position + new Vector3(-80, -80));
				iTween.MoveTo(tr.gameObject, iTween.Hash("time", 10, "position", pos * 4));
			}

			PowerManager.Instance.TrySpawnPower(PowerManager.Powers.Seed, transform.position);
		}

		if (stateTime > 8 && Groups.Get<PowerCollectable>().Count == 0)
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
