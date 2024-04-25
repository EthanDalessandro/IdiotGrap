using UnityEngine;
using UnityEngine.InputSystem;

public class RobotMovementController : MonoBehaviour
{
    private Vector2 direction;

    [Range(1, 20)] public float moveSpeed;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x + direction.x * moveSpeed * Time.deltaTime,
                                         transform.position.y,
                                         transform.position.z + direction.y * moveSpeed * Time.deltaTime);

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
