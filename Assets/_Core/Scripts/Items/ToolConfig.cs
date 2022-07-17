using UnityEngine;

namespace _Core.Scripts.Items
{
    public abstract class ToolConfig : ScriptableObject
    {
        [SerializeField] private GameObject toolPrefab;
        
    }
}