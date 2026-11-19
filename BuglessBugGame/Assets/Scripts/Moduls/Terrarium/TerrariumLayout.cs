using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TerrariumLayout : MonoBehaviour
{
    [Header("Layout")]
    [SerializeField] private float spacing = 2f;
    [SerializeField] private bool centered = true;
    [SerializeField] private Vector3 axis = Vector3.right;

    [Header("Animation")]
    [SerializeField] private bool animate = true;
    [SerializeField] private float duration = 0.3f;
    [SerializeField] private Ease ease = Ease.OutCubic;

    private readonly List<Transform> items = new();

    private void Start()
    {
        foreach (Transform child in transform)
            if (child.gameObject.activeSelf)
                items.Add(child);

        Refresh(false);
    }

    public void Add(Transform item)
    {
        item.SetParent(transform, false);
        items.Add(item);
        Refresh(animate);
    }

    public void Remove(Transform item)
    {
        items.Remove(item);
        Refresh(animate);
    }

    public void Refresh(bool animated)
    {
        int count = items.Count;
        if (count == 0) return;

        float totalWidth = (count - 1) * spacing;
        float start = centered ? -totalWidth * 0.5f : 0f;
        Vector3 dir = axis.normalized;

        for (int i = 0; i < count; i++)
        {
            Vector3 target = dir * (start + i * spacing);
            Transform t = items[i];

            t.DOKill();
            if (animated)
                t.DOLocalMove(target, duration).SetEase(ease);
            else
                t.localPosition = target;
        }
    }
    private void OnValidate()
    {
        if (!Application.isPlaying) return;
        Refresh(false);
    }

    private void OnDestroy()
    {
        foreach (var t in items)
            if (t != null) t.DOKill();
    }
}