using Cubium.SpatialControl;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cubium.Player
{
    public class PlayerCameraControls : MonoBehaviour
    {
        [SerializeField] private Arm m_arm = null;
        [SerializeField] private float m_distancePerZoom = 0.1f;
        [SerializeField] private float m_maxZoom = 5.0f;
        [SerializeField] private float m_pitchSpeed = 10.0f;
        [SerializeField] private Vector2 m_pitchLimits = new Vector2(20.0f, 60.0f);
        
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
        
        public void Pitch(InputAction.CallbackContext context)
        {
            if (m_arm is null || m_bPitchLocked)
                return;
            
            float pitchInput = context.ReadValue<float>();
            float rotationAngle = pitchInput * m_pitchSpeed * Time.deltaTime;

            // Check if the rotation is within the limits
            if (m_arm.transform.rotation.eulerAngles.x + rotationAngle < m_pitchLimits.x)
            {
                m_arm.transform.rotation = Quaternion.Euler(m_pitchLimits.x, 0, 0);
                return;
            }
            if (m_arm.transform.rotation.eulerAngles.x + rotationAngle > m_pitchLimits.y)
            {
                m_arm.transform.rotation = Quaternion.Euler(m_pitchLimits.y, 0, 0);
                return;
            }
            
            m_arm.transform.Rotate(Vector3.right, rotationAngle);
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
