using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
public class UILineDrawer : Graphic
{
    public enum DrawMode
    {
        Freehand,
        StraightLine
    }

    [Header("Mode")]
    [SerializeField] private DrawMode drawMode = DrawMode.Freehand;

    [Header("Settings")]
    [SerializeField] private float lineThickness = 8f;
    [SerializeField] private float maxLineLength = 1300f;
    [SerializeField] private float fadeDuration = 0.4f;

    private readonly List<Vector2> points = new List<Vector2>();
    private float currentLength = 0f;

    private struct FadingSegment
    {
        public Vector2 start;
        public Vector2 end;
        public float alpha;
    }

    private readonly List<FadingSegment> fadingSegments = new List<FadingSegment>();

    public DrawMode CurrentDrawMode
    {
        get => drawMode;
        set => drawMode = value;
    }

    public float CurrentLength => currentLength;
    public float MaxLineLength => maxLineLength;
    public bool IsAtMaxLength => currentLength >= maxLineLength;

    public bool HasActiveLine => points.Count >= 2;
    public Vector2 ActiveStart => points.Count >= 2 ? points[0] : Vector2.zero;
    public Vector2 ActiveEnd => points.Count >= 2 ? points[points.Count - 1] : Vector2.zero;

    private void Update()
    {
        if (fadingSegments.Count == 0) return;

        for (int i = fadingSegments.Count - 1; i >= 0; i--)
        {
            FadingSegment seg = fadingSegments[i];
            seg.alpha -= Time.deltaTime / fadeDuration;

            if (seg.alpha <= 0f)
                fadingSegments.RemoveAt(i);
            else
                fadingSegments[i] = seg;
        }

        SetVerticesDirty();
    }
    
    public void BeginLine(Vector2 startPoint)
    {
        if (drawMode == DrawMode.StraightLine && points.Count >= 2)
        {
            fadingSegments.Add(new FadingSegment
            {
                start = points[0],
                end = points[points.Count - 1],
                alpha = 1f
            });
        }

        points.Clear();
        points.Add(startPoint);
        currentLength = 0f;
        SetVerticesDirty();
    }

    public void SetPoints(List<Vector2> newPoints)
    {
        if (newPoints == null || newPoints.Count == 0)
        {
            points.Clear();
            currentLength = 0f;
            SetVerticesDirty();
            return;
        }

        if (drawMode == DrawMode.StraightLine)
        {
            UpdateStraightLine(newPoints[0], newPoints[newPoints.Count - 1]);
            return;
        }

        // Comportement Freehand d'origine
        points.Clear();
        currentLength = 0f;

        foreach (var p in newPoints)
        {
            if (!TryAddPoint(p)) break;
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
        bool added = TryAddPoint(newPoint);
        if (drawMode == DrawMode.StraightLine)
        {
            Vector2 start = points.Count > 0 ? points[0] : newPoint;
            UpdateStraightLine(start, newPoint);
            return !IsAtMaxLength;
        }
        
        if (IsAtMaxLength)
        {
            ClearPoints();
            return false;
        }

        SetVerticesDirty();
        return added;
    }

    private void UpdateStraightLine(Vector2 start, Vector2 target)
    {
        Vector2 delta = target - start;
        float dist = delta.magnitude;

        Vector2 end = dist > maxLineLength
            ? start + delta.normalized * maxLineLength
            : target;

        currentLength = Mathf.Min(dist, maxLineLength);

        points.Clear();
        points.Add(start);
        points.Add(end);

        SetVerticesDirty();
    }

    private bool TryAddPoint(Vector2 newPoint)
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
        fadingSegments.Clear();
        currentLength = 0f;
        SetVerticesDirty();
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        int vertIndex = 0;
        
        foreach (var faded in fadingSegments)
        {
            Color c = color;
            c.a *= faded.alpha;
            DrawSegment(vh, faded.start, faded.end, vertIndex, c);
            vertIndex += 4;
        }

        if (points.Count < 2) return;

        for (int i = 0; i < points.Count - 1; i++)
        {
            DrawSegment(vh, points[i], points[i + 1], vertIndex, color);
            vertIndex += 4;
        }
    }

    private void DrawSegment(VertexHelper vh, Vector2 a, Vector2 b, int startIndex, Color segColor)
    {
        if (Vector2.SqrMagnitude(b - a) < 0.001f) return;

        Vector2 dir = (b - a).normalized;
        Vector2 normal = new Vector2(-dir.y, dir.x) * (lineThickness * 0.5f);

        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = segColor;

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