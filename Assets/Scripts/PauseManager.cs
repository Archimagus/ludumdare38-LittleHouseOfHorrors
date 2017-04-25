using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseManager : MonoBehaviour
{
	private HUDManager _hudManager;
	public GameObject PausePanel;
	public GameObject OptionsPanel;
	public GameObject AudioPanel;
	public GameObject VisualPanel;

	private GameObject _currentPanel;
	private GameObject _subOptionsPanel;

	public List<QualitySetting> QualitySettingsList;
	public TextMeshProUGUI QualityText;

	public bool isPaused = false;

	private int _currentQualityNum;

    private SelectedPlayerCard _selectedPlayerCard;

	void Start()
	{
		GameManager.PauseManager = this;

        _selectedPlayerCard = FindObjectOfType<SelectedPlayerCard>();
	}

	void Update()
	{

		if (_hudManager == null)
		{
			_hudManager = GameManager.HudManager;
		}

		if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
		{
			isPaused = true;
			PausePanel.SetActive(true);
			_hudManager.HideHUD();
		}
		else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
		{
			if (PausePanel.activeInHierarchy)
			{
				isPaused = false;
				PausePanel.SetActive(false);
				_hudManager.ShowHUD();
			}
			else if (OptionsPanel.activeInHierarchy)
			{
				BackToPause();
			}
			else
			{
				ShowOptions();
			}
		}

		if (isPaused)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
	}

	public void Resume()
	{
		isPaused = false;
		PausePanel.SetActive(false);
		_hudManager.ShowHUD();
	}

	public void BackToPause()
	{
		_currentPanel.SetActive(false);
		PausePanel.SetActive(true);
	}

	public void ShowOptions()
	{
		PlayerPrefs.SetFloat("masterVolume", AudioManager.MasterVolume);
		PlayerPrefs.SetFloat("musicVolume", AudioManager.MusicVolume);
		PlayerPrefs.SetFloat("effectVolume", AudioManager.EffectVolume);
		PlayerPrefs.SetFloat("interfaceVolume", AudioManager.InterfaceVolume);

		PausePanel.SetActive(false);
		_currentPanel = OptionsPanel;
		_currentPanel.SetActive(true);

		if (_subOptionsPanel != null)
		{
			_subOptionsPanel.SetActive(false);
		}
	}

	public void ShowAudio()
	{
		_currentPanel.SetActive(false);
		_subOptionsPanel = AudioPanel;
		_subOptionsPanel.SetActive(true);
	}

	public void ShowVisual()
	{
		_currentQualityNum = QualitySettings.GetQualityLevel();
		for (int i = 0; i < QualitySettingsList.Count; i++)
		{
			if (i == _currentQualityNum)
			{
				QualityText.SetText(QualitySettingsList[i].QualityText);
				break;
			}
		}

		_currentPanel.SetActive(false);
		_subOptionsPanel = VisualPanel;
		_subOptionsPanel.SetActive(true);
	}

	public void QuitGame()
	{
        if(_selectedPlayerCard != null)
        {
            // Destory this because we don't want main menu to have more than one of these
            // and there is no need to keep this past the gameplay scene
            _selectedPlayerCard.DestroyThisObject();
        }
		SceneManager.LoadScene("MainMenu");
	}

	public void ButtonClick(BaseEventData eventData)
	{
		AudioClip clip = AudioManager.Clips
			.ButtonClick[Random.Range(0, AudioManager.Clips.ButtonClick.Length)];
		AudioManager.PlaySound(clip, AudioType.Interface);
	}

	public void ButtonHover(BaseEventData eventData)
	{
		AudioManager.PlaySound(AudioManager.Clips.ButtonHover, AudioType.Interface);
	}

	public void LowerQualitySetting()
	{
		if (_currentQualityNum > 0)
		{
			_currentQualityNum--;
		}
		else
		{
			_currentQualityNum = QualitySettingsList.Count - 1;
		}

		foreach (QualitySetting setting in QualitySettingsList)
		{
			if (setting.QualityNum == _currentQualityNum)
			{
				QualityText.SetText(setting.QualityText);
				QualitySettings.SetQualityLevel(_currentQualityNum);
				AudioManager.PlaySound(AudioManager.Clips.ButtonHover, AudioType.Interface);
				break;
			}
		}
	}

	public void RaiseQualitySetting()
	{
		if (_currentQualityNum < QualitySettingsList.Count - 1)
		{
			_currentQualityNum++;
		}
		else
		{
			_currentQualityNum = 0;
		}

		foreach (QualitySetting setting in QualitySettingsList)
		{
			if (setting.QualityNum == _currentQualityNum)
			{
				QualityText.SetText(setting.QualityText);
				QualitySettings.SetQualityLevel(_currentQualityNum);
				AudioManager.PlaySound(AudioManager.Clips.ButtonHover, AudioType.Interface);
				break;
			}
		}
	}

	[System.Serializable]
	public class QualitySetting
	{
		public int QualityNum;
		public string QualityText;
	}
}
