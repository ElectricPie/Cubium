using UnityEngine;

namespace Cubium.Resources
{
    public class GatherableResource : MonoBehaviour
    {
        [SerializeField]
        private ResourceType m_resourceType = null;
        
        [SerializeField]
        private int m_gatherableAmount = 20;

        public int Gather(int amount)
        {
            if (m_gatherableAmount - amount < 0)
            {
                Destroy(gameObject);
                return m_gatherableAmount;
            }
            
            m_gatherableAmount -= amount;
            return amount;
        }
    }
}