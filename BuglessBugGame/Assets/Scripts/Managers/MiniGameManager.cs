using System.Collections.Generic;
using GamePlayCore;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField] private GameObject instanceMinigameParent;
    
    private GameObject currentMiniGameInstance;
    private MiniGameUI currentMiniGameUI;
    public Insect currentInsect {get; private set;}

    public bool IsMiniGameActive => currentMiniGameInstance != null;

    public void DetectForInteraction(Insect insect)
    {
        if (IsMiniGameActive) return;
        
        GameManager.instance.M_UI.isUiActive = true;
        GameManager.instance.M_UI.journey._outMiniGame_UI.SetActive(false);
        
        List<InsectSlot> insectList = GameManager.instance.player.playerData.playerInventoryData.insects;
        InsectSlot existingSlot = insectList.Find(slot => slot.insect == insect.bug);
        if (existingSlot != null && existingSlot.count == insect.bug.maxStackable) return;

        if (insect.bug.catchMiniGamePrefabUI == null)
        {
            Debug.LogWarning($"No catchMiniGamePrefabUI set on {insect.bug.name}");
            return;
        }

        currentInsect = insect;
        currentMiniGameInstance = Instantiate(insect.bug.catchMiniGamePrefabUI, instanceMinigameParent.transform);
        currentMiniGameUI = currentMiniGameInstance.GetComponent<MiniGameUI>();
        
        //ici c'est pour init le MiniGame type d'input a recieve
        currentMiniGameUI.Init(this, insect.bug);
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
        
        CloseMiniGame();
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
}