using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/*
 * This class will only be able to target framerate if v-sync is turned off in quality
 * settings. 
 */
public class FrameRateManager : MonoBehaviour
{
    public bool ShowFPS = true;
    public float AutoCheckInterval = 10f;
    public int TargetFrameRate = 60;
    public int FontSize = 25;

    private float _fpsUpdateInterval = 0.5f;
    private float _fpsAccum = 0;
    private int _fpsFrames = 0;
    private float _fpsTimeLeft;
    private float _fpsValue = 0f;
    private string _fpsValueStr = string.Empty;
    private GUIStyle _fpsStyle = new GUIStyle();
    private GUIStyle _fpsShadowStyle = new GUIStyle();
    private Rect _fpsLocation = new Rect(5, 5, 100, 25);
    private Rect _fpsShadowLocation = new Rect(5 + 1, 5 + 1, 100, 25);

    private Scene _startingScene;
    public bool FromMainMenu = false;

    private FrameRateManager[] _frameRateManagers;

    private void Awake()
    {
        _startingScene = SceneManager.GetActiveScene();
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        HandleObjectAndRemoveDuplicate();

        Application.targetFrameRate = TargetFrameRate;

        _fpsStyle.fontSize = FontSize;
        _fpsShadowStyle.fontSize = FontSize;

        _fpsTimeLeft = _fpsUpdateInterval;
        _fpsLocation = new Rect(Screen.width - 250, 5, 100, 25);
        _fpsShadowLocation = new Rect(Screen.width - 250 + 1, 5 + 1, 100, 25);
    }

    void OnGUI()
    {
        if (!ShowFPS)
        {
            return;
        }

        if (ShowFPS)
        {
            GUI.Label(_fpsShadowLocation, _fpsValueStr, _fpsShadowStyle);
            GUI.Label(_fpsLocation, _fpsValueStr, _fpsStyle);
        }
    }

    void Update()
    {
        if(Application.targetFrameRate != TargetFrameRate)
        {
            Application.targetFrameRate = TargetFrameRate;
        }

        if(Input.GetKeyDown(KeyCode.LeftBracket))
        {
            ShowFPS = !ShowFPS;
        }

        _fpsTimeLeft -= Time.deltaTime;
        _fpsAccum += Time.timeScale / Time.deltaTime;
        ++_fpsFrames;

        // Interval ended - update GUI text and start new interval
        if (_fpsTimeLeft <= 0.0f)
        {
            _fpsValue = _fpsAccum / _fpsFrames;

            if (_fpsValue < 30)
                _fpsStyle.normal.textColor = Color.yellow;
            else if (_fpsValue < 10)
                _fpsStyle.normal.textColor = Color.red;
            else
                _fpsStyle.normal.textColor = Color.green;

            _fpsValueStr = System.String.Format("{0:f0}", _fpsValue) + "fps";

            _fpsTimeLeft = _fpsUpdateInterval;
            _fpsAccum = 0.0F;
            _fpsFrames = 0;
        }
    }

    // Does all the initial setup when scene loads and deletes newly created object
    // if correct one already exists
    private void HandleObjectAndRemoveDuplicate()
    {
        if(_frameRateManagers == null)
        {
            _frameRateManagers = GameObject.FindObjectsOfType<FrameRateManager>();
        }

        if (_frameRateManagers.Length > 1)
        {
            foreach (FrameRateManager rateManager in _frameRateManagers)
            {
                if (!rateManager.FromMainMenu)
                {
                    Destroy(rateManager.gameObject);
                }
            }
        }

        if (_startingScene.name.Equals("MainMenu"))
        {
            FromMainMenu = true;
        }
    }
}