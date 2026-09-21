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

    private void Start()
    {
        _player = GameManager.instance.player;
        currentSpeed = _player.playerData.playerControllerData.walkSpeed;
    }


    void FixedUpdate()
    {
        if (GameManager.instance.M_UI.isUiActive) return;
        
        Movement();
        if (GameManager.instance.player._rb.linearVelocity.y > 0f)
        {
            GameManager.instance.player._rb.linearVelocity = new Vector3(GameManager.instance.player._rb.linearVelocity.x, 0f, GameManager.instance.player._rb.linearVelocity.z);
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
            GameManager.instance.player.isInteracted = true;
        }

        if (context.canceled)
        {
            GameManager.instance.player.isInteracted = false;
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

        currentSpeed = GameManager.instance.player.playerData.playerControllerData.walkSpeed;

        Vector3 velocity = isoDirection * currentSpeed;
        velocity.y = GameManager.instance.player._rb.linearVelocity.y;

        GameManager.instance.player._rb.linearVelocity = velocity;

        if (isoDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(isoDirection);

            GameManager.instance.player._rb.MoveRotation(Quaternion.Slerp(
                GameManager.instance.player._rb.rotation,
                targetRotation,
                  GameManager.instance.player.playerData.playerControllerData.rotationSpeed * Time.fixedDeltaTime));
        }


        bool isMoving = input.sqrMagnitude > 0.01f;

    }

}
