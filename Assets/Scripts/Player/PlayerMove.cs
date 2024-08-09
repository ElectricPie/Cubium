using UnityEngine;
using UnityEngine.InputSystem;

namespace Cubium.Player
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private float m_movementSpeed = 10.0f;
        [SerializeField] private Bounds m_bounds = new Bounds(Vector3.zero, new Vector3(100.0f, 20.0f, 100.0f));
        [SerializeField] private Transform m_cameraTransform = null;
        
        private Vector3 m_moveDirection = Vector3.zero;
        
        public void Move(InputAction.CallbackContext context)
        {
            Vector2 moveInput = context.ReadValue<Vector2>();
            m_moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            if (m_cameraTransform is null)
                return;

            Vector3 cameraMoveDirection = m_cameraTransform.forward * m_moveDirection.z;
            cameraMoveDirection += m_cameraTransform.right * m_moveDirection.x;
            cameraMoveDirection.y = 0;
            cameraMoveDirection.Normalize();

            Vector3 newPosition = transform.position + cameraMoveDirection * (m_movementSpeed * Time.deltaTime);

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