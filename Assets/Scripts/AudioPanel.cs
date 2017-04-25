using UnityEngine;
using UnityEngine.UI;

public class AudioPanel : MonoBehaviour
{
	[SerializeField]
	private Slider _masterSlider;
	[SerializeField]
	private Slider _effectSlider;
	[SerializeField]
	private Slider _interfaceSlider;
	[SerializeField]
	private Slider _musicSlider;

	private void Awake()
	{
		_masterSlider.value = DecibelToLinear(AudioManager.MasterVolume);
		_effectSlider.value = DecibelToLinear(AudioManager.EffectVolume);
		_interfaceSlider.value = DecibelToLinear(AudioManager.InterfaceVolume);
		_musicSlider.value = DecibelToLinear(AudioManager.MusicVolume);
	}

	public void SetMasterVolume(float value)
	{
		AudioManager.MasterVolume = LinearToDecibel(value);
	}

	public void SetEffectVolume(float value)
	{
		AudioManager.EffectVolume = LinearToDecibel(value);
	}

	public void SetInterfaceVolume(float value)
	{
		AudioManager.InterfaceVolume = LinearToDecibel(value);
	}

	public void SetMusicVolume(float value)
	{
		AudioManager.MusicVolume = LinearToDecibel(value);
	}

	private static float LinearToDecibel(float lin)
	{
		if (lin <= float.Epsilon)
			return -80;
		return Mathf.Log(lin, 3) * 20;
	}

	private static float DecibelToLinear(float db)
	{
		return Mathf.Pow(3, db / 20);
	}
}
