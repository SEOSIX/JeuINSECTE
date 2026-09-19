using System.Collections.Generic;
using GamePlayCore;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField] private GameObject instanceMinigameParent;

    private GameObject currentMiniGameInstance;
    private MiniGameUI currentMiniGameUI;
    private Insect currentInsect;

    public bool IsMiniGameActive => currentMiniGameInstance != null;

    public void DetectForInteraction(Insect insect)
    {
        if (IsMiniGameActive) return;
        
        GameManager.instance.M_UI.isUiActive = true;
        GameManager.instance.M_UI.journey._outMiniGame_UI.SetActive(false);
        
        List<InsectSlot> insectList = Player.Instance.playerData.playerInventoryData.insects;
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
    }

    public void OnMiniGameSuccess()
    {
        if (currentInsect == null) return;

        Player.Instance.insectPickUp.AddItem(currentInsect.bug, currentInsect.count);
        Destroy(currentInsect.gameObject);

        Player player = GameManager.instance.player;
        player.isInteracted = false;

        Debug.Log($"{currentInsect.bug.name} has been picked up");

        CloseMiniGame();
    }

    public void OnMiniGameFailed()
    {
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
        GameManager.instance.M_UI.journey._outMiniGame_UI.SetActive(true);
    }
}