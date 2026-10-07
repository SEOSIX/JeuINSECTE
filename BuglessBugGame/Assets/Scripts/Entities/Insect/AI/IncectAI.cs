using UnityEngine;

public class IncectAI : MonoBehaviour
{

    [SerializeField] private RectTransform insectTransform;
    
    public enum AiState
    {
        Idle,
        InMovements,
        Hide,
        Catched
    }
    private InsectData data;
    
    private AiState aiState;
    
    private float currentVelocity;
    private float directionChangeTimer;
    private bool isHidden;
    private RectTransform canvasRectTransform;
    private Vector2 direction;
    private Vector2 screenLimit;
    private Vector3 lastPosition;
    private float intervalPickChance = 1.5f;

    void OnEnable()
    {
        Init();
    }
    
    void FixedUpdate()
    {
        InsectMovements();
    }

    void Init()
    {
        data = GameManager.instance.M_MiniGameManager.currentInsect.bug;
        canvasRectTransform = GetComponent<RectTransform>();

        direction = GetRandomDirection();
        
        directionChangeTimer = intervalPickChance;
        lastPosition = insectTransform.position;
    }

    private void InsectMovements()
    {
        directionChangeTimer -= Time.fixedDeltaTime;
        if (directionChangeTimer <= 0f)
        {
            directionChangeTimer = intervalPickChance;
            if (Random.value < data.changeDirectionPercentage)
            {
                direction = GetRandomDirection();
            }
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

    private bool CheckPosition()
    {
        return false;
    }
}
