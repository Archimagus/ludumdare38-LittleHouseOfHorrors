using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject IntroScreen;
    public GameObject MainMenuPanel;
    public GameObject SelectPlayerPanel;
    public GameObject OptionsPanel;
    public GameObject AudioPanel;
    public GameObject VisualPanel;
    public GameObject InstructionsPanel;
    public GameObject CreditsPanel;

    public Image SplashImage;

    public List<QualitySetting> QualitySettingsList;
    public TextMeshProUGUI QualityText;

    public List<InstructionTab> InstructionTabsList;
    public TextMeshProUGUI InstructionTabText;

    public List<PlayingCard> PlayingCards;
    public GameObject CurrentCard;
    public TextMeshProUGUI[] CardStats;

	private GameObject _currentPanel;
	private GameObject _subOptionsPanel;

	private IEnumerator _introSplashScreen;

	private int _currentQualityNum;
	private int _currentInstructionTab;
    private int _currentPlayingCardNum;

	private IntroSceenManagement _introScreenManagement;
	private FrameRateManager _frameRateManager;
    private SelectedPlayerCard _selectedPlayerCard;

	void Start()
	{
		_currentInstructionTab = 0;
        _currentPlayingCardNum = 0;

        _selectedPlayerCard = FindObjectOfType<SelectedPlayerCard>();

        // Builds array of text objects of CurrentCard object
        // Array is built in order in which stats appear in SelectPlayerCard.cs
        //CardStats = CurrentCard.GetComponentsInChildren<TextMeshProUGUI>();

		StartInitialScreen();
	}

	void Update()
	{

		if (IntroScreen.activeInHierarchy && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
		{
			SkipSplashScreen();
		}

		if(_introScreenManagement == null)
		{
			_introScreenManagement = FindObjectOfType<IntroSceenManagement>();
		}
		else
		{
			if(_introScreenManagement.SkipIntro && IntroScreen.activeInHierarchy)
			{
				SkipSplashScreen();
			}
		}

	}

	private void StartInitialScreen()
	{
		// Setting timescale back to 1 is for when player quits the game
		// via pause. Pause sets timescale to 0 and engine needs it set back to
		// 1 for image fading to work
		Time.timeScale = 1;
		SplashImage.CrossFadeAlpha(0, 0, false);

		_introSplashScreen = SplashScreen();

		StartCoroutine(_introSplashScreen);
	}

	public void SkipSplashScreen()
	{
		StopCoroutine(_introSplashScreen);
		IntroScreen.SetActive(false);
		MainMenuPanel.SetActive(true);

		_introScreenManagement.SkipIntro = true;
	}

	public void StartGame()
	{
		AudioManager.PlaySound(AudioManager.Clips.StartGame, AudioType.Interface);
		SceneManager.LoadScene("Gameplay");
	}

    public void SelectPlayer()
    {
        MainMenuPanel.SetActive(false);
        _currentPanel = SelectPlayerPanel;
        _currentPanel.SetActive(true);

        foreach(PlayingCard card in PlayingCards)
        {
            if(card.Id == _currentPlayingCardNum)
            {
                _selectedPlayerCard.PlayerName = card.PlayerName;
                _selectedPlayerCard.Health = card.Health;
                _selectedPlayerCard.Sanity = card.Sanity;
                _selectedPlayerCard.Movement = card.Movement;
                _selectedPlayerCard.Strength = card.Strength;
                _selectedPlayerCard.Intelligence = card.Intelligence;
                _selectedPlayerCard.Essence = card.Essence;
                _selectedPlayerCard.CardSprite = card.CardSprite;
            }
        }

        ManageCurrentCard();
    }

	public void BackToMain()
	{
		_currentPanel.SetActive(false);
		MainMenuPanel.SetActive(true);
	}

	public void ShowOptions()
	{
		PlayerPrefs.SetFloat("masterVolume", AudioManager.MasterVolume);
		PlayerPrefs.SetFloat("musicVolume", AudioManager.MusicVolume);
		PlayerPrefs.SetFloat("effectVolume", AudioManager.EffectVolume);
		PlayerPrefs.SetFloat("interfaceVolume", AudioManager.InterfaceVolume);

		MainMenuPanel.SetActive(false);
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

	public void ShowInstructions()
	{
		MainMenuPanel.SetActive(false);
		_currentPanel = InstructionsPanel;
		_currentPanel.SetActive(true);

		for (int i = 0; i < InstructionTabsList.Count; i++)
		{
			if (i == _currentInstructionTab)
			{
				InstructionTabText.SetText(InstructionTabsList[i].TabLabel);
				InstructionTabsList[i].TabPanel.SetActive(true);
				break;
			}
		}
	}

	public void ShowCredits()
	{
		MainMenuPanel.SetActive(false);
		_currentPanel = CreditsPanel;
		_currentPanel.SetActive(true);

	}

	public void ExitGame()
	{
#if UNITY_EDITOR
		if (Application.isEditor)
			UnityEditor.EditorApplication.isPlaying = false;
#endif
			Application.Quit();
	}

	public void ButtonClick(BaseEventData eventData)
	{
		AudioClip clip = AudioManager.Clips
			.ButtonClick[Random.Range(0, AudioManager.Clips.ButtonClick.Length)];
		AudioManager.PlaySound(clip, AudioType.Interface);
	}

	public void ButtonHover(BaseEventData eventDate)
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
				break;
			}
		}
	}

	public void InstructionTabLeft()
	{
		InstructionTabsList[_currentInstructionTab].TabPanel.SetActive(false);

		if (_currentInstructionTab > 0)
		{
			_currentInstructionTab--;
		}
		else
		{
			_currentInstructionTab = InstructionTabsList.Count - 1;
		}

		foreach (InstructionTab tab in InstructionTabsList)
		{
			if (tab.TabNum == _currentInstructionTab)
			{
				InstructionTabText.SetText(tab.TabLabel);
				tab.TabPanel.SetActive(true);
				break;
			}
		}
	}

	public void InstructionTabRight()
	{
		InstructionTabsList[_currentInstructionTab].TabPanel.SetActive(false);

		if (_currentInstructionTab < InstructionTabsList.Count - 1)
		{
			_currentInstructionTab++;
		}
		else
		{
			_currentInstructionTab = 0;
		}

		foreach (InstructionTab tab in InstructionTabsList)
		{
			if (tab.TabNum == _currentInstructionTab)
			{
				InstructionTabText.SetText(tab.TabLabel);
				tab.TabPanel.SetActive(true);
				break;
			}
		}
	}

    public void SelectPlayerCardLeft()
    {
        if (_currentPlayingCardNum > 0)
        {
            _currentPlayingCardNum--;
        }
        else
        {
            _currentPlayingCardNum = PlayingCards.Count - 1;
        }

        foreach (PlayingCard card in PlayingCards)
        {
            if (card.Id == _currentPlayingCardNum)
            {
                _selectedPlayerCard.PlayerName = card.PlayerName;
                _selectedPlayerCard.Health = card.Health;
                _selectedPlayerCard.Sanity = card.Sanity;
                _selectedPlayerCard.Movement = card.Movement;
                _selectedPlayerCard.Strength = card.Strength;
                _selectedPlayerCard.Intelligence = card.Intelligence;
                _selectedPlayerCard.Essence = card.Essence;
                _selectedPlayerCard.CardSprite = card.CardSprite;
                ManageCurrentCard();
                break;
            }
        }
    }

    public void SelectPlayerCardRight()
    {
        if (_currentPlayingCardNum < PlayingCards.Count - 1)
        {
            _currentPlayingCardNum++;
        }
        else
        {
            _currentPlayingCardNum = 0;
        }

        foreach (PlayingCard card in PlayingCards)
        {
            if (card.Id == _currentPlayingCardNum)
            {
                _selectedPlayerCard.PlayerName = card.PlayerName;
                _selectedPlayerCard.Health = card.Health;
                _selectedPlayerCard.Sanity = card.Sanity;
                _selectedPlayerCard.Movement = card.Movement;
                _selectedPlayerCard.Strength = card.Strength;
                _selectedPlayerCard.Intelligence = card.Intelligence;
                _selectedPlayerCard.Essence = card.Essence;
                _selectedPlayerCard.CardSprite = card.CardSprite;
                ManageCurrentCard();
                break;
            }
        }
    }

    private void ManageCurrentCard()
    {
        CurrentCard.GetComponent<Image>().sprite = _selectedPlayerCard.CardSprite;
        CardStats[0].SetText(_selectedPlayerCard.PlayerName);
        CardStats[1].SetText(_selectedPlayerCard.Health.ToString());
        CardStats[2].SetText(_selectedPlayerCard.Sanity.ToString());
        CardStats[3].SetText(_selectedPlayerCard.Movement.ToString());
        CardStats[4].SetText(_selectedPlayerCard.Strength.ToString());
        CardStats[5].SetText(_selectedPlayerCard.Intelligence.ToString());
        CardStats[6].SetText(_selectedPlayerCard.Essence.ToString());
    }

	IEnumerator SplashScreen()
	{
		yield return new WaitForSeconds(3);

		SplashImage.CrossFadeAlpha(255, 3, false);

		yield return new WaitForSeconds(6);

		SplashImage.CrossFadeAlpha(0, 3, false);

		yield return new WaitForSeconds(4);

		IntroScreen.SetActive(false);
		MainMenuPanel.SetActive(true);
		_introScreenManagement.SkipIntro = true;
	}

	[System.Serializable]
	public class QualitySetting
	{
		public int QualityNum;
		public string QualityText;
	}

	[System.Serializable]
	public class InstructionTab
	{
		public int TabNum;
		public string TabLabel;
		public GameObject TabPanel;
	}

    [System.Serializable]
    public class PlayingCard
    {
        public string PlayerName;
        public int Id;
        public int Health;
        public int Sanity;
        public int Movement;
        public int Strength;
        public int Intelligence;
        public int Essence;
        public Sprite CardSprite;
    }
}
