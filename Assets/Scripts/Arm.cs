using UnityEngine;

namespace Cubium
{
    public class Arm : MonoBehaviour
    {
        [SerializeField] [Min(0)] private float m_armLength = 2.0f;

        public float ArmLength => m_armLength;
        
        public void SetArmLength(float newLength)
        {
            m_armLength = newLength;
        }
        
        public void AddArmLength(float amount)
        {
            if (m_armLength + amount < 0)
            {
                m_armLength = 0;
            }
            else
            {
                m_armLength += amount;
            }
            
            UpdateChildPositions();
        }
        
        
        private void OnValidate()
        {
            UpdateChildPositions();
        }
        
        private void UpdateChildPositions()
        {
            // Set the distance of the arm to the specified value
            foreach (Transform child in transform)
            {
                child.localPosition = new Vector3(0, 0, -m_armLength);
            }
        }
    }
}