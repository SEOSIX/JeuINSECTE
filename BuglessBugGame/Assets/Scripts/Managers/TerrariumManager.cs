using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class TerrariumManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject leftUI;

    [Header("Animation")]
    [SerializeField] private float slideDuration = 0.3f;
    [SerializeField] private Ease slideEase = Ease.OutCubic;

    [Header("Left UI (2D)")]
    [SerializeField] private float leftUIHiddenOffset = 600f;

    [Header("Terrariums")]
    [SerializeField] private Transform terrariumsContainer;
    [SerializeField] private float terrariumsHiddenOffset = 6f;
    [SerializeField] private TerrariumFocus focus;

    [Header("Terrariums Scroll")]
    [SerializeField] private float minScroll = 0f;
    [SerializeField] private float maxScroll = 10f;
    [SerializeField] private float scrollSmoothing = 15f;
    [SerializeField] private Camera cam;

    [Header("Terrariums Touch")]
    [SerializeField] private float tapThreshold = 20f;
    [SerializeField] private LayerMask terrariumsLayerMask;

    public Action<Transform> terrariumSelected;

    public delegate void OnSwipe(Vector2 direction);
    public event OnSwipe swipePerformed;
    [SerializeField] private InputAction position, press;
    [SerializeField] private float swipeResistance = 100;

    private Vector2 initialPos;
    private Vector2 lastPos;
    private Vector2 currentPos => position.ReadValue<Vector2>();

    private RectTransform leftUIRect;
    private float leftShownX, leftHiddenX;
    private Vector3 terrariumsShownPos, terrariumsHiddenPos;

    private bool isLeftUIOpen = true;
    private bool wasAtMaxOnPress;
    private bool isDragging;
    private bool canScroll;
    private float scroll, targetScroll;

    public bool IsLeftUIOpen => isLeftUIOpen;
    public bool WasAtMaxOnPress => wasAtMaxOnPress;

    private void Awake()
    {
        if (cam == null) cam = Camera.main;

        leftUIRect = leftUI.GetComponent<RectTransform>();
        leftShownX = leftUIRect.anchoredPosition.x;
        leftHiddenX = leftShownX - leftUIHiddenOffset;

        terrariumsShownPos = terrariumsContainer.localPosition;
        terrariumsHiddenPos = terrariumsShownPos + Vector3.left * terrariumsHiddenOffset;
        leftUI.SetActive(true);
        leftUIRect.anchoredPosition = new Vector2(leftShownX, leftUIRect.anchoredPosition.y);

        scroll = targetScroll = Mathf.Clamp(0f, minScroll, maxScroll);
        terrariumsContainer.localPosition = terrariumsHiddenPos;
        terrariumsContainer.gameObject.SetActive(false);
        canScroll = false;
    }

    private void OnEnable()
    {
        position.Enable();
        press.Enable();
        press.performed += OnPressStarted;
        press.canceled += OnPressEnded;
        if (focus != null) focus.exited += OnFocusExited;
    }

    private void OnDisable()
    {
        press.performed -= OnPressStarted;
        press.canceled -= OnPressEnded;
        position.Disable();
        press.Disable();
        if (focus != null) focus.exited -= OnFocusExited;
    }

    private void OnDestroy()
    {
        leftUIRect.DOKill();
        terrariumsContainer.DOKill();
    }

    private void OnValidate()
    {
        if (maxScroll < minScroll) maxScroll = minScroll;
    }

    private void Update()
    {
        // Pendant le focus, le glissement pilote l'orbit de la caméra
        if (focus != null && focus.IsFocused)
        {
            if (isDragging)
            {
                float dx = currentPos.x - lastPos.x;
                lastPos = currentPos;
                focus.Drag(dx);
            }
            return;
        }

        if (!canScroll)
        {
            if (isDragging) lastPos = currentPos;
            return;
        }

        if (isDragging)
        {
            float deltaPx = currentPos.x - lastPos.x;
            lastPos = currentPos;
            targetScroll = Mathf.Clamp(targetScroll + deltaPx * WorldUnitsPerPixel(), minScroll, maxScroll);
        }

        float k = 1f - Mathf.Exp(-scrollSmoothing * Time.unscaledDeltaTime);
        scroll = Mathf.Lerp(scroll, targetScroll, k);
        terrariumsContainer.localPosition = terrariumsShownPos + Vector3.right * scroll;
    }

    private float WorldUnitsPerPixel()
    {
        if (cam.orthographic)
            return cam.orthographicSize * 2f / Screen.height;

        float dist = Vector3.Dot(terrariumsContainer.position - cam.transform.position, cam.transform.forward);
        return 2f * dist * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad) / Screen.height;
    }


    #region Input

    private void OnPressStarted(InputAction.CallbackContext ctx)
    {
        initialPos = lastPos = currentPos;
        isDragging = true;

        wasAtMaxOnPress = canScroll && targetScroll >= maxScroll - 0.01f;
    }

    private void OnPressEnded(InputAction.CallbackContext ctx)
    {
        isDragging = false;

        bool isTap = (lastPos - initialPos).magnitude < tapThreshold;

        if (focus != null && focus.IsFocused)
        {
            if (isTap)
            {
                Transform tapped = RaycastItem(lastPos);
                if (tapped != focus.FocusedItem) focus.Exit();
            }
            else
            {
                focus.ReleaseDrag();
            }
            return;
        }

        if (canScroll && isTap)
        {
            Transform item = RaycastItem(lastPos);
            if (item != null)
            {
                canScroll = false;
                focus.Enter(item, terrariumsShownPos + Vector3.right * scroll);
                terrariumSelected?.Invoke(item);
            }
            return;
        }

        DetectSwipe();
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

    private Transform RaycastItem(Vector2 screenPos)
    {
        Ray ray = cam.ScreenPointToRay(screenPos);
        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, terrariumsLayerMask, QueryTriggerInteraction.Collide))
            return null;

        Transform item = hit.transform;
        while (item != null && item.parent != terrariumsContainer)
            item = item.parent;

        return item;
    }

    public void OpenLeftUI()
    {
        if (focus != null) focus.Exit();

        isLeftUIOpen = true;
        canScroll = false;
        isDragging = false;

        leftUI.SetActive(true);
        leftUIRect.DOKill();
        leftUIRect.anchoredPosition = new Vector2(leftHiddenX, leftUIRect.anchoredPosition.y);
        leftUIRect.DOAnchorPosX(leftShownX, slideDuration)
            .SetEase(slideEase)
            .SetUpdate(true);
    }

    public void CloseLeftUI()
    {
        if (focus != null && focus.IsFocused) return;

        isLeftUIOpen = false;
        leftUIRect.DOKill();
        leftUIRect.DOAnchorPosX(leftHiddenX, slideDuration)
            .SetEase(slideEase)
            .SetUpdate(true)
            .OnComplete(() => leftUI.SetActive(false));

        terrariumsContainer.gameObject.SetActive(true);
        terrariumsContainer.DOKill();
        terrariumsContainer.DOLocalMove(terrariumsShownPos + Vector3.right * scroll, slideDuration)
            .SetEase(slideEase)
            .SetUpdate(true)
            .OnComplete(() => canScroll = true);
    }

    private void OnFocusExited()
    {
        canScroll = !isLeftUIOpen;
    }

    #endregion
}