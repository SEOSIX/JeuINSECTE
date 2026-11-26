using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [HideInInspector]public UIJourney journey;
    [HideInInspector]public UIChalet lobby;
    [HideInInspector]public TerrariumManager terrariumManager;
    
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
        terrariumManager = TerrariumManager.Instance;
    }
}
