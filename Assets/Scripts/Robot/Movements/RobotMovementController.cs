using UnityEngine;
using UnityEngine.InputSystem;

public class RobotMovementController : MonoBehaviour
{

    public void OnRobotMove(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();

        if(context.performed)
        {
            transform.position = new Vector3(transform.position.x + direction.x, transform.position.y, transform.position.z + direction.y);
        }
    }
}
