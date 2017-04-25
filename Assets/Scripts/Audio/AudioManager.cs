using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public static class AudioManager
{
	private static AudioMixerGroup _effectGroup;
	private static AudioMixerGroup _interfaceGroup;
	private static AudioMixerGroup _musicGroup;
	private static AudioMixerGroup _stingerGroup;

	private static GameObject _audioPlayer;
	private static AudioMixer _audioMixer;

	private static List<AudioSource> _sources;

	private static float _maxPitchOffset = 0.1f;
	private static float _minSteroPan = -1f;
	private static float _maxStereoPan = 1f;
	private static float _spatialBlend = 0f;

	public static AudioClipHelper Clips;

	static AudioManager()
	{
		if (!Application.isPlaying)
			return;

		_sources = new List<AudioSource>();


		_audioMixer = Resources.Load("MasterMixer") as AudioMixer;

		_effectGroup = _audioMixer.FindMatchingGroups("Effect")[0];
		_interfaceGroup = _audioMixer.FindMatchingGroups("Interface")[0];
		_musicGroup = _audioMixer.FindMatchingGroups("Music")[0];
		_stingerGroup = _audioMixer.FindMatchingGroups("Stinger")[0];

		_audioMixer.SetFloat("masterVolume", PlayerPrefs.GetFloat("masterVolume", 1));
		_audioMixer.SetFloat("musicVolume", PlayerPrefs.GetFloat("musicVolume", 0));
		_audioMixer.SetFloat("effectVolume", PlayerPrefs.GetFloat("effectsVolume", 0));
		_audioMixer.SetFloat("interfaceVolume", PlayerPrefs.GetFloat("interfaceVolume", 0));



		_audioPlayer = GameObject.Find("AudioPlayer");
		if (_audioPlayer == null)
		{
			_audioPlayer = Object.Instantiate(Resources.Load("AudioPlayer") as GameObject);
			_audioPlayer.name = "AudioPlayer";
			Clips = _audioPlayer.GetComponent<AudioClipHelper>();
			_audioPlayer.GetComponent<MusicManager>().OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
		}
		else
		{
			Clips = _audioPlayer.GetComponent<AudioClipHelper>();
		}


	}

	public static AudioSource PlaySound(AudioClip clip, AudioType audioType,
		bool loop = false, float delay = 0, GameObject parent = null, bool spatialEffect = false)
	{
		CleanupSources();
		parent = parent ?? _audioPlayer.gameObject;

		AudioSource source = parent.AddComponent<AudioSource>();
		source.clip = clip;
		source.loop = loop;

		switch (audioType)
		{
			case AudioType.Effect:
				{
					source.outputAudioMixerGroup = _effectGroup;
					if (spatialEffect)
					{
						source.spatialBlend = 1;
						source.minDistance = 0.01f;
						source.maxDistance = 0.05f;
					}
					else
					{
						source.spatialBlend = 0;
						source.minDistance = 1;
						source.maxDistance = 5;
					}
				}
				break;
			case AudioType.Interface:
				{
					source.outputAudioMixerGroup = _interfaceGroup;
				}
				break;
			case AudioType.Music:
				{
					source.outputAudioMixerGroup = _musicGroup;
				}
				break;
			case AudioType.Stinger:
				{
					SetStingerProperties(ref source);
				}
				break;
		}

		if (delay > 0)
		{
			source.PlayDelayed(delay);
		}
		else
		{
			source.Play();
		}

		_sources.Add(source);

		return source;
	}

	public static void StopSound(AudioSource stopSource)
	{
		AudioSource source = _sources.Find(x => x == stopSource);

		if (source == null)
		{ return; }

		if (source.isPlaying)
		{
			source.Stop();
			Object.Destroy(source);
			_sources.Remove(source);
		}
	}

	private static void CleanupSources()
	{
		List<AudioSource> removeSources = new List<AudioSource>();
		foreach (var source in _sources)
		{
			if (source == null)
			{ return; }

			if (!source.isPlaying)
			{
				removeSources.Add(source);
			}
		}
		foreach (var source in removeSources)
		{
			_sources.Remove(source);
			Object.Destroy(source);
		}
	}

	private static void SetStingerProperties(ref AudioSource source)
	{
		source.outputAudioMixerGroup = _stingerGroup;
		source.pitch = Random.Range(1f - _maxPitchOffset, 1f + _maxPitchOffset);
		source.panStereo = Random.Range(_minSteroPan, _maxStereoPan);
		source.spatialBlend = _spatialBlend;
	}

	public static float MasterVolume
	{
		get
		{
			float value;
			_audioMixer.GetFloat("masterVolume", out value);
			return value;
		}

		set
		{
			_audioMixer.SetFloat("masterVolume", value);
		}
	}

	public static float MusicVolume
	{
		get
		{
			float value;
			_audioMixer.GetFloat("musicVolume", out value);
			return value;
		}

		set
		{
			_audioMixer.SetFloat("musicVolume", value);
		}
	}

	public static float EffectVolume
	{
		get
		{
			float value;
			_audioMixer.GetFloat("effectVolume", out value);
			return value;
		}

		set
		{
			_audioMixer.SetFloat("effectVolume", value);
		}
	}

	public static float InterfaceVolume
	{
		get
		{
			float value;
			_audioMixer.GetFloat("interfaceVolume", out value);
			return value;
		}

		set
		{
			_audioMixer.SetFloat("interfaceVolume", value);
		}
	}

	public static float StingerVolume
	{
		get
		{
			float value;
			_audioMixer.GetFloat("stingerVolume", out value);
			return value;
		}

		set
		{
			_audioMixer.SetFloat("stingerVolume", value);
		}
	}
}

public enum AudioType
{
	Effect,
	Interface,
	Music,
	Stinger,
}
