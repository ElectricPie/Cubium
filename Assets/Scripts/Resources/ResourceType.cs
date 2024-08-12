using UnityEngine;

namespace Cubium.Resources
{
    [CreateAssetMenu(fileName = "New Resource", menuName = "Cubium/New Resource", order = 0)]
    public class ResourceType : ScriptableObject
    {
        [SerializeField] private string Name;
        [SerializeField] public Sprite Icon;

        public string GetName()
        {
            return Name;
        }

        public Sprite GetSprite()
        {
            return Icon;
        }
    }
}