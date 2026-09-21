using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance {get; private set;}

    [SerializeField] private PlayerData data;

    private PlayerController p_controller;
    private PlayerInsectPickUp p_insectPickUp;
    private Rigidbody ridigBody;
    public PlayerData playerData => data;
    public PlayerInsectPickUp insectPickUp => p_insectPickUp;
    public Rigidbody _rb => ridigBody;
    
    [HideInInspector] public bool isInteracted;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject);}
        Init();
    }

    void Init()
    {
        p_controller = GetComponent<PlayerController>();
        p_insectPickUp = GetComponent<PlayerInsectPickUp>();
        ridigBody = GetComponent<Rigidbody>();
    }
}
