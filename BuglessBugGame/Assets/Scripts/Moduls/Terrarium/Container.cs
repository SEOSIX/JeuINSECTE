using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// À placer sur l'IMAGE draggable.
/// L'image est enfant d'un "Box" (slot) de SlotObject (qui a un Horizontal Layout Group).
/// - Drag : l'image suit le pointeur (reparentée sur le Canvas pour passer au-dessus de tout).
/// - Drop sur un Terrarium : l'image est reparentée dans le Terrarium (Terrarium.cs).
/// - Drop ailleurs : l'image retourne dans son slot d'origine.
/// </summary>
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(CanvasGroup))]
public class Container : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rect;
    private CanvasGroup canvasGroup;
    private Canvas rootCanvas;
    private RectTransform canvasRect;

    private Transform originalParent;
    private int originalSiblingIndex;
    private bool droppedInTerrarium;
    
    public bool IsInTerrarium => droppedInTerrarium;
    public InsectData Insect { get; private set; }

    public static Transform DraggingFromSlot { get; private set; }

    private Image image;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
        rootCanvas = GetComponentInParent<Canvas>().rootCanvas;
        canvasRect = rootCanvas.transform as RectTransform;
    }
    public void Setup(InsectData insect)
    {
        Insect = insect;

        if (image == null)
            image = GetComponent<Image>();

        image.sprite = insect.insectIcon;
        image.preserveAspect = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (droppedInTerrarium)
            return;

        originalParent = transform.parent;
        originalSiblingIndex = transform.GetSiblingIndex();
        DraggingFromSlot = originalParent;
        transform.SetParent(rootCanvas.transform, true);
        transform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (droppedInTerrarium)
            return;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
                canvasRect, eventData.position, eventData.pressEventCamera, out Vector3 worldPos))
        {
            rect.position = worldPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        if (!droppedInTerrarium)
            ReturnToSlot();

        DraggingFromSlot = null;
    }
    public void PlaceInTerrarium(RectTransform terrariumArea)
    {
        droppedInTerrarium = true;
        transform.SetParent(terrariumArea, true);
        transform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = true;
    }
    public void ReturnToSlot()
    {
        droppedInTerrarium = false;

        if (originalParent == null)
            return;

        transform.SetParent(originalParent, false);
        transform.SetSiblingIndex(originalSiblingIndex);
        LayoutRebuilder.ForceRebuildLayoutImmediate(originalParent as RectTransform);
    }
}