using UnityEngine;

public class TerrariumManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject terrariumUI;
    [SerializeField] private GameObject leftUI;

    [SerializeField] private float minSwipeDistance = 50f;

    private Vector2 startSwipePosition;

    private void Update()
    {
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                startSwipePosition = touch.position;
            else if (touch.phase == TouchPhase.Ended)
                HandleSwipe(touch.position);
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
                startSwipePosition = Input.mousePosition;
            else if (Input.GetMouseButtonUp(0))
                HandleSwipe(Input.mousePosition);
        }
    }

    private void HandleSwipe(Vector2 endPosition)
    {
        float deltaX = endPosition.x - startSwipePosition.x;

        if (Mathf.Abs(deltaX) < minSwipeDistance) return;

        if (deltaX < 0)
        {
            terrariumUI.SetActive(true);
            leftUI.SetActive(false);
            Debug.Log("showTerra");
        }
        else
        {
            terrariumUI.SetActive(false);
            leftUI.SetActive(true);
            Debug.Log("Mask");
        }
    }
}