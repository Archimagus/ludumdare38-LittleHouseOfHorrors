using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventWindow : MonoBehaviour
{
	[SerializeField]
	GameObject _panel;
	[SerializeField]
	Button _oKButton;
	[SerializeField]
	TextMeshProUGUI _titleText;
	[SerializeField]
	TextMeshProUGUI _descriptionText;

	private void Awake()
	{
		GameManager.TheEventDriver = this;
		_panel.SetActive(false);	
		
	}
	internal void ShowEvent(Event e, Player p, Room rm)
	{
		_panel.SetActive(true);
		_titleText.text = e.Name;
		var text = e.Check(p, rm);
		if (e.Name != "Closing a Portal")
			text += EventList.EssenceString(1);
		_descriptionText.SetText(text);
		_oKButton.onClick.RemoveAllListeners();
		Event e1 = e;
		_oKButton.onClick.AddListener(() => { _panel.SetActive(false); e1.Do(p, rm); });
	}
}
