using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TerrariumFocus : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform terrariumsContainer;
    [SerializeField] private Camera cam;

    [Header("Focus")]
    [SerializeField] private float focusDuration = 0.4f;
    [SerializeField] private Ease focusEase = Ease.OutCubic;
    [SerializeField] private float focusZoomFactor = 0.5f;
    [SerializeField] private float offscreenMargin = 2f;

    [Header("Orbit")]
    [SerializeField] private float maxOrbitAngle = 12f;
    [SerializeField] private float orbitSensitivity = -0.05f;
    [SerializeField] private float orbitSmoothing = 12f;
    [SerializeField] private bool returnOnRelease = true;

    public bool IsFocused { get; private set; }
    public Transform FocusedItem { get; private set; }
    public event Action exited;

    private float baseOrthoSize;
    private Vector3 restLocalPos;
    private readonly Dictionary<Transform, Vector3> itemsHomePos = new();

    private bool orbitActive;
    private float orbitYaw, orbitTargetYaw;
    private Vector3 orbitPivot, baseCamPos;
    private Quaternion baseCamRot;

    private void Awake()
    {
        if (cam == null) cam = Camera.main;
    }

    private void OnDestroy()
    {
        if (terrariumsContainer != null) terrariumsContainer.DOKill();
        if (cam != null) cam.DOKill();
    }

    private void Update()
    {
        UpdateOrbit();
    }
    
    public void Enter(Transform item, Vector3 restLocalPos)
    {
        if (orbitActive) cam.transform.SetPositionAndRotation(baseCamPos, baseCamRot);

        baseCamPos = cam.transform.position;
        baseCamRot = cam.transform.rotation;
        orbitYaw = orbitTargetYaw = 0f;
        orbitActive = true;

        IsFocused = true;
        FocusedItem = item;
        this.restLocalPos = restLocalPos;

        terrariumsContainer.DOKill();
        cam.DOKill();
        itemsHomePos.Clear();

        baseOrthoSize = cam.orthographicSize;
        float halfWidth = baseOrthoSize * cam.aspect;
        
        Vector3 center = GetBounds(item).center;
        Vector3 toCenter = center - cam.transform.position;
        float dx = Vector3.Dot(toCenter, cam.transform.right);
        float dy = Vector3.Dot(toCenter, cam.transform.up);

        orbitPivot = center - cam.transform.right * dx - cam.transform.up * dy;

        Vector3 targetPos = terrariumsContainer.position
                            - cam.transform.right * dx
                            - cam.transform.up * dy;

        terrariumsContainer.DOMove(targetPos, focusDuration)
            .SetEase(focusEase).SetUpdate(true);
        
        foreach (Transform other in terrariumsContainer)
        {
            if (other == item || !other.gameObject.activeSelf) continue;

            itemsHomePos[other] = other.localPosition;

            float side = Mathf.Sign(Vector3.Dot(other.position - item.position, cam.transform.right));
            Vector3 worldPush = cam.transform.right * side * (halfWidth + offscreenMargin);
            Vector3 localPush = terrariumsContainer.InverseTransformVector(worldPush);

            Transform o = other;
            o.DOKill();
            o.DOLocalMove(other.localPosition + localPush, focusDuration)
                .SetEase(focusEase).SetUpdate(true)
                .OnComplete(() => { if (IsFocused) o.gameObject.SetActive(false); });
        }
        cam.DOOrthoSize(baseOrthoSize * focusZoomFactor, focusDuration)
            .SetEase(focusEase).SetUpdate(true);
    }

    public void Exit()
    {
        if (!IsFocused) return;
        IsFocused = false;
        FocusedItem = null;
        orbitTargetYaw = 0f;

        cam.DOKill();
        cam.DOOrthoSize(baseOrthoSize, focusDuration).SetEase(focusEase).SetUpdate(true);

        foreach (var kv in itemsHomePos)
        {
            kv.Key.gameObject.SetActive(true);
            kv.Key.DOKill();
            kv.Key.DOLocalMove(kv.Value, focusDuration).SetEase(focusEase).SetUpdate(true);
        }
        itemsHomePos.Clear();

        terrariumsContainer.DOKill();
        terrariumsContainer.DOLocalMove(restLocalPos, focusDuration)
            .SetEase(focusEase).SetUpdate(true)
            .OnComplete(() => exited?.Invoke());
    }
    public void Drag(float deltaXPixels)
    {
        if (!IsFocused) return;
        orbitTargetYaw = Mathf.Clamp(orbitTargetYaw + deltaXPixels * orbitSensitivity, -maxOrbitAngle, maxOrbitAngle);
    }
    public void ReleaseDrag()
    {
        if (IsFocused && returnOnRelease) orbitTargetYaw = 0f;
    }

    private void UpdateOrbit()
    {
        if (!orbitActive) return;

        float k = 1f - Mathf.Exp(-orbitSmoothing * Time.unscaledDeltaTime);
        orbitYaw = Mathf.Lerp(orbitYaw, orbitTargetYaw, k);
        
        if (!IsFocused && Mathf.Abs(orbitYaw) < 0.01f)
        {
            orbitYaw = orbitTargetYaw = 0f;
            orbitActive = false;
        }

        Quaternion rot = Quaternion.AngleAxis(orbitYaw, Vector3.up);
        cam.transform.SetPositionAndRotation(
            orbitPivot + rot * (baseCamPos - orbitPivot),
            rot * baseCamRot);
    }

    private Bounds GetBounds(Transform item)
    {
        var renderers = item.GetComponentsInChildren<Renderer>();
        Bounds b = renderers[0].bounds;
        for (int i = 1; i < renderers.Length; i++) b.Encapsulate(renderers[i].bounds);
        return b;
    }
}