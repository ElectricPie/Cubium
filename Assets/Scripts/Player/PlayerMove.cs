using UnityEngine;
using UnityEngine.InputSystem;

namespace Cubium.Player
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private float m_movementSpeed = 10.0f;
        [SerializeField] private Bounds m_bounds = new Bounds(Vector3.zero, new Vector3(100.0f, 20.0f, 100.0f));
        
        private Vector3 m_moveDirection = Vector3.zero;
        
        public void Move(InputAction.CallbackContext context)
        {
            Vector2 moveInput = context.ReadValue<Vector2>();
            m_moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        }

        private void Update()
        {
            Vector3 newPosition = transform.position + m_moveDirection * (m_movementSpeed * Time.deltaTime);

            if (m_bounds.Contains(newPosition))
            {
                transform.position = newPosition;
            }
            else
            {
                transform.position = m_bounds.ClosestPoint(newPosition);
            }
        }
    }
}