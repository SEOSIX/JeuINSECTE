using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [HideInInspector]public UIJourney journey;
    [HideInInspector]public UIChalet lobby; 
    public TerrariumManager terrariumManager;

    private bool IsUIActive;
    private TerrariumManager currentTerrariumManager;
    
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

    private void Start()
    {
        if (terrariumManager != null)
        {
            currentTerrariumManager = terrariumManager;
        }
        if (terrariumManager == null)
        {
            terrariumManager = currentTerrariumManager;
        }
    }
}
