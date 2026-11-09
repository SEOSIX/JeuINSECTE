using System;
using System.Collections;
using System.Collections.Generic;
using Moduls;
using UnityEngine;
using UnityEngine.UI;

public class Encyclopedia : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private EncyclopediaData data;

    [Header("ParentsUI")]
    [SerializeField] private GameObject encyclopediaUI;

    [Header("Buttons")]
    [SerializeField] private Button openBookButton;
    [SerializeField] private Button closeBookButton;
    [SerializeField] private Button nextPageBookButton;
    [SerializeField] private Button previousPageBookButton;

    [Header("Pages")]
    [SerializeField] private EncyclopediaPage leftPage;
    [SerializeField] private EncyclopediaPage rightPage;

    [Header("Prefab")]
    [SerializeField] private GameObject insectPrefab;

    [Header("Pool")]
    [SerializeField] private Transform cellPool;

    [Header("Slide")]
    [SerializeField] private float slideDuration = 0.5f;
    [SerializeField] private AnimationCurve slideCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    public EncyclopediaData Data => data;

    private const int InsectsPerSpread = EncyclopediaPage.MaxInsectsPerPage * 2;
    private int currentSpread = 0;

    private readonly List<EncyclopediaCell> allCells = new List<EncyclopediaCell>();

    private RectTransform uiRect;
    private Vector2 uiTargetPos;
    private Coroutine slideRoutine;

    private void OnEnable()
    {
        //PlayerInsectPickUp.OnInsectPicked += AddToBook;
    }

    private void OnDisable()
    {
        //PlayerInsectPickUp.OnInsectPicked -= AddToBook;
    }

    void Awake()
    {
        openBookButton.onClick.AddListener(OpenBook);
        closeBookButton.onClick.AddListener(CloseBook);
        nextPageBookButton.onClick.AddListener(NextSpread);
        previousPageBookButton.onClick.AddListener(PreviousSpread);

        if (encyclopediaUI != null)
        {
            uiRect = encyclopediaUI.GetComponent<RectTransform>();
            uiTargetPos = uiRect.anchoredPosition;
        }

        InitializeFromData();
    }

    private void InitializeFromData()
    {
        if (data == null) return;

        foreach (InsectData insectData in data.encyclopediaData)
        {
            CreateCell(insectData);
        }
    }

    void OpenBook()
    {
        if (encyclopediaUI == null) return;

        Vector2 from;
        if (encyclopediaUI.activeSelf)
        {
            from = uiRect.anchoredPosition;
        }
        else
        {
            encyclopediaUI.SetActive(true);
            Canvas.ForceUpdateCanvases();
            from = uiTargetPos + Vector2.down * GetSlideDistance();
            uiRect.anchoredPosition = from;
        }

        leftPage.ShowGrid();
        rightPage.ShowGrid();
        RefreshCurrentSpread();

        StartSlide(from, uiTargetPos, null);
    }

    void CloseBook()
    {
        if (encyclopediaUI == null || !encyclopediaUI.activeSelf) return;

        Vector2 offscreen = uiTargetPos + Vector2.down * GetSlideDistance();

        StartSlide(uiRect.anchoredPosition, offscreen, () =>
        {
            encyclopediaUI.SetActive(false);
            uiRect.anchoredPosition = uiTargetPos;
        });
    }

    private float GetSlideDistance()
    {
        Canvas root = uiRect.GetComponentInParent<Canvas>().rootCanvas;
        return ((RectTransform)root.transform).rect.height;
    }

    private void StartSlide(Vector2 from, Vector2 to, Action onComplete)
    {
        if (slideRoutine != null) StopCoroutine(slideRoutine);
        slideRoutine = StartCoroutine(SlideRoutine(from, to, onComplete));
    }

    private IEnumerator SlideRoutine(Vector2 from, Vector2 to, Action onComplete)
    {
        float elapsed = 0f;

        while (elapsed < slideDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = slideCurve.Evaluate(Mathf.Clamp01(elapsed / slideDuration));
            uiRect.anchoredPosition = Vector2.LerpUnclamped(from, to, t);
            yield return null;
        }

        uiRect.anchoredPosition = to;
        slideRoutine = null;
        onComplete?.Invoke();
    }

    void AddToBook(InsectData insectData, int count = 1)
    {
        if (data == null) return;

        if (!data.encyclopediaData.Contains(insectData))
        {
            data.encyclopediaData.Add(insectData);
            CreateCell(insectData);

            if (encyclopediaUI != null && encyclopediaUI.activeInHierarchy)
                RefreshCurrentSpread();
        }
    }

    private void CreateCell(InsectData insectData)
    {
        GameObject newInsect = Instantiate(insectPrefab, cellPool);
        EncyclopediaCell cell = newInsect.GetComponent<EncyclopediaCell>();
        cell.SetupInsectEncyclopedia(insectData);
        cell.OnCellClicked += (insect) => HandleCellClicked(insect, cell);

        allCells.Add(cell);
    }

    private void RefreshCurrentSpread()
    {
        leftPage.ShowGrid();
        rightPage.ShowGrid();

        int startIndex = currentSpread * InsectsPerSpread;

        for (int i = 0; i < allCells.Count; i++)
        {
            EncyclopediaCell cell = allCells[i];

            if (i >= startIndex && i < startIndex + EncyclopediaPage.MaxInsectsPerPage)
            {
                cell.transform.SetParent(leftPage.Container, false);
                cell.gameObject.SetActive(true);
            }
            else if (i >= startIndex + EncyclopediaPage.MaxInsectsPerPage && i < startIndex + InsectsPerSpread)
            {
                cell.transform.SetParent(rightPage.Container, false);
                cell.gameObject.SetActive(true);
            }
            else
            {
                cell.transform.SetParent(cellPool, false);
                cell.gameObject.SetActive(false);
            }
        }
        UpdateNavigationButtons();
    }

    private void HandleCellClicked(InsectData insect, EncyclopediaCell cell)
    {
        if (cell.transform.parent == leftPage.Container)
            rightPage.ShowDetail(insect);
        else if (cell.transform.parent == rightPage.Container)
            leftPage.ShowDetail(insect);
    }

    private int MaxSpread => allCells.Count == 0 ? 0 : (allCells.Count - 1) / InsectsPerSpread;

    private void UpdateNavigationButtons()
    {
        nextPageBookButton.interactable = allCells.Count > 0 && currentSpread < MaxSpread;
        previousPageBookButton.interactable = currentSpread > 0;
    }

    public void NextSpread()
    {
        if (currentSpread < MaxSpread)
        {
            currentSpread++;
            RefreshCurrentSpread();
        }
    }

    public void PreviousSpread()
    {
        if (currentSpread > 0)
        {
            currentSpread--;
            RefreshCurrentSpread();
        }
    }
}