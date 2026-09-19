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
        else { Destroy(gameObject);}
        
        
        Init();
    }

    private void Update()
    {
        ViewManaging();
    }

    private void Init()
    {
        uiManager = GetComponent<UIManager>();
        miniGameManager = GetComponent<MiniGameManager>();
    }

    private void ViewManaging()
    {
        if (SceneManager.GetActiveScene().name == _lobbySceneName)
        {
            M_UI.lobby._parentLobbyUI.SetActive(true);
            cam.gameObject.SetActive(false);
            player.gameObject.SetActive(false);
            M_UI.journey._parentJourney.SetActive(false);
        }
        else if (SceneManager.GetActiveScene().name == _testScene)
        {
            M_UI.lobby._parentLobbyUI.SetActive(false);
            cam.gameObject.SetActive(true);
            player.gameObject.SetActive(true);
            M_UI.journey._parentJourney.SetActive(true);
        }
    }
}
