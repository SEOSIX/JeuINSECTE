using System.Collections.Generic;
using GamePlayCore;
using UnityEngine;

[CreateAssetMenu(fileName = "Encyclopedia", menuName = "Data/Encyclopedia Data")]
public class EncyclopediaData : ScriptableObject
{
    public List<InsectData> encyclopediaData;
}
