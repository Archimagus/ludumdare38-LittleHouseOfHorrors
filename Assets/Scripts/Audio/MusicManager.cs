using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
	public AudioClip IntroMusic;
	public AudioClip[] LevelMusic;

	private AudioSource _introMusic;
	private AudioSource _currentLevelMusic;

	private bool _firstScene = true;
	private bool _stoppedIntro = true;

	private MainMenuManager _mainMenuManager;

	private static MusicManager _musicManager;

	private void Awake()
	{
		if(_musicManager != null)
		{
			DestroyImmediate(gameObject);
			return;
		}

		_musicManager = this;
		if (Application.isPlaying)
		{
			DontDestroyOnLoad(gameObject);
		}
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	public void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
	{
		if (_currentLevelMusic != null)
		{
			AudioManager.StopSound(_currentLevelMusic);
		}

		if (scene.buildIndex == 0)
		{
			if(_mainMenuManager == null)
			{
				_mainMenuManager = FindObjectOfType<MainMenuManager>();
			}
		}

		if (scene.buildIndex < 0 || scene.buildIndex >= LevelMusic.Length)
		{
			_firstScene = false;
			_stoppedIntro = true;
			Debug.LogWarning(
				"Could not find Level Music for this Scene's Build Index: " + scene.buildIndex);
			return;
		}

		if (_firstScene && scene.name == "MainMenu")
		{
			_stoppedIntro = false;
			_introMusic = AudioManager.PlaySound(IntroMusic, AudioType.Music);
			_currentLevelMusic = AudioManager
				.PlaySound(LevelMusic[scene.buildIndex], AudioType.Music, true, IntroMusic.length);
		}
		else
		{
			_firstScene = false;
			_stoppedIntro = true;
			_currentLevelMusic = AudioManager
				.PlaySound(LevelMusic[scene.buildIndex], AudioType.Music, true);
		}
	}

	private void Update()
	{
		if (_firstScene && !_stoppedIntro && !_mainMenuManager.IntroScreen.activeInHierarchy)
		{
			if (_introMusic.isPlaying)
			{
				AudioManager.StopSound(_introMusic);
				AudioManager.StopSound(_currentLevelMusic);
				_currentLevelMusic = AudioManager
					.PlaySound(LevelMusic[0], AudioType.Music, true);
			}
			_stoppedIntro = true;
			_firstScene = false;
		}
	}
}
