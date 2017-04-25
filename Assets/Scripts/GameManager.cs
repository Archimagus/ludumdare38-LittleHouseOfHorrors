using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public GameObject PortalPrefab;
	private void Awake()
	{
		Instance = this;
	}
	public static FollowCamera TheCamera { get; set; }
	public static Player ThePlayer { get; set; }
	public static PauseManager PauseManager { get; set; }
	public static HUDManager HudManager { get; set; }
	public static EventWindow TheEventDriver { get; set; }

	public static Map TheMap { get; set; }

	public static Clock TheClock { get; set; }

	public static SceneStartFade TheFader { get; set; }

	public static void GameOver()
	{
		DestroyImmediate(GameObject.Find("SelectedPlayerCard"));
		Instance.LoadLevel("GameOver", 2);
	}
	public void LoadLevel(string levelName, float delay)
	{
		StartCoroutine(doLoadLeve(levelName, delay));
	}
	public void LoadLevel(string levelName)
	{
		StartCoroutine(doLoadLeve(levelName));
	}
	private IEnumerator doLoadLeve(string levelName, float delay = 0)
	{
		if(TheFader != null)
			TheFader.FadeOut();
		yield return new WaitForSeconds(delay);
		SceneManager.LoadScene(levelName);

	}
}
