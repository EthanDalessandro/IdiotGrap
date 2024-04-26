using UnityEngine;
using UnityEngine.InputSystem;

public class RobotMovementController : MonoBehaviour
{
    private Vector2 direction;
    private Rigidbody rb;

    private void Start()
    {
        //VIVE KAARIS
        rb = GetComponent<Rigidbody>();
    }

    [Range(1, 20)] public float moveSpeed;

    private void Update()
    {
        rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.y * moveSpeed);

    }

    public void OnRobotMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            direction = context.ReadValue<Vector2>();
        }
        else
        {
            direction = Vector2.zero;
        }
    }
}
