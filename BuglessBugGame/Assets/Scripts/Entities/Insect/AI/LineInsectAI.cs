using System.Collections.Generic;
using UnityEngine;

public class LineInsectAI : InsectBase
{
    [SerializeField] private List<Vector2> pathPoints = new List<Vector2>();

    private int targetIndex;

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
    private Vector2 ToLocal(Vector2 normalized)
    {
        Vector2 size = canvasRectTransform.rect.size;
        return new Vector2((normalized.x - 0.5f) * size.x, (0.5f - normalized.y) * size.y);
    }
}