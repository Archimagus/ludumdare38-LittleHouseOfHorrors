using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StingerManager : MonoBehaviour
{
	[SerializeField]
	private List<AudioClip> _stingers;
	[SerializeField]
	[Range(3f, 30f)]
	private int _minDelayTime;
	[SerializeField]
	[Range(20f, 60f)]
	private int _maxDelayTime;
	[SerializeField]
	private bool _global;

	private float _lastStingerTime;
	private float _nextStingerTimeOffset;

	public bool Active;
	
	void OnValidate()
	{
		_maxDelayTime = _maxDelayTime > _minDelayTime ? _maxDelayTime : _minDelayTime + 1;
	}

	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;

		_lastStingerTime = Time.time;
		_nextStingerTimeOffset = Random.Range(_minDelayTime, _maxDelayTime);
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
	{
		if (_global)
		{
			Active = (scene.name != "MainMenu" && scene.name != "GameOver");
		}
	}

	private void Update()
	{
		if (Active && (Time.time - _nextStingerTimeOffset) > _lastStingerTime)
		{
			AudioManager.PlaySound(_stingers[Random.Range(0, _stingers.Count)], AudioType.Stinger);
			_lastStingerTime = Time.time;
			_nextStingerTimeOffset = Random.Range(_minDelayTime, _maxDelayTime);
		}
	}
}
