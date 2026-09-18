using System.Collections.Generic;
using UnityEngine;

public class PlayerInsectPickUp : MonoBehaviour
{
    public void AddItem(InsectData insect, int count = 1)
    {
        if (insect == null || count <= 0) return;
        List<InsectSlot> insects = global::Player.Instance.playerData.playerInventoryData.insects;
        InsectSlot existingSlot = insects.Find(slot => slot.insect == insect);

        if (existingSlot != null)
        {
            existingSlot.count += count;
        }
        else
        {
            insects.Add(new InsectSlot { insect = insect, count = count });
        }
    }
}
