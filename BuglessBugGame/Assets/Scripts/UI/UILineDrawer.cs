using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
public class UILineDrawer : Graphic
{
    //tuto suivis pour la réalisation du script
    [SerializeField] private float lineThickness = 8f;
    [SerializeField] private float maxLineLength = 1300f;

    private readonly List<Vector2> points = new List<Vector2>();
    private float currentLength = 0f;

    public float CurrentLength => currentLength;
    public float MaxLineLength => maxLineLength;
    public bool IsAtMaxLength => currentLength >= maxLineLength;

    public void SetPoints(List<Vector2> newPoints)
    {
        points.Clear();
        currentLength = 0f;

        foreach (var p in newPoints)
        {
            if (!TryAddPointInternal(p))
                break;
        }

        if (IsAtMaxLength)
        {
            ClearPoints();
            return;
        }

        SetVerticesDirty();
    }
    public bool AddPoint(Vector2 newPoint)
    {
        bool added = TryAddPointInternal(newPoint);

        if (IsAtMaxLength)
        {
            ClearPoints();
            return false;
        }

        SetVerticesDirty();
        return added;
    }

    private bool TryAddPointInternal(Vector2 newPoint)
    {
        if (points.Count == 0)
        {
            points.Add(newPoint);
            return true;
        }

        Vector2 last = points[points.Count - 1];
        float segmentLength = Vector2.Distance(last, newPoint);

        if (currentLength + segmentLength <= maxLineLength)
        {
            points.Add(newPoint);
            currentLength += segmentLength;
            return true;
        }
        else
        {
            float remaining = maxLineLength - currentLength;
            if (remaining > 0f)
            {
                Vector2 dir = (newPoint - last).normalized;
                Vector2 clampedPoint = last + dir * remaining;
                points.Add(clampedPoint);
                currentLength = maxLineLength;
            }
            return false;
        }
    }

    public void ClearPoints()
    {
        points.Clear();
        currentLength = 0f;
        SetVerticesDirty();
    }
    

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        if (points.Count < 2) return;

        for (int i = 0; i < points.Count - 1; i++)
        {
            DrawSegment(vh, points[i], points[i + 1], i * 4);
        }
    }

    private void DrawSegment(VertexHelper vh, Vector2 a, Vector2 b, int startIndex)
    {
        Vector2 dir = (b - a).normalized;
        Vector2 normal = new Vector2(-dir.y, dir.x) * (lineThickness * 0.5f);

        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;

        vertex.position = a - normal;
        vh.AddVert(vertex);

        vertex.position = a + normal;
        vh.AddVert(vertex);

        vertex.position = b + normal;
        vh.AddVert(vertex);

        vertex.position = b - normal;
        vh.AddVert(vertex);

        vh.AddTriangle(startIndex, startIndex + 1, startIndex + 2);
        vh.AddTriangle(startIndex + 2, startIndex + 3, startIndex);
    }
}