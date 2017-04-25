using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneStartFade : MonoBehaviour
{
	private Image _initialBlackScreen;

	private void Awake()
	{
		GameManager.TheFader = this;
		_initialBlackScreen = GetComponent<Image>();
	}

	void Start ()
	{
		FadeIn();
	}
	public void FadeIn()
	{
		gameObject.SetActive(true);
		StartCoroutine(fadeIn());
	}
	public void FadeOut()
	{
		gameObject.SetActive(true);
		StartCoroutine(fadeOut());
	}
	IEnumerator fadeIn()
	{
		yield return new WaitForSeconds(1.5f);

		_initialBlackScreen.CrossFadeAlpha(1, 0, false);
		_initialBlackScreen.CrossFadeAlpha(0, 1, false);

		yield return new WaitForSeconds(1);

		gameObject.SetActive(false);
	}
	IEnumerator fadeOut()
	{
		_initialBlackScreen.CrossFadeAlpha(0, 0, false);
		_initialBlackScreen.CrossFadeAlpha(1, 2, false);
		yield return new WaitForSeconds(2);

	}
}
