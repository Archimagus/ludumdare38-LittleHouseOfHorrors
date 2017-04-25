using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
	private float _nextSoundTimeOffset;
	private float _lastSoundTime;

	[SerializeField]
	[Range(3f, 15f)]
	private float _minDelayTime;
	[SerializeField]
	[Range(10f, 30f)]
	private float _maxDelayTime;

	[SerializeField]
	private AudioClip[] _clips;

	public bool Active;

	void OnValidate()
	{
		_maxDelayTime = _maxDelayTime > _minDelayTime ? _maxDelayTime : _minDelayTime + 1;
	}

	private void Update()
	{
		if (Active && (Time.time - _nextSoundTimeOffset) > _lastSoundTime)
		{
			AudioManager.PlaySound(_clips[Random.Range(0, _clips.Length)], AudioType.Effect, parent: gameObject, spatialEffect: true);
			_lastSoundTime = Time.time;
			_nextSoundTimeOffset = Random.Range(_minDelayTime, _maxDelayTime);
		}
	}
}
