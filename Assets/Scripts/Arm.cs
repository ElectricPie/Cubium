using UnityEngine;

namespace Cubium
{
    public class Arm : MonoBehaviour
    {
        [SerializeField] [Min(0)] private float m_armLength = 2.0f;

        public void SetArmLength(float newLength)
        {
            m_armLength = newLength;
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