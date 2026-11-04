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

    private InsectData _currentInsectData;
    private void Update()
    {
        if (loseCoroutine != null) return;
    }

    public void DetectForInteraction(Insect insect)
    {
        if (IsMiniGameActive) return;

        currentInsect = insect;
        _currentInsectData = currentInsect.PickRandomInsect();
        
        GameManager.instance.M_UI.isUiActive = true;
        GameManager.instance.M_UI.journey._outMiniGame_UI.SetActive(false);
        
        List<InsectSlot> insectList = GameManager.instance.player.playerData.playerInventoryData.insects;
        InsectSlot existingSlot = insectList.Find(slot => slot.insect == insect._insectPicked);
        if (existingSlot != null && existingSlot.count == insect._insectPicked.catchCount) return;

        if (insect._insectPicked.catchMiniGamePrefabUI == null)
        {
            Debug.LogWarning($"No prefab set for {insect._insectPicked.name}");
            return;
        }

        currentMiniGameInstance = Instantiate(insect._insectPicked.catchMiniGamePrefabUI, instanceMinigameParent.transform);
        currentMiniGameUI = currentMiniGameInstance.GetComponent<MiniGameUI>();
        
        //ici c'est pour init le MiniGame type d'input a recieve
        currentMiniGameUI.Init(this, insect._insectPicked);
        currentMiniGameUI.loseInsectGo.SetActive(false);
        MiniGameUI.currentTryCount = currentInsect._insectPicked.tryCount;
    }

    public void OnMiniGameSuccess()
    {
        if (currentInsect == null) return;

        GameManager.instance.player.insectPickUp.AddItem(_currentInsectData);
        Destroy(currentInsect.gameObject);

        Player player = GameManager.instance.player;
        player.isInteracted = false;

        CloseMiniGame();
    }

    public void OnMiniGameFailed()
    {
        MiniGameUI.currentTryCount = currentInsect._insectPicked.tryCount;

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

        var text = currentMiniGameUI.loseInsectGo.GetComponent<TextMeshProUGUI>();
        StartCoroutine(RevealTextCoroutine(text, text.textInfo.characterCount));

        if (ai != null)
        {
            while (!ai.IsOutsideScreen())
            {
                ai.RunAway();
                yield return null;
            }
        }

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