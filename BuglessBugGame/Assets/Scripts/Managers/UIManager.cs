using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [HideInInspector]public UIJourney journey;
    [HideInInspector]public UIChalet lobby;

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
    }
}
