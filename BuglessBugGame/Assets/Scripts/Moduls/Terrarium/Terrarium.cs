using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// À placer sur l'objet Terrarium (il doit avoir un Graphic, ex. Image, avec Raycast Target activé,
/// pour pouvoir recevoir le drop).
/// Reçoit l'image du Container et l'affiche dans la zone du terrarium.
/// </summary>
public class Terrarium : MonoBehaviour, IDropHandler
{
    [SerializeField] private RectTransform dropArea;

    private void Awake()
    {
        if (dropArea == null)
            dropArea = transform as RectTransform;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        Container container = eventData.pointerDrag.GetComponent<Container>();
        if (container == null)
            return;
        container.PlaceInTerrarium(dropArea);
    }
}