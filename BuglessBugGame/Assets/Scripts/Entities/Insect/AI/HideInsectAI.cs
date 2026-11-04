using GamePlayCore;
using UnityEngine;

public class HideInsectAI : InsectBase
{
    public enum AiState
    {
        Idle,
        InMovements,
        Hide,
        Catched
    }

    [Header("Cachette")]
    [SerializeField] private float arriveDistance = 5f;

    private const float IntervalPickChance = 1.5f;

    private AiState aiState;
    private RectTransform targetSpot;
    private bool arrivedAtSpot;
    private float hideTimer;
    private float directionChangeTimer;
    private float currentVelocity;
    private Vector2 lastHideSpot;
    private Vector3 lastPosition;

    public AiState State => aiState;

    protected override void Init()
    {
        base.Init();

        directionChangeTimer = IntervalPickChance;
        lastPosition = insectTransform.position;
        aiState = AiState.InMovements;
    }

    private void FixedUpdate()
    {
        if (!isFleeing)
            InsectMovements();

        CheckPosition();
    }

    public override void InsectMovements()
    {
        if (aiState == AiState.Hide)
        {
            HideBehaviour();
            return;
        }

        directionChangeTimer -= Time.fixedDeltaTime;
        if (directionChangeTimer <= 0f)
        {
            directionChangeTimer = IntervalPickChance;

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
                directionChangeTimer = IntervalPickChance;
            }
        }
    }
}