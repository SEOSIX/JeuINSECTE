using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player Data")]
public class PlayerData : ScriptableObject
{
    public PlayerControllerData playerControllerData;
    public PlayerInventoryData playerInventoryData;
}

[Serializable]
public class PlayerControllerData
{
    [Header("Movement")]
    public float walkSpeed = 7.0f;
    public float runSpeed = 10f;
    public float CrouchSpeed = 4.0f;
    public float rotationSpeed = 1.0f;
    [Header("Pickup")]
    public float pickupDistance = 2f;
    public float maxAimAngle = 15f;
}

[Serializable]
public class PlayerInventoryData
{
    [Header("Inventory")]
    public List<InsectSlot> insects = new List<InsectSlot>();
    
    public int GetCount(InsectData item)
    {
        foreach (var slot in insects)
        {
            if (slot.insect == item) return slot.count;
        }
        return 0;
    }
}

[Serializable]
public class InsectSlot
{
    public InsectData insect;
    public int count;
}