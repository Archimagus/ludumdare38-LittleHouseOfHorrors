using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventUIManager : MonoBehaviour
{
    public GameObject OptionalEventPopup;

	void Update ()
    {
        // FOR TESTING PURPOSES - SHOULD BE DELETED
		if(Input.GetKeyDown(KeyCode.E) && !OptionalEventPopup.activeInHierarchy)
        {
            ShowOptioonalEventPopup();
        }
	}

    // Call this method when we want an optional event to possibly
    // occur;
    public void ShowOptioonalEventPopup()
    {
        PreparePopupData(null);
    }

    // Prepare all event data on the popup and then display the popup
    private void PreparePopupData(Event e)
    {
        OptionalEventPopup.SetActive(true);
    }

    public void AcceptEvent()
    {
        // Do what we need to do in order to cause
        // event to take place
        OptionalEventPopup.SetActive(false);
    }

    public void DeclineEvent()
    {
        OptionalEventPopup.SetActive(false);
    }

    public void ButtonHover(BaseEventData eventData)
    {
        AudioManager.PlaySound(AudioManager.Clips.ButtonHover, AudioType.Interface);
    }
}
