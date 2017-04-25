using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceenManagement : MonoBehaviour
{

    private IntroSceenManagement[] _introScreenManagements;
    private Scene _startingScene;
    public bool FromMainMenu;

    public bool SkipIntro = false;

    private void Awake()
    {
        _startingScene = SceneManager.GetActiveScene();
        DontDestroyOnLoad(this.gameObject);
        HandleObjectAndRemoveDuplicate();
    }

    // Does all the initial setup when scene loads and deletes newly created object
    // if correct one already exists
    private void HandleObjectAndRemoveDuplicate()
    {
        if (_introScreenManagements == null)
        {
            _introScreenManagements = GameObject.FindObjectsOfType<IntroSceenManagement>();
        }

        if (_introScreenManagements.Length > 1)
        {
            foreach (IntroSceenManagement introManagement in _introScreenManagements)
            {
                if (!introManagement.FromMainMenu)
                {
                    Destroy(introManagement.gameObject);
                }
            }
        }

        if (_startingScene.name.Equals("MainMenu"))
        {
            FromMainMenu = true;
        }
    }
}
