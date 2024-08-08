using UnityEngine;
using UnityEngine.InputSystem;

namespace Cubium.Player
{
    public class PlayerCameraZoom : MonoBehaviour
    {
        [SerializeField] private Arm m_arm = null;
        [SerializeField] private float m_distancePerZoom = 0.1f;
        [SerializeField] private float m_maxZoom = 5.0f;
        
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
    }
}
