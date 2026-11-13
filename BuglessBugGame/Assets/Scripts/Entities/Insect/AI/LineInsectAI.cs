using System.Collections.Generic;
using UnityEngine;

public class LineInsectAI : InsectBase
{
    [SerializeField] private List<Vector2> pathPoints = new List<Vector2>();

    private int targetIndex;
    private bool isHidden;

    public bool IsHidden => isHidden;

    public void SetPath(List<Vector2> points)
    {
        pathPoints = points;
    }

    protected override void Init()
    {
        base.Init();

        targetIndex = 0;
        if (pathPoints.Count > 0)
        {
            insectTransform.anchoredPosition = ToLocal(pathPoints[0]);
            targetIndex = pathPoints.Count > 1 ? 1 : 0;
        }
    }

    private void FixedUpdate()
    {
        if (!isFleeing)
            InsectMovements();

        CheckPosition();
        UpdateHiddenState();
    }

    public override void InsectMovements()
    {
        if (pathPoints.Count < 2) return;

        Vector2 target = ToLocal(pathPoints[targetIndex]);
        Vector2 toTarget = target - insectTransform.anchoredPosition;
        float step = data.bugSpeed * Time.fixedDeltaTime;

        if (toTarget.magnitude <= step)
        {
            insectTransform.anchoredPosition = target;
            targetIndex = (targetIndex + 1) % pathPoints.Count;
            return;
        }

        direction = toTarget.normalized;
        insectTransform.anchoredPosition += direction * step;
    }

    private void UpdateHiddenState()
    {
        isHidden = false;

        var ui = miniGameManager.currentMiniGameUI;
        if (ui == null || ui.hideSpot == null) return;

        foreach (RectTransform spot in ui.hideSpot)
        {
            Vector2 local = spot.InverseTransformPoint(insectTransform.position);
            if (spot.rect.Contains(local))
            {
                isHidden = true;
                return;
            }
        }
    }

    private Vector2 ToLocal(Vector2 normalized)
    {
        Vector2 size = canvasRectTransform.rect.size;
        return new Vector2((normalized.x - 0.5f) * size.x, (0.5f - normalized.y) * size.y);
    }
}