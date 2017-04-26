using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

	//Singleton instance
	private static SoundManager _instance;
	public static SoundManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<SoundManager>();
			}
			return _instance;
		}
	}

	private Transform cameraTransform;
	private GameObject lastOneShotAudioCreated;

	private float lastTimeAudioPlayed;
	private string lastAudioPlayed;

	public AudioClip generalSuccessAudio;
	public AudioClip generalErrorAudio;
	public AudioClip generalKeypadAudio;
	public AudioClip generalDoorOpeningAudio;
	public AudioClip generalClick;

	void Start()
	{
		cameraTransform = Camera.main.transform;
	}

	public float PlaySound(string pResourceName, float volume = 1f)
	{
		AudioClip soundClip = Load(pResourceName);
		if (soundClip == null)
		{
			Debug.LogError("SoundManager : Couldn't find audio resource (" + pResourceName + ")");
			return 0;
		}
		else
		{
			return ExecuteSound(soundClip, false, Vector3.zero, volume);
		}
	}

	public float PlayPitchAudio(string pResourceName, float volume = 1f)
	{
		AudioClip soundClip = Load(pResourceName);
		GameObject soundGameObject = PlayClipAt(soundClip, Vector3.zero, volume);
		soundGameObject.transform.parent = cameraTransform;
		soundGameObject.transform.localPosition = Vector3.zero;
		soundGameObject.GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(0.8f, 1.2f);
		soundGameObject.GetComponent<AudioSource>().volume = 0.5f;
		return 0;
	}

	private IEnumerator PlayLoadedAudioWhenReady(AudioClip pClip, float volume = 1f)
	{
		while (pClip.isReadyToPlay == false)
		{
			yield return new WaitForSeconds(0.05f);
		}

		ExecuteSound(pClip, false, Vector3.zero, volume);
	}

	public float PlaySound(AudioClip pSoundClip, float volume = 1 )
	{
		return ExecuteSound(pSoundClip, false, Vector3.zero, volume);
	}

	public float PlaySoundAt(string pResourceName, Vector3 pSoundPosition, float volume = 1)
	{
		AudioClip soundClip = Load(pResourceName);
		if (soundClip == null)
		{
			Debug.LogError("SoundManager : Couldn't find audio resource (" + pResourceName + ")");
			return 0;
		}
		else
		{
			return ExecuteSound(soundClip, true, pSoundPosition, volume);
		}
	}

	public float PlaySoundAt(AudioClip pSoundClip, Vector3 pSoundPosition, float volume = 1)
	{
		return ExecuteSound(pSoundClip, true, pSoundPosition, volume);
	}

	public float PlaySoundSuccess(bool pIsSuccess)
	{
		AudioClip clip = generalSuccessAudio;

		if (pIsSuccess == false) clip = generalErrorAudio;

		return PlaySound(clip);
	}

	public float KeypadKeyPressSound()
	{
		AudioClip clip = generalKeypadAudio;
		return PlaySound(clip);
	}

	public float DoorOpeningSound()
	{
		AudioClip clip = generalDoorOpeningAudio;
		return PlaySound(clip);
	}

	public float PlayGeneralClick()
	{
		AudioClip clip = generalClick;
		return PlaySound(clip);
	}

	private float ExecuteSound(AudioClip pSoundClip, bool pIs3DSound, Vector3 p3DSoundPosition, float volume)
	{
		//if(bool.Parse(PlayerPrefs.GetString("sound")))
		{
			if (Chronos.Instance.Time - lastTimeAudioPlayed > 0.01f)
			{
				if (pIs3DSound)
				{
					PlayClipAt(pSoundClip, p3DSoundPosition, volume);
				}
				else
				{
					GameObject soundGameObject = PlayClipAt(pSoundClip, Vector3.zero, volume);
					soundGameObject.transform.parent = cameraTransform;
					soundGameObject.transform.localPosition = Vector3.zero;

				}
			}
		}

		return pSoundClip.length;
	}

	//Isntantiate a game obect with an AudioSource component playing the audio file. The game object is destroyed when audip has finshied playing.
	private GameObject PlayClipAt(AudioClip clip, Vector3 pos, float volume)
	{
		GameObject tempGO = new GameObject("TempAudio");
		tempGO.transform.position = pos;
		AudioSource aSource = tempGO.AddComponent<AudioSource>();
		aSource.clip = clip;
		aSource.volume = volume;

		lastAudioPlayed = clip.name;
		lastTimeAudioPlayed = Chronos.Instance.Time;

		aSource.minDistance = 10;

		aSource.Play();
		Destroy(tempGO, clip.length); // Destroy object once clip has finished playing
		lastOneShotAudioCreated = tempGO;
		return tempGO;
	}

	private AudioClip Load(string content)
	{
		AudioClip obj = null;

		if (obj == null)
		{
			obj = Resources.Load(content, typeof(AudioClip)) as AudioClip;
		}

		if (obj == null && content != content.ToLower()) //Retry with lower case
		{
			obj = Resources.Load(content.ToLower(), typeof(AudioClip)) as AudioClip;
		}

		if (obj == null)
		{
			Debug.LogWarning("Loading AudioClip failed : " + content);
		}

		return obj;
	}

	public GameObject LastOneShotAudioGameObject
	{
		get
		{
			return lastOneShotAudioCreated;
		}
	}
}