using UnityEngine;
using UnityEngine.InputSystem;

namespace Cubium.Player
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private float m_movementSpeed = 5.0f;
        
        private Vector3 m_moveDirection = Vector3.zero;
        
        public void Move(InputAction.CallbackContext context)
        {
            Vector2 moveInput = context.ReadValue<Vector2>();
            m_moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        }

        private void Update()
        {
            transform.Translate(m_moveDirection * (m_movementSpeed * Time.deltaTime), Space.World);
        }
    }
}