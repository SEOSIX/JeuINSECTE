using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIChalet : MonoBehaviour
{
    [Header("RefUI")]
    [SerializeField] public Button _goWalkButton;
    [SerializeField] public GameObject _parentLobbyUI;
    [SerializeField] public GameObject _parentMapUI;
    [SerializeField] private RectTransform _mapContent;

    [Header("Slide")]
    [SerializeField] private float _slideDuration = 0.5f;
    [SerializeField] private AnimationCurve _slideCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    private Vector2 _mapTargetPos;
    private Coroutine _slideRoutine;

    void Start()
    {
        _mapTargetPos = _mapContent.anchoredPosition;
        _goWalkButton.onClick.AddListener(GoToMap);
    }

    private float GetSlideDistance()
    {
        Canvas.ForceUpdateCanvases();
        RectTransform canvasRect = (RectTransform)_parentMapUI.transform;
        return canvasRect.rect.width;
    }

    private void GoToMap()
    {
        _goWalkButton.interactable = false;
        GameManager.instance.player.playerData.playerInventoryData.insects.Clear();

        _parentMapUI.SetActive(true);
        Canvas.ForceUpdateCanvases();

        Vector2 offscreen = _mapTargetPos + Vector2.left * GetSlideDistance();
        _mapContent.anchoredPosition = offscreen;

        StartSlide(offscreen, _mapTargetPos, () =>
        {
            _parentLobbyUI.SetActive(false);
            _goWalkButton.interactable = true;
        });
    }

    public void GoToLobby(Action onComplete = null)
    {
        _parentLobbyUI.SetActive(true);

        Vector2 offscreen = _mapTargetPos + Vector2.left * GetSlideDistance();

        StartSlide(_mapContent.anchoredPosition, offscreen, () =>
        {
            _parentMapUI.SetActive(false);
            _mapContent.anchoredPosition = _mapTargetPos;
            onComplete?.Invoke();
        });
    }

    private void StartSlide(Vector2 from, Vector2 to, Action onComplete)
    {
        if (_slideRoutine != null) StopCoroutine(_slideRoutine);
        _slideRoutine = StartCoroutine(SlideRoutine(from, to, onComplete));
    }

    private IEnumerator SlideRoutine(Vector2 from, Vector2 to, Action onComplete)
    {
        float elapsed = 0f;

        while (elapsed < _slideDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = _slideCurve.Evaluate(Mathf.Clamp01(elapsed / _slideDuration));
            _mapContent.anchoredPosition = Vector2.LerpUnclamped(from, to, t);
            yield return null;
        }

        _mapContent.anchoredPosition = to;
        _slideRoutine = null;
        onComplete?.Invoke();
    }

    private void OnDestroy()
    {
        _goWalkButton.onClick.RemoveListener(GoToMap);
    }
}