namespace Infrastructure.Data
{
    [System.Serializable]
    public sealed class EnemyCreationData
    {
        public GameCore.CommonLogic.EnemyTypeId Type;
        public int Count;
    }
}
