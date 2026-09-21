using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInsectPickUp : MonoBehaviour
{
    
    public InsectData LastPickedInsect { get; private set; }
    public int LastPickedInsectCount { get; private set; }
    
    public static event Action<InsectData, int> OnInsectPicked;
    
    
    private void Start()
    {
        GameManager.instance.player.playerData.playerInventoryData.insects.Clear();
    }

    public void AddItem(InsectData insect, int count = 1)
    {
        PlayerInventoryData inventoryData = GameManager.instance.player.playerData.playerInventoryData;
        
        if (insect == null || count <= 0) return;
        
        LastPickedInsect = insect;
        LastPickedInsectCount = count;
        
        List<InsectSlot> insects = inventoryData.insects;
        InsectSlot existingSlot = insects.Find(slot => slot.insect == insect);

        UIJourney journey = GameManager.instance.M_UI.journey; 
        
        if (existingSlot != null)
        {
            existingSlot.count += count;
        }
        else
        {
            insects.Add(new InsectSlot { insect = insect, count = count });
        }

        if (!inventoryData.insectsOnInventory.Contains(insect))
        {
            inventoryData.insectsOnInventory.Add(insect);
        }
        if (journey != null)
            journey.RefreshUI();
        
        OnInsectPicked?.Invoke(insect, count);
    }
}
