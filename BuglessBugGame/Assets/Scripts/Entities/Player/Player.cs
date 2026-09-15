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
    void Awake()
    {
        Instance = this;
        Init();
    }

    void Init()
    {
        p_controller = GetComponent<PlayerController>();
        p_insectPickUp = GetComponent<PlayerInsectPickUp>();
        ridigBody = GetComponent<Rigidbody>();
    }
}
