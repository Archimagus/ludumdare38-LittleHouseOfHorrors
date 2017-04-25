using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
	[SerializeField]
	private Image _hourHand;
	[SerializeField]
	private float[] _portalTimes = new float[] { 3, 6, 9 };

	private bool _yellowWarning = false;
	private AudioSource _yellowWarningSource;
	private bool _redWarning = false;
	private AudioSource _redWarningSource;

	public float TickAmmount;

	public float Time;

	public int PortalsLeft;

	private float _nextPortalTIme;
	private int _portalTimeIndex;

	private void Awake()
	{
		GameManager.TheClock = this;
		_nextPortalTIme = _portalTimes[0];
		_portalTimeIndex = 1;
		PortalsLeft = _portalTimes.Length + 1;
	}

	public void Tick(float time = 0)
	{
		if (time == 0)
			time = TickAmmount;
		Time += time;
		_hourHand.transform.rotation = Quaternion.Euler(0, 0, -360f * Time / 12);
		CheckTime();
	}

	public void CheckTime()
	{
		if (Time >= 10 && !_yellowWarning)
		{
			AudioManager.PlaySound(AudioManager.Clips.ClockWarningYellow, AudioType.Interface);
			_yellowWarningSource = AudioManager
				.PlaySound(AudioManager.Clips.ClockWarningYellowLoop, AudioType.Interface, true);
			_yellowWarning = true;
		}

		if (Time >= 11 && !_redWarning)
		{
			AudioManager.PlaySound(AudioManager.Clips.ClockWarningRed, AudioType.Interface);
			_redWarningSource = AudioManager
				.PlaySound(AudioManager.Clips.ClockWarningRedLoop, AudioType.Interface, true);
			_redWarning = true;
		}

		if (Time >= 12)
		{
			AudioManager.StopSound(_yellowWarningSource);
			AudioManager.StopSound(_redWarningSource);
			AudioManager.PlaySound(AudioManager.Clips.GameOver, AudioType.Interface);
			if (GameManager.TheMap.Rooms.Values.Any(rm => rm.HasPortal))
				PlayerPrefs.SetInt("GameOver", -1);
			else
				PlayerPrefs.SetInt("GameOver", 1);

			GameManager.GameOver();
		}
		if (_portalTimeIndex > 0 && _portalTimeIndex <= _portalTimes.Length && Time >= _nextPortalTIme)
		{
			int tries = 0;
			Room rm;
			do
			{
				rm = GameManager.TheMap.GetRandomRoom();
			} while (rm.HasPortal && ++tries <100);
			rm.OpenPortal();
			if(_portalTimeIndex < _portalTimes.Length)
				_nextPortalTIme = _portalTimes[_portalTimeIndex];
			_portalTimeIndex++;
		}
	}
}
