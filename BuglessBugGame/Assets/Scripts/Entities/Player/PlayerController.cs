using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    private Player _player;

    [Header("Parameters")]
    private float currentSpeed;
    private Vector2 _moveInput;
    private PlayerInput playerInput;

    private void Awake()
    {
        _player = Player.Instance;
        currentSpeed = _player.playerData.playerControllerData.walkSpeed;
    }


    void FixedUpdate()
    {
        if (GameManager.instance.M_UI.isUiActive) return;
        
        Movement();
        if (Player.Instance._rb.linearVelocity.y > 0f)
        {
            Player.Instance._rb.linearVelocity = new Vector3(Player.Instance._rb.linearVelocity.x, 0f, Player.Instance._rb.linearVelocity.z);
        }
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _moveInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            _moveInput = Vector2.zero;
        }
    }
    
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Interact");
            Player.Instance.isInteracted = true;
        }

        if (context.canceled)
        {
            Player.Instance.isInteracted = false;
        }
    }

    private void Movement() 
    {
        Vector2 input = Vector2.ClampMagnitude(_moveInput, 1f);
        Vector3 isoDirectionRaw = new Vector3(
            input.y + input.x,
            0f,
            input.y - input.x
        );

        Vector3 isoDirection = Vector3.ClampMagnitude(isoDirectionRaw, 1f);

        currentSpeed = Player.Instance.playerData.playerControllerData.walkSpeed;

        Vector3 velocity = isoDirection * currentSpeed;
        velocity.y = Player.Instance._rb.linearVelocity.y;

        Player.Instance._rb.linearVelocity = velocity;

        if (isoDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(isoDirection);

            Player.Instance._rb.MoveRotation(Quaternion.Slerp(
                Player.Instance._rb.rotation,
                targetRotation,
                  Player.Instance.playerData.playerControllerData.rotationSpeed * Time.fixedDeltaTime));
        }


        bool isMoving = input.sqrMagnitude > 0.01f;

    }

}
