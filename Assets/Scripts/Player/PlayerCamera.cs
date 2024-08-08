using UnityEngine;
using UnityEngine.InputSystem;

namespace Cubium.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        public void Move(InputAction.CallbackContext context)
        {
            Vector2 move = context.ReadValue<Vector2>();
            Debug.Log($"Move: {move}");
        }
    }
}