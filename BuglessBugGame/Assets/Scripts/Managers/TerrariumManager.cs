using UnityEngine;
using UnityEngine.InputSystem;

public class TerrariumManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject terrariumUI;
    [SerializeField] private GameObject leftUI;
    
    [SerializeField]private TerrariumData terrariumData;

    private Vector2 swipeInput;
    private PlayerInput playerInput;

    private void Awake()
    {
        Init();
    }
    
    private void Init()
    {
        SetTerrariumLevel();
    }
    
    private void Update()
    {
        ActivateTerrariumUI();
    }

    public void Swipe(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            swipeInput = context.ReadValue<Vector2>();
        }
    }

    private void SetTerrariumLevel()
    {
        int maxInsectsByLevel = 0;
        
        if(terrariumData == null) return;

        if (terrariumData.terrariumLevel == 1)
        {
            maxInsectsByLevel = 1;
            terrariumData.insects.Capacity = maxInsectsByLevel;
        }
        if (terrariumData.terrariumLevel == 2)
        {
            maxInsectsByLevel = 1;
            terrariumData.insects.Capacity = maxInsectsByLevel;
        }
        if (terrariumData.terrariumLevel == 3)
        {
            maxInsectsByLevel = 3;
            terrariumData.insects.Capacity = maxInsectsByLevel;
        }
        if (terrariumData.terrariumLevel == 4)
        {
            maxInsectsByLevel = 4;
            terrariumData.insects.Capacity = maxInsectsByLevel;
        }
    }

    #region TouchInput

    public void ActivateTerrariumUI()
    {
        if (swipeInput != Vector2.zero && swipeInput.x + swipeInput.y > 0f)
        {
            leftUI.SetActive(true);
            terrariumUI.SetActive(false);
        }
        else if (swipeInput != Vector2.zero && swipeInput.x + swipeInput.y < 0f)
        {
            leftUI.SetActive(false);
            terrariumUI.SetActive(true);
        }
    }
    public void Drag()
    {
        
    }

    public void Drop()
    {
        
    }

    #endregion
}