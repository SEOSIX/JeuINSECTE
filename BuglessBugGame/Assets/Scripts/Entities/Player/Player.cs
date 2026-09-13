using UnityEngine;

public class Player : MonoBehaviour
{
    
    public static Player Instance {get; private set;}
    
    private PlayerController p_controller;
    
    void Awake()
    {
        Instance = this;
    }

    void Init()
    {
        p_controller = GetComponent<PlayerController>();
    }
}
