using _Core.Scripts.Items;
using UnityEngine;

namespace Blocks.Core
{
    public class BaseBlock : UnityEngine.MonoBehaviour
    {
        [SerializeField] protected float health;
        [SerializeField] protected ItemSO rewardItem;
        [SerializeField] protected int rewardItemCount;
        
    }
}