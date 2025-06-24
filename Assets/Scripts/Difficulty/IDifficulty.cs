namespace Game.Difficulty
{
    public interface IDifficulty
    {
        string Name { get; }

        public int CalculateTotalEnemyAmount(int waveId);
    }
}