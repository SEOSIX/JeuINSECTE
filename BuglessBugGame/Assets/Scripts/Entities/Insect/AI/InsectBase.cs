using GamePlayCore;
using UnityEngine;

public abstract class InsectBase : MonoBehaviour, IInsect
{
    [SerializeField] protected RectTransform insectTransform;
    [SerializeField] protected float coefFuite = 2f;

    protected InsectData data;
    protected MiniGameManager miniGameManager;
    protected RectTransform canvasRectTransform;
    protected Vector2 direction;

    public bool isHidden { get; protected set; }
    public bool isFleeing { get; protected set; }

    protected virtual void OnEnable()
    {
        Init();
    }

    protected virtual void Init()
    {
        miniGameManager = GameManager.instance.M_MiniGameManager;
        data = miniGameManager.currentInsect._insectPicked;
        canvasRectTransform = GetComponent<RectTransform>();
        direction = GetRandomDirection();
    }

    public virtual void InsectMovements()
    {
        throw new System.NotImplementedException();
    }

    public void LimitToScreen()
    {
        Vector2 half = canvasRectTransform.rect.size * 0.5f;
        Vector2 halfInsect = insectTransform.rect.size * 0.5f;
        Vector2 min = -half + halfInsect;
        Vector2 max = half - halfInsect;

        Vector2 pos = insectTransform.anchoredPosition;
        if (pos.x <= min.x)
        {
            pos.x = min.x;
            direction.x = Mathf.Abs(direction.x);
        }
        else if (pos.x >= max.x)
        {
            pos.x = max.x;
            direction.x = -Mathf.Abs(direction.x);
        }
        if (pos.y <= min.y)
        {
            pos.y = min.y;
            direction.y = Mathf.Abs(direction.y);
        }
        else if (pos.y >= max.y)
        {
            pos.y = max.y;
            direction.y = -Mathf.Abs(direction.y);
        }

        direction.Normalize();
        insectTransform.anchoredPosition = pos;
    }

    public void RunAway()
    {
        if (!isFleeing)
        {
            isFleeing = true;
            Vector2 pos = insectTransform.anchoredPosition;
            direction = pos.sqrMagnitude > 0.01f ? pos.normalized : GetRandomDirection();
        }
        insectTransform.anchoredPosition += direction * (data.bugSpeed * coefFuite) * Time.deltaTime;
    }

    public void CheckPosition()
    {
        isHidden = false;
        foreach (RectTransform hidingSpots in miniGameManager.currentMiniGameUI.hideSpot)
        {
            Vector2 pos = hidingSpots.InverseTransformPoint(insectTransform.position);

            if (hidingSpots.rect.Contains(pos))
            {
                isHidden = true;
                return;
            }
        }
    }

    public bool IsOutsideScreen()
    {
        Vector2 half = canvasRectTransform.rect.size * 0.5f;
        Vector2 halfInsect = insectTransform.rect.size * 0.5f;
        Vector2 pos = insectTransform.anchoredPosition;

        return Mathf.Abs(pos.x) - halfInsect.x >= half.x
               || Mathf.Abs(pos.y) - halfInsect.y >= half.y;
    }

    protected Vector2 GetRandomDirection()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }
}