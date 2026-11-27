using System.Collections;
using UnityEngine;

public class BlockInsectAI : InsectBase
{
    private UILineDrawer lineDrawer;
    private float bounceCooldown;
    private float wavePhase;
    private float noiseSeed;
    private Vector2 currentMoveDirection;
    private bool hasEscaped;
    private bool caugth;

    protected override void Init()
    {
        base.Init();
        TryCacheReferences();

        hasEscaped = false;
        caugth = false;
        noiseSeed = Random.Range(0f, 100f);
        wavePhase = Random.Range(0f, Mathf.PI * 2f);

        direction = GetExitDirection(preferClosestEdge: true);
        currentMoveDirection = direction;

        UpdateRotation(instant: true);
        StartCoroutine(CheckForVictory());
    }

    private bool TryCacheReferences()
    {
        if (lineDrawer != null) return true;

        if (miniGameManager != null && miniGameManager.currentMiniGameUI != null)
            lineDrawer = miniGameManager.currentMiniGameUI.LineDrawer;

        return lineDrawer != null;
    }

    private void FixedUpdate()
    {
        if (hasEscaped) return;
        if (caugth) return;

        if (bounceCooldown > 0f)
            bounceCooldown -= Time.fixedDeltaTime;

        if (!isFleeing)
        {
            InsectMovements();
        }
        else
        {
            currentMoveDirection = direction;
            RunAway();
        }

        UpdateRotation();

        if (IsOutsideScreen())
        {
            hasEscaped = true;
            miniGameManager?.OnMiniGameFailed();
        }
    }

    public override void InsectMovements()
    {
        var blockingData = miniGameManager.currentInsect._insectPicked.insectBlockingData;

        float speedVariation = 1f + (Mathf.PerlinNoise(Time.time * 0.8f, noiseSeed) - 0.5f) *
            blockingData.organicVariation;

        wavePhase += blockingData.zigzagFrequency * speedVariation * Time.fixedDeltaTime;

        float waveAngle = Mathf.Sin(wavePhase) * blockingData.zigzagAmplitude;
        currentMoveDirection = RotateVector(direction, waveAngle).normalized;

        float stepDistance = data.bugSpeed * Time.fixedDeltaTime;
        if (bounceCooldown <= 0f && CheckLineInFront(stepDistance))
        {
            BounceOffLine();
            currentMoveDirection = direction;
        }

        insectTransform.anchoredPosition += currentMoveDirection * stepDistance;
    }

    private Vector2 GetExitDirection(bool preferClosestEdge)
    {
        Vector2 half = canvasRectTransform.rect.size * 0.5f;
        Vector2 pos = insectTransform.anchoredPosition;

        if (preferClosestEdge)
        {
            float distRight = half.x - pos.x;
            float distLeft = pos.x + half.x;
            float distTop = half.y - pos.y;
            float distBottom = pos.y + half.y;

            float minDist = Mathf.Min(distRight, distLeft, distTop, distBottom);
            if (Mathf.Approximately(minDist, distRight))
                return new Vector2(half.x + 100f - pos.x, Random.Range(-half.y * 0.5f, half.y * 0.5f) - pos.y).normalized;
            if (Mathf.Approximately(minDist, distLeft))
                return new Vector2(-half.x - 100f - pos.x, Random.Range(-half.y * 0.5f, half.y * 0.5f) - pos.y).normalized;
            if (Mathf.Approximately(minDist, distTop))
                return new Vector2(Random.Range(-half.x * 0.5f, half.x * 0.5f) - pos.x, half.y + 100f - pos.y).normalized;

            return new Vector2(Random.Range(-half.x * 0.5f, half.x * 0.5f) - pos.x, -half.y - 100f - pos.y).normalized;
        }

        Vector2 oppositeDir = -direction;
        float cone = miniGameManager.currentInsect._insectPicked.insectBlockingData.bounceConeAngle;

        for (int i = 0; i < 6; i++)
        {
            int edge = Random.Range(0, 4);
            Vector2 exitTarget = edge switch
            {
                0 => new Vector2(half.x + 100f, Random.Range(-half.y, half.y)),
                1 => new Vector2(-half.x - 100f, Random.Range(-half.y, half.y)),
                2 => new Vector2(Random.Range(-half.x, half.x), half.y + 100f),
                _ => new Vector2(Random.Range(-half.x, half.x), -half.y - 100f)
            };

            Vector2 candidateDir = (exitTarget - pos).normalized;
            if (Vector2.Angle(oppositeDir, candidateDir) <= cone * 1.5f)
                return candidateDir;
        }

        return RotateVector(oppositeDir, Random.Range(-cone, cone)).normalized;
    }

    private void UpdateRotation(bool instant = false)
    {
        Vector2 lookDir = isFleeing ? direction : currentMoveDirection;
        if (lookDir.sqrMagnitude < 0.0001f) return;

        var blockingData = miniGameManager.currentInsect._insectPicked.insectBlockingData;
        float targetAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + blockingData.rotationOffset;

        if (instant)
        {
            insectTransform.localEulerAngles = new Vector3(0f, 0f, targetAngle);
        }
        else
        {
            float currentAngle = insectTransform.localEulerAngles.z;
            float smoothAngle = Mathf.LerpAngle(currentAngle, targetAngle, blockingData.rotationSpeed * Time.fixedDeltaTime);
            insectTransform.localEulerAngles = new Vector3(0f, 0f, smoothAngle);
        }
    }

    private IEnumerator CheckForVictory()
    {
        float currentStamina = data.insectBlockingData.staminaInsect;

        while(currentStamina > 0)
        {
            currentStamina -= Time.deltaTime;
            yield return null;
        }

        caugth = true;
        yield return new WaitForSeconds(0.2f);
        miniGameManager.OnMiniGameSuccess();
    }

    private bool CheckLineInFront(float stepLength)
    {
        if (!TryCacheReferences() || !lineDrawer.HasActiveLine)
            return false;

        RectTransform lineRect = lineDrawer.rectTransform;
        Vector2 insectLocalPos = lineRect.InverseTransformPoint(insectTransform.position);

        Vector3 worldDir = insectTransform.parent.TransformDirection(currentMoveDirection);
        Vector2 localDir = ((Vector2)lineRect.InverseTransformDirection(worldDir)).normalized;

        float rayDist = miniGameManager.currentInsect._insectPicked.insectBlockingData.rayDistance;
        float checkDist = Mathf.Max(rayDist, stepLength * 1.5f);
        Vector2 rayEnd = insectLocalPos + localDir * checkDist;

        return SegmentsIntersect(insectLocalPos, rayEnd, lineDrawer.ActiveStart, lineDrawer.ActiveEnd);
    }

    private void BounceOffLine()
    {
        direction = GetExitDirection(preferClosestEdge: false);
        wavePhase = 0f;
        bounceCooldown = 0.2f;
    }

    private Vector2 RotateVector(Vector2 v, float degrees)
    {
        float rad = degrees * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);
        return new Vector2(v.x * cos - v.y * sin, v.x * sin + v.y * cos);
    }

    private bool SegmentsIntersect(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
    {
        float d = (p2.x - p1.x) * (p4.y - p3.y) - (p2.y - p1.y) * (p4.x - p3.x);
        if (Mathf.Abs(d) < 0.0001f) return false;

        float u = ((p3.x - p1.x) * (p4.y - p3.y) - (p3.y - p1.y) * (p4.x - p3.x)) / d;
        float v = ((p3.x - p1.x) * (p2.y - p1.y) - (p3.y - p1.y) * (p2.x - p1.x)) / d;

        return (u >= 0f && u <= 1f) && (v >= 0f && v <= 1f);
    }
}