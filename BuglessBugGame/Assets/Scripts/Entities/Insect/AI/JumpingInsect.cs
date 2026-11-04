using UnityEngine;

public class JumpingInsect : InsectBase
{
    public enum JumpingState
    {
        Jumping,
        InMovements
    }

    public JumpingState jumpingState;

    [Header("Jump values")]
    [SerializeField] private float jumpHeight = 300f;
    [SerializeField] private float jumpDuration = 0.6f;
    [SerializeField] private float horizontalFactor = 1f;
    [SerializeField] private Vector2 waitRange = new Vector2(0.4f, 1.2f);
    [SerializeField] private float jumpHeightVariation = 0.25f;

    [Header("Random Floor")]
    [SerializeField] private Vector2 groundDivisorRange = new Vector2(1.5f, 4f);

    [Header("Random wall bounce")]
    [SerializeField] private Vector2Int wallBounceJumps = new Vector2Int(3, 4);

    [Header("Visual")]
    [SerializeField] private float maxTilt = 30f;

    private float waitTimer;
    private float jumpTimer;
    private float currentHeight;
    private int facing = 1;
    private int lockedJumpsRemaining;

    private float startGroundY;
    private float targetGroundY; 

    protected override void Init()
    {
        base.Init();
        jumpingState = JumpingState.InMovements;
        facing = direction.x >= 0f ? 1 : -1;
        lockedJumpsRemaining = 0;
        waitTimer = Random.Range(waitRange.x, waitRange.y);

        startGroundY = PickRandomGroundY();
        targetGroundY = startGroundY;

        Vector2 pos = insectTransform.anchoredPosition;
        pos.y = startGroundY;
        insectTransform.anchoredPosition = pos;
        ApplyVisual(0f);
    }

    private void Update()
    {
        InsectMovements();
    }

    public override void InsectMovements()
    {
        switch (jumpingState)
        {
            case JumpingState.InMovements:
                WaitOnGround();
                break;
            case JumpingState.Jumping:
                UpdateJump();
                break;
        }
    }

    private void WaitOnGround()
    {
        waitTimer -= Time.deltaTime;
        if (waitTimer <= 0f)
            StartJump();
    }

    private void StartJump()
    {
        jumpingState = JumpingState.Jumping;
        jumpTimer = 0f;

        if (lockedJumpsRemaining > 0)
            lockedJumpsRemaining--;

        // Le sol d'arrivée est tiré UNE SEULE FOIS par saut
        startGroundY = targetGroundY;
        targetGroundY = PickRandomGroundY();

        float ceiling = canvasRectTransform.rect.size.y * 0.5f - insectTransform.rect.size.y * 0.5f;
        float maxAllowed = ceiling - Mathf.Max(startGroundY, targetGroundY);
        float wanted = jumpHeight * (1f + Random.Range(-jumpHeightVariation, jumpHeightVariation));
        currentHeight = Mathf.Max(0f, Mathf.Min(wanted, maxAllowed));
    }

    private void UpdateJump()
    {
        jumpTimer += Time.deltaTime;
        float t = Mathf.Clamp01(jumpTimer / jumpDuration);

        Vector2 pos = insectTransform.anchoredPosition;
        pos.x += facing * data.bugSpeed * horizontalFactor * Time.deltaTime;

        Vector2 halfCanvas = canvasRectTransform.rect.size * 0.5f;
        Vector2 halfInsect = insectTransform.rect.size * 0.5f;
        float minX = -halfCanvas.x + halfInsect.x;
        float maxX = halfCanvas.x - halfInsect.x;

        if (pos.x <= minX)
        {
            pos.x = minX;
            Turn(1);
        }
        else if (pos.x >= maxX)
        {
            pos.x = maxX;
            Turn(-1);
        }

        // Le "sol" glisse du départ vers l'arrivée, l'arc symétrique s'ajoute par-dessus
        float baseY = Mathf.Lerp(startGroundY, targetGroundY, t);
        pos.y = baseY + currentHeight * 4f * t * (1f - t);

        if (t >= 1f)
        {
            pos.y = targetGroundY;
            insectTransform.anchoredPosition = pos;
            Land();
            return;
        }

        insectTransform.anchoredPosition = pos;
        ApplyVisual(t);
    }

    private void Turn(int newFacing)
    {
        facing = newFacing;
        direction.x = facing;
        lockedJumpsRemaining = Random.Range(wallBounceJumps.x, wallBounceJumps.y + 1);
    }

    private void Land()
    {
        jumpingState = JumpingState.InMovements;
        waitTimer = Random.Range(waitRange.x, waitRange.y);

        if (lockedJumpsRemaining == 0 && Random.value > 0.5f)
        {
            facing = -facing;
            direction.x = facing;
        }

        ApplyVisual(0f);
    }

    private float PickRandomGroundY()
    {
        float halfCanvasY = canvasRectTransform.rect.size.y * 0.5f;
        float halfInsectY = insectTransform.rect.size.y * 0.5f;
        float divisor = Random.Range(groundDivisorRange.x, groundDivisorRange.y);
        return (-halfCanvasY / divisor) + halfInsectY;
    }

    private void ApplyVisual(float t)
    {
        Vector3 scale = insectTransform.localScale;
        scale.x = Mathf.Abs(scale.x) * facing;
        insectTransform.localScale = scale;

        float tilt = 0f;
        if (jumpingState == JumpingState.Jumping)
            tilt = (1f - 2f * t) * maxTilt;

        insectTransform.localRotation = Quaternion.Euler(0f, 0f, tilt * facing);
    }
}