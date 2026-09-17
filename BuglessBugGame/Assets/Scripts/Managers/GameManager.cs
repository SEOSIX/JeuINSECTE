using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    
    [Header("Privates")]
    private UIManager uiManager;
    
    
    [Header("Public")]
    public Player player;

    public UIManager M_UI => uiManager;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else { Destroy(gameObject);}
        
        Init();
    }
    
    private void Init()
    {
        uiManager = GetComponent<UIManager>();
    }
}
