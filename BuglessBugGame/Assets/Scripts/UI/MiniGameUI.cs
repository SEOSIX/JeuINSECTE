using System.Collections.Generic;
using GamePlayCore;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MiniGameUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI tryCountTxt;
    [SerializeField] private RectTransform insectIconRect;
    [SerializeField] private UILineDrawer lineDrawer;
    [SerializeField] private RectTransform drawingArea;
    [SerializeField] private Canvas canvas;

    [Header("Swipe Settings")]
    [SerializeField] private float swipeHitRadius = 40f;

    [Header("Circle Settings")]
    [SerializeField] private float circleClosureThreshold = 50f;
    [SerializeField] private int minCirclePoints = 10;

    private MiniGameManager miniGameManager;
    private InsectData.TypeCatch typeCatch;

    private List<Vector2> currentPoints = new List<Vector2>();
    private List<Vector2> currentLocalPoints = new List<Vector2>();
    private bool isDrawing;
    
    [HideInInspector] public static int currentTryCount;
    
    private Canvas rootCanvas;

    public void Init(MiniGameManager manager, InsectData bug)
    {
        miniGameManager = manager;
        typeCatch = bug.typeCatch;
        rootCanvas = GameManager.instance.M_UI.journey._parentJourney.GetComponent<Canvas>();
    }

    private void Update()
    {
        HandleInput();
        
        tryCountTxt.text = $"essais restant : {currentTryCount}";
    }

    private void HandleInput()
    {
        Pointer pointer = Pointer.current;
        if (pointer == null) return;

        Vector2 screenPos = pointer.position.ReadValue();
        bool pressed = pointer.press.isPressed;

        if (pressed)
        {
            if (!isDrawing) StartInput(screenPos);
            else AddPoint(screenPos);
        }
        else if (isDrawing)
        {
            EndInput();
        }
    }

    private void StartInput(Vector2 startScreenPos)
    {
        isDrawing = true;
        currentPoints.Clear();
        currentLocalPoints.Clear();

        currentPoints.Add(startScreenPos);
        AppendLocalPoint(startScreenPos);

        lineDrawer.SetPoints(currentLocalPoints);
    }

    private void AddPoint(Vector2 screenPos)
    {
        currentPoints.Add(screenPos);
        AppendLocalPoint(screenPos);

        lineDrawer.SetPoints(currentLocalPoints);
    }

    private void AppendLocalPoint(Vector2 screenPos)
    {
        Camera cam = rootCanvas.renderMode == 
                     RenderMode.ScreenSpaceOverlay ? null : rootCanvas.worldCamera;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            drawingArea, screenPos, cam, out Vector2 localPoint);

        currentLocalPoints.Add(localPoint);
    }

    private void EndInput()
    {
        isDrawing = false;

        switch (typeCatch)
        {
            case InsectData.TypeCatch.DrawCircle:
                CheckCircle();
                break;
            case InsectData.TypeCatch.Swipe:
                CheckSwipe();
                break;
        }

        ResetDrawing();
    }
    
    public void ResetDrawing()
    {
        isDrawing = false;
        currentPoints.Clear();
        currentLocalPoints.Clear();
        lineDrawer.ClearPoints();
    }

    private void CheckFail()
    {
        currentTryCount--;
        tryCountTxt.text = $"essais restant : {currentTryCount}";
        if (currentTryCount <= 0)
        {
            Fail();
        }
    }

    #region DrawCircle

        private void CheckCircle()
        {
            if (typeCatch != InsectData.TypeCatch.DrawCircle) return;

            if (currentPoints.Count < minCirclePoints || insectIconRect == null)
            {
                CheckFail();
                ResetDrawing();
                return;
            }

            float distStartEnd = Vector2.Distance(currentPoints[0], currentPoints[^1]);
            if (distStartEnd > circleClosureThreshold)
            {
                CheckFail();
                ResetDrawing();
                return;
            }

            Vector2 insectScreenPos = insectIconRect.position;

            if (IsPointInsidePolygon(insectScreenPos, currentPoints))
                Success();
            else
            {
                CheckFail();
                ResetDrawing();
            }
        }

        private bool IsPointInsidePolygon(Vector2 point, List<Vector2> polygon)
        {
            bool inside = false;
            int j = polygon.Count - 1;

            for (int i = 0; i < polygon.Count; i++)
            {
                if ((polygon[i].y > point.y) != (polygon[j].y > point.y) &&
                    point.x < (polygon[j].x - polygon[i].x) * (point.y - polygon[i].y) / (polygon[j].y - polygon[i].y) + polygon[i].x)
                {
                    inside = !inside;
                }
                j = i;
            }
            return inside;
        }
        
        

    #endregion

    #region Swipe

        private void CheckSwipe()
        {
            if (typeCatch != InsectData.TypeCatch.Swipe) return;

            if (currentPoints.Count < 2 || insectIconRect == null)
            {
                ResetDrawing();
                CheckFail();
                return;
            }

            Vector2 insectScreenPos = insectIconRect.position;

            for (int i = 0; i < currentPoints.Count - 1; i++)
            {
                float dist = DistancePointToSegment(insectScreenPos, currentPoints[i], currentPoints[i + 1]);
                if (dist <= swipeHitRadius)
                {
                    Success();
                    return;
                }
            }
            CheckFail();
        }

        private float DistancePointToSegment(Vector2 point, Vector2 a, Vector2 b)
        {
            Vector2 ab = b - a;
            float sqrLen = ab.sqrMagnitude;
            if (sqrLen == 0f) return Vector2.Distance(point, a);

            float t = Mathf.Clamp01(Vector2.Dot(point - a, ab) / sqrLen);
            Vector2 projection = a + t * ab;
            return Vector2.Distance(point, projection);
        }

    #endregion

    private void Success() => miniGameManager?.OnMiniGameSuccess();
    private void Fail() => miniGameManager?.OnMiniGameFailed();
}