using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [HideInInspector]public UIJourney journey;
    [HideInInspector]public UIChalet lobby;
    [HideInInspector] public TerrariumManager terrariumManager;

    private bool IsUIActive;
    
    public bool isUiActive
    {
        get => IsUIActive;
        set => IsUIActive = value;
    }

    void Awake()
    {
        journey = GetComponent<UIJourney>();
        lobby = GetComponent<UIChalet>();
        terrariumManager = GetComponent<TerrariumManager>();
    }
}
