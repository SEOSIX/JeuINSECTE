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
        Player player = GameManager.instance.player;
        catchCoroutine = StartCoroutine(CatchInsectCoroutine(player));
    }

    private IEnumerator CatchInsectCoroutine(Player player)
    {
        player.animationManager.SetCatch();
        yield return new WaitForSeconds(1f);
        player.isInteracted = true;
    }
}
