using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player Data")]
public class PlayerData : ScriptableObject
{
    public PlayerControllerData playerControllerData;
}

[Serializable]
public class PlayerControllerData
{
    [Header("Movement")]
    public float walkSpeed = 7.0f;
    public float runSpeed = 10f;
    public float CrouchSpeed = 4.0f;
    public float rotationSpeed = 1.0f;
}