using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] public Button _catchButton;
    [SerializeField] private Button _leaveButton;

    
    [Header("routines")]
    private Coroutine catchCoroutine;
    private void Start()
    {
        
    }

    void Update()
    {
        CatchInsect();
    }

    private void CatchInsect()
    {
        Player player = GameManager.instance.player;
        
        if (_catchButton.onClick != null)
        {
            player.isInteracted = true;
        }
    }
}
