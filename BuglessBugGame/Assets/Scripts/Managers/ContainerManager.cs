using System.Collections.Generic;
using UnityEngine;

public class ContainerManager : MonoBehaviour
{
    [SerializeField] private GameObject containerPrefab;
    
    [SerializeField] private List<RectTransform> slots = new List<RectTransform>();

    private void Awake()
    {
        if (slots.Count == 0)
        {
            foreach (Transform child in transform)
                slots.Add(child as RectTransform);
        }
    }

    private void OnEnable()
    {
        PlayerInsectPickUp.OnInsectPicked += HandleInsectPicked;
    }

    private void OnDisable()
    {
        PlayerInsectPickUp.OnInsectPicked -= HandleInsectPicked;
    }

    private void HandleInsectPicked(InsectData insect, int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (!TrySpawn(insect))
            {
                Debug.LogWarning("no slot free");
                return;
            }
        }
    }

    private bool TrySpawn(InsectData insect)
    {
        RectTransform slot = GetFreeSlot();
        if (slot == null)
            return false;

        GameObject container = Instantiate(containerPrefab, slot, false);
        Container insectContainerScript = container.GetComponent<Container>();
        insectContainerScript.Setup(insect);
        return true;
    }

    private RectTransform GetFreeSlot()
    {
        foreach (RectTransform slot in slots)
        {
            if (slot.childCount == 0 && slot != Container.DraggingFromSlot)
                return slot;
        }
        return null;
    }
}