using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] protected RectTransform obstacleTransform;
    
    protected InsectData data;
    protected MiniGameManager miniGameManager;
    protected RectTransform canvasRectTransform;
    protected Vector2 position;

    protected virtual void OnEnable()
    {
        Init();
    }
    
    protected virtual void Init()
    {
        miniGameManager = GameManager.instance.M_MiniGameManager;
        data = miniGameManager.currentInsect._insectPicked;
        canvasRectTransform = GetComponent<RectTransform>();
      //  position = GetRandomPosition();
    }
    
    public void LimitToScreen()
    {
        Vector2 half = canvasRectTransform.rect.size * 0.5f;
        Vector2 halfInsect = obstacleTransform.rect.size * 0.5f;
        Vector2 min = -half + halfInsect;
        Vector2 max = half - halfInsect;
        
        Vector2 pos = obstacleTransform.anchoredPosition;
        if (pos.x <= min.x)
        {
            pos.x = min.x;
            position.x = Mathf.Abs(position.x);
        }
        else if (pos.x >= max.x)
        {
            pos.x = max.x;
            position.x = -Mathf.Abs(position.x);
        }
        if (pos.y <= min.y)
        {
            pos.y = min.y;
            position.y = Mathf.Abs(position.y);
        }
        else if (pos.y >= max.y)
        {
            pos.y = max.y;
            position.y = -Mathf.Abs(position.y);
        }

      //  protected Vector2 GetRandomPosition();
        {
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
      //      return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        }
        
        position.Normalize();
        obstacleTransform.anchoredPosition = pos;
        
    }
}
