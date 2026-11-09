using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] public Button _catchButton;

    
    [Header("routines")]
    private Coroutine catchCoroutine;
    private void Start()
    {
        _catchButton.onClick.AddListener(CatchInsect);
    }
    
    private void CatchInsect()
    {
        GameManager.instance.player.RaiseInteract();
        GameManager.instance.player.isInteracted = true;
    }
}
