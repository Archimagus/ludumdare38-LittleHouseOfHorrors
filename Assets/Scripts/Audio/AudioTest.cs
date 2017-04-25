using System.Collections;
using UnityEngine;

class AudioTest : MonoBehaviour
{
	[SerializeField]
	private AudioClip _oneShotEffect;
	[SerializeField]
	private AudioClip _loopingEffect;
	[SerializeField]
	private AudioClip _interface;
	[SerializeField]
	private AudioClip _music;
	[SerializeField]
	private AudioClip _stinger;

	[SerializeField]
	private GameObject _oneShotEffectObject;
	[SerializeField]
	private GameObject _loopingEffectObject;
	[SerializeField]
	private GameObject _interfaceObject;
	[SerializeField]
	private GameObject _stingerObject;

	private AudioSource _loopingEffectSource;
	private AudioSource _musicSource;

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.E))
		{
			AudioManager.PlaySound(_oneShotEffect, AudioType.Effect, false, 0f, _oneShotEffectObject);
		}
		if (Input.GetKeyUp(KeyCode.L))
		{
			_loopingEffectSource = AudioManager
				.PlaySound(_loopingEffect, AudioType.Effect, true, 0f, _loopingEffectObject);
		}
		if (Input.GetKeyUp(KeyCode.I))
		{
			AudioManager.PlaySound(_interface, AudioType.Interface, false, 0f, _interfaceObject);
		}
		if (Input.GetKeyUp(KeyCode.M))
		{
			_musicSource = AudioManager.PlaySound(_music, AudioType.Music, true);
		}
		if (Input.GetKeyUp(KeyCode.S))
		{
			AudioManager.PlaySound(_stinger, AudioType.Stinger);
		}
		if (Input.GetKeyUp(KeyCode.P))
		{
			AudioManager.StopSound(_musicSource);
			AudioManager.StopSound(_loopingEffectSource);
		}
	}
}