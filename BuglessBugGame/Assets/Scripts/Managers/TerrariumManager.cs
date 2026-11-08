using System.Collections;
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
    private Coroutine slideRoutine;

    private void Awake()
    {
        position.Enable();
        press.Enable();
        press.performed += _ => { initialPos = currentPos; };
        press.canceled += _ => DetectSwipe();

        leftUIRect = leftUI.GetComponent<RectTransform>();
        shownX = 0;
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
        slideTween?.Kill();
        slideTween = leftUIRect.DOAnchorPosX(hiddenX, slideDuration)
            .SetEase(slideEase)
            .SetUpdate(true);
    }

    public void OpenLeftUI()
    {
        leftUI.SetActive(true);

        // Si l'UI est déjà affichée, on rejoue l'animation depuis la position cachée
        if (Mathf.Abs(leftUIRect.anchoredPosition.x - shownX) < 1f)
            SetX(hiddenX);

        StartSlide(shownX, false);
    }
    
    private void StartSlide(float targetX, bool deactivateAtEnd)
    {
        if (slideRoutine != null) StopCoroutine(slideRoutine);
        slideRoutine = StartCoroutine(SlideRoutine(targetX, deactivateAtEnd));
    }

    private IEnumerator SlideRoutine(float targetX, bool deactivateAtEnd)
    {
        float startX = leftUIRect.anchoredPosition.x;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.unscaledDeltaTime / slideDuration; 
            float eased = 1f - Mathf.Pow(1f - Mathf.Clamp01(t), 3f);
            SetX(Mathf.Lerp(startX, targetX, eased));
            yield return null;
        }

        SetX(targetX);
        if (deactivateAtEnd) leftUI.SetActive(false);
        slideRoutine = null;
    }

    private void SetX(float x)
    {
        leftUIRect.anchoredPosition = new Vector2(x, leftUIRect.anchoredPosition.y);
    }
    

    public void OpenTerrariumUI()
    {
        terrariumUI.SetActive(true);
    }

    public void CloseTerrariumUI()
    {
        slideTween?.Kill();

        terrariumUI.SetActive(false);
    }

    public void ToggleTerrariumUI()
    {
        if (terrariumUI.activeSelf)
            CloseTerrariumUI();
        else
            OpenTerrariumUI();
    }
}