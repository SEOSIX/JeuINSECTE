using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    
    [Header("Privates")]
    private UIManager uiManager;
    private MiniGameManager miniGameManager;
    
    [Header("Public")]
    public Player player;
    public Camera cam;
    public Transform playerSpawn;
    
    [Header("ScenesName")]
    [SerializeField] public string _lobbySceneName;
    //TESTSCENE
    [SerializeField] public string _testScene;

    public UIManager M_UI => uiManager;
    public MiniGameManager M_MiniGameManager => miniGameManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); return; }
        
        Init();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Init()
    {
        uiManager = GetComponent<UIManager>();
        miniGameManager = GetComponent<MiniGameManager>();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ViewManaging(scene.name);
    }

    private void ViewManaging(string sceneName)
    {
        if (sceneName == _lobbySceneName)
        {
            player.gameObject.transform.position = playerSpawn.position;
            M_UI.lobby._parentLobbyUI.SetActive(true);
            cam.gameObject.SetActive(false);
            M_UI.isUiActive = true;
            M_UI.journey._parentJourney.SetActive(false);
        }
        else if (sceneName == _testScene)
        {
            player.gameObject.transform.position = playerSpawn.position;
            M_UI.lobby._parentLobbyUI.SetActive(false);
            cam.gameObject.SetActive(true);
            M_UI.journey._parentJourney.SetActive(true);
            M_UI.isUiActive = false;
        }
    }
}