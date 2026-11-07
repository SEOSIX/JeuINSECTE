using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class TerrariumManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject terrariumUI;
    [SerializeField] private GameObject leftUI;

    [Header("Left UI Animation")]
    [SerializeField] private float slideDuration = 0.3f;
    [SerializeField] private float hiddenOffset = 600f;
    [SerializeField] private Ease slideEase = Ease.OutCubic;

    public delegate void OnSwipe(Vector2 direction);
    public event OnSwipe swipePerformed;
    [SerializeField] private InputAction position, press;
    [SerializeField] private float swipeResistance = 100;

    private Vector2 initialPos;
    private Vector2 currentPos => position.ReadValue<Vector2>();

    private RectTransform leftUIRect;
    private float shownX;
    private float hiddenX;
    private Tween slideTween;

    private void Awake()
    {
        position.Enable();
        press.Enable();
        press.performed += _ => { initialPos = currentPos; };
        press.canceled += _ => DetectSwipe();

        leftUIRect = leftUI.GetComponent<RectTransform>();
        shownX = leftUIRect.anchoredPosition.x;
        hiddenX = shownX - hiddenOffset;
    }

    private void OnDestroy()
    {
        slideTween?.Kill();
    }

    private void DetectSwipe()
    {
        Vector2 delta = currentPos - initialPos;
        Vector2 direction = Vector2.zero;

        if (Mathf.Abs(delta.x) > swipeResistance)
            direction.x = Mathf.Sign(delta.x);
        if (Mathf.Abs(delta.y) > swipeResistance)
            direction.y = Mathf.Sign(delta.y);

        if (direction != Vector2.zero)
            swipePerformed?.Invoke(direction);
    }

    public void Swipe(InputAction.CallbackContext context)
    {
        swipePerformed?.Invoke(context.ReadValue<Vector2>());
    }

    public void CloseLeftUI()
    {
        if (!leftUI.activeSelf) return;

        slideTween?.Kill();
        slideTween = leftUIRect
            .DOAnchorPosX(hiddenX, slideDuration)
            .SetEase(slideEase)
            .OnComplete(() => leftUI.SetActive(false));
    }

    public void OpenLeftUI()
    {
        slideTween?.Kill();

        if (!leftUI.activeSelf)
        {
            leftUIRect.anchoredPosition = new Vector2(hiddenX, leftUIRect.anchoredPosition.y);
            leftUI.SetActive(true);
        }

        slideTween = leftUIRect
            .DOAnchorPosX(shownX, slideDuration)
            .SetEase(slideEase);
    }
}