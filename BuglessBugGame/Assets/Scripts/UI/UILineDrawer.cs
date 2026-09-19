using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
public class UILineDrawer : Graphic
{
    //tuto suivis pour la réalisation du script
    [SerializeField] private float lineThickness = 8f;

    private readonly List<Vector2> points = new List<Vector2>();

    public void SetPoints(List<Vector2> newPoints)
    {
        points.Clear();
        points.AddRange(newPoints);
        SetVerticesDirty();
    }

    public void ClearPoints()
    {
        points.Clear();
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