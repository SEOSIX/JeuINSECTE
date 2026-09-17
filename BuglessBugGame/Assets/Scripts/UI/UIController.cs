using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] public Button _catchButton;
    [SerializeField] private Button _leaveButton;

    
    [Header("routines")]
    private Coroutine catchCoroutine;
    private void Start()
    {
        _leaveButton.onClick.AddListener(SummaryJourney);
        _catchButton.onClick.AddListener(CatchInsect);
    }

    private void CatchInsect()
    {
        Player player = GameManager.instance.player;
        
        player.isInteracted = true;
    }

    private void SummaryJourney()
    {
        GameManager.instance.M_UI.journey._parentJourneySum.SetActive(true);
    }
    
}
