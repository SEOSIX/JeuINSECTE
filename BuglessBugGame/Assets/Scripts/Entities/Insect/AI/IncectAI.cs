using UnityEngine;

public class IncectAI : MonoBehaviour
{

    [SerializeField] private RectTransform insectTransform;
    [SerializeField] private float coefFuite = 2f;
    
    [Header("Cachette")]
    [SerializeField] private float arriveDistance = 5f;

    private RectTransform targetSpot;
    private bool arrivedAtSpot;
    private float hideTimer;
    
    public enum AiState
    {
        Idle,
        InMovements,
        Hide,
        Catched
    }
    private InsectData data;
    private MiniGameManager miniGameManager;
    private AiState aiState;
    
    private float currentVelocity;
    private float directionChangeTimer;
    private RectTransform canvasRectTransform;
    private Vector2 direction;
    private Vector2 screenLimit;
    private Vector2 lastHideSpot;
    private Vector3 lastPosition;
    private float intervalPickChance = 1.5f;
    
    public bool isHidden{ get; private set; }
    public bool isFleeing { get; private set; }

    void OnEnable()
    {
        Init();
    }
    
    void FixedUpdate()
    {
        if (!isFleeing)
        {
            if (aiState == AiState.Hide) HideBehaviour();
            else InsectMovements();
        }
        Debug.Log(isHidden);
        CheckPosition();
    }

    void Init()
    {
        data = GameManager.instance.M_MiniGameManager.currentInsect.bug;
        canvasRectTransform = GetComponent<RectTransform>();
        
        miniGameManager = GameManager.instance.M_MiniGameManager;
        direction = GetRandomDirection();
        
        directionChangeTimer = intervalPickChance;
        lastPosition = insectTransform.position;
    }

    
    public void StartRunAway()
    {
        isFleeing = true;
        Vector2 pos = insectTransform.anchoredPosition;
        direction = pos.sqrMagnitude > 0.01f ? pos.normalized : GetRandomDirection();
    }
    
    public bool RunAwayStep(float dt)
    {
        insectTransform.anchoredPosition += direction * (data.bugSpeed * coefFuite) * dt;
        return IsOutsideScreen();
    }

    private bool IsOutsideScreen()
    {
        Vector2 half = canvasRectTransform.rect.size * 0.5f;
        Vector2 halfInsect = insectTransform.rect.size * 0.5f;
        Vector2 pos = insectTransform.anchoredPosition;

        return Mathf.Abs(pos.x) - halfInsect.x >= half.x
               || Mathf.Abs(pos.y) - halfInsect.y >= half.y;
    }
    private void InsectMovements()
    {
        directionChangeTimer -= Time.fixedDeltaTime;
        if (directionChangeTimer <= 0f)
        {
            directionChangeTimer = intervalPickChance;
            

            if (Random.value < data.hideChance && TryGetNearestSpot(out targetSpot) && lastHideSpot != targetSpot.anchoredPosition)
            {
                lastHideSpot = targetSpot.anchoredPosition;
                
                aiState = AiState.Hide;
                arrivedAtSpot = false;
                return;
            }
            if (Random.value < data.changeDirectionPercentage)
                direction = GetRandomDirection();
        }
        insectTransform.anchoredPosition += direction * data.bugSpeed * Time.fixedDeltaTime;

        LimitToScreen();
        

        currentVelocity = (insectTransform.position - lastPosition).magnitude / Time.fixedDeltaTime;
        lastPosition = insectTransform.position;
    }

    private Vector2 GetRandomDirection()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    private void LimitToScreen()
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

    private void CheckPosition()
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
    
    private bool TryGetNearestSpot(out RectTransform nearest)
    {
        nearest = null;
        float best = float.MaxValue;
        foreach (RectTransform spot in miniGameManager.currentMiniGameUI.hideSpot)
        {
            float d = (GetSpotPos(spot) - insectTransform.anchoredPosition).sqrMagnitude;
            if (d < best) { best = d; nearest = spot; }
        }
        return nearest != null;
    }

    private Vector2 GetSpotPos(RectTransform spot)
    {
        return canvasRectTransform.InverseTransformPoint(spot.position);
    }

    private void HideBehaviour()
    {
        currentVelocity = 0f;

        if (!arrivedAtSpot)
        {
            Vector2 toTarget = GetSpotPos(targetSpot) - insectTransform.anchoredPosition;
            if (toTarget.magnitude <= arriveDistance)
            {
                arrivedAtSpot = true;
                hideTimer = data.hideTime;
            }
            else
            {
                insectTransform.anchoredPosition +=
                    toTarget.normalized * data.bugSpeed * Time.fixedDeltaTime;
            }
        }
        else
        {
            hideTimer -= Time.fixedDeltaTime;
            if (hideTimer <= 0f)
            {
                aiState = AiState.InMovements;
                direction = GetRandomDirection();
                directionChangeTimer = intervalPickChance;
            }
        }
    }
}
