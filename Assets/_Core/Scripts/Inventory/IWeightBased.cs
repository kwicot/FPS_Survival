namespace _Core.Scripts.InventorySystem
{
    public interface IWeightBased
    {
        public float MaxWeight { get; }
        public float Weight { get; }
        public float FreeWeight { get; }
    }
}