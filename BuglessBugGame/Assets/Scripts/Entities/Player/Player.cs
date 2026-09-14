using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance {get; private set;}

    [SerializeField] private PlayerData data;

    private PlayerController p_controller;
    private Rigidbody ridigBody;

    public PlayerData playerData => data;
    public Rigidbody _rb => ridigBody;
    void Awake()
    {
        Instance = this;
        Init();
    }

    void Init()
    {
        p_controller = GetComponent<PlayerController>();
        ridigBody = GetComponent<Rigidbody>();
    }
}
