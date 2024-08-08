using Cubium.SpatialControl;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cubium.Player
{
    public class PlayerCameraControls : MonoBehaviour
    {
        [Header("Zoom")]
        [SerializeField] private Arm m_arm = null;
        [SerializeField] private float m_distancePerZoom = 0.1f;
        
        [Header("Rotation")]
        [SerializeField] private float m_maxZoom = 5.0f;
        [SerializeField] private float m_pitchSpeed = 100.0f;
        [SerializeField] private Vector2 m_pitchLimits = new Vector2(20.0f, 60.0f);
        [SerializeField] private float m_yawSpeed = 100.0f;
        
        private bool m_bPitchLocked = true;

        public void Zoom(InputAction.CallbackContext context)
        {
            if (m_arm is null)
                return;

            float zoomInput = context.ReadValue<float>();
            float armChange = zoomInput * m_distancePerZoom;
            
            if (m_arm.ArmLength + armChange > m_maxZoom)
            {
                m_arm.SetArmLength(m_maxZoom);
                return;
            }
            
            m_arm.AddArmLength(armChange);
        }
        
        public void Rotate(InputAction.CallbackContext context)
        {
            if (m_arm is null || m_bPitchLocked)
                return;
            
            Vector2 rotationDirection = context.ReadValue<Vector2>();
            
            // Yaw
            float yawAngle = rotationDirection.x * m_yawSpeed * Time.deltaTime;
            m_arm.transform.Rotate(Vector3.up, yawAngle, Space.World);
            
            // Pitch
            float pitchAngle = rotationDirection.y * m_pitchSpeed * Time.deltaTime;
            // Check if the pitch is within the limits
            if (m_arm.transform.rotation.eulerAngles.x + pitchAngle < m_pitchLimits.x)
            {
                Vector3 newRotation = m_arm.transform.rotation.eulerAngles;
                newRotation.x = m_pitchLimits.x;
                m_arm.transform.rotation = Quaternion.Euler(newRotation);
                return;
            }
            if (m_arm.transform.rotation.eulerAngles.x + pitchAngle > m_pitchLimits.y)
            {
                Vector3 newRotation = m_arm.transform.rotation.eulerAngles;
                newRotation.x = m_pitchLimits.y;
                m_arm.transform.rotation = Quaternion.Euler(newRotation);
                return;
            }
            
            m_arm.transform.Rotate(Vector3.right, pitchAngle, Space.Self);
        }
        
        public void UnlockPitch(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                m_bPitchLocked = false;
            }
            if (context.canceled)
            {
                m_bPitchLocked = true;
            }
        }
    }
}
