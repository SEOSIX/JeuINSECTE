using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float smoothTime = 0.3f;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    private Vector3 _velocity;

    private Vector3 PlayerPos => Player.Instance.transform.position;

    void Start()
    {
        if (target)
            transform.position = TargetToCam(target.position);
        else
        {
            target = Player.Instance.transform;
        }
    }

    void LateUpdate()
    {
        if (!target) return;

        Vector3 desiredPosition = TargetToCam(target.position);

        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref _velocity,
            smoothTime
        );
    }

    public float GetPlayerToTargetLerp(Vector3 target)
    {
        return InverseLerp(PlayerPos, TargetToCam(target), transform.position);
    }

    private static float InverseLerp(Vector3 a, Vector3 b, Vector3 value)
    {
        if (Vector3.Distance(a, b) == 0) return 1;
        return Vector3.Distance(a, value) / Vector3.Distance(a, b);
    }

    private Vector3 TargetToCam(Vector3 target)
    {
        return target + offset;
    }

    [ContextMenu("Recenter")]
    public void Recenter()
    {
        if (!target) return;
        transform.position = target.position + offset;
        _velocity = Vector3.zero; 
    }
}