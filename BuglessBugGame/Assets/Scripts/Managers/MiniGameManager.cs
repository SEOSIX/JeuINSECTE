using System.Collections;
using System.Collections.Generic;
using GamePlayCore;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField] private GameObject instanceMinigameParent;
    
    private GameObject currentMiniGameInstance;
    public MiniGameUI currentMiniGameUI {get; private set;}
    public Insect currentInsect {get; private set;}

    public bool IsMiniGameActive => currentMiniGameInstance != null;

    [HideInInspector]public Coroutine loseCoroutine;

    private void Update()
    {
        if (loseCoroutine != null) return;
    }

    public void DetectForInteraction(Insect insect)
    {
        if (IsMiniGameActive) return;
        
        GameManager.instance.M_UI.isUiActive = true;
        GameManager.instance.M_UI.journey._outMiniGame_UI.SetActive(false);
        
        List<InsectSlot> insectList = GameManager.instance.player.playerData.playerInventoryData.insects;
        InsectSlot existingSlot = insectList.Find(slot => slot.insect == insect.bug);
        if (existingSlot != null && existingSlot.count == insect.bug.catchCount) return;

        if (insect.bug.catchMiniGamePrefabUI == null)
        {
            Debug.LogWarning($"No prefab set for {insect.bug.name}");
            return;
        }

        currentInsect = insect;
        currentMiniGameInstance = Instantiate(insect.bug.catchMiniGamePrefabUI, instanceMinigameParent.transform);
        currentMiniGameUI = currentMiniGameInstance.GetComponent<MiniGameUI>();
        
        //ici c'est pour init le MiniGame type d'input a recieve
        currentMiniGameUI.Init(this, insect.bug);
        currentMiniGameUI.loseInsectGo.SetActive(false);
        MiniGameUI.currentTryCount = currentInsect.bug.tryCount;
    }

    public void OnMiniGameSuccess()
    {
        if (currentInsect == null) return;

        GameManager.instance.player.insectPickUp.AddItem(currentInsect.bug, currentInsect.count);
        Destroy(currentInsect.gameObject);

        Player player = GameManager.instance.player;
        player.isInteracted = false;

        Debug.Log($"{currentInsect.bug.name} has been picked up");

        CloseMiniGame();
    }

    public void OnMiniGameFailed()
    {
        MiniGameUI.currentTryCount = currentInsect.bug.tryCount;

        if (loseCoroutine != null) return;
        loseCoroutine = StartCoroutine(RunAwayCoroutine());
        
    }

    private void CloseMiniGame()
    {
        if (currentMiniGameInstance != null)
        {
            Destroy(currentMiniGameInstance);
        }
        currentMiniGameInstance = null;
        currentMiniGameUI = null;
        currentInsect = null;
        GameManager.instance.M_UI.isUiActive = false;
        GameManager.instance.player.isInteracted = false;
        GameManager.instance.M_UI.journey._outMiniGame_UI.SetActive(true);
    }
    private IEnumerator RunAwayCoroutine()
    {
        var ai = currentMiniGameUI.aiMovements;
        ai.StartRunAway();
        
        var text = currentMiniGameUI.loseInsectGo.GetComponent<TextMeshProUGUI>();
        int totalCharacters = text.textInfo.characterCount;
        
        StartCoroutine(RevealTextCoroutine(text, totalCharacters));
        
        while (!ai.RunAwayStep(Time.deltaTime))
            yield return null;

        yield return new WaitForSeconds(1.5f);
        
        CloseMiniGame();
        loseCoroutine = null;
    }

    IEnumerator RevealTextCoroutine(TextMeshProUGUI text, int totalCharacters)
    {
        currentMiniGameUI.loseInsectGo.SetActive(true);
        text.maxVisibleCharacters = 0;
        text.ForceMeshUpdate();
        totalCharacters = text.textInfo.characterCount;
        float duration = 1f;
        float t = 0f;

        while (text.maxVisibleCharacters < totalCharacters)
        {
            t += Time.deltaTime;
            text.maxVisibleCharacters = Mathf.Min(totalCharacters, Mathf.FloorToInt(t / duration * totalCharacters));
            yield return null;
        }
        text.maxVisibleCharacters = totalCharacters;
    }
}