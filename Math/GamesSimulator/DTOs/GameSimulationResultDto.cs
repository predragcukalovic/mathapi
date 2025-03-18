namespace GamesSimulator.DTOs
{
    public class GameSimulationResultDto
    {
        public string GameName { get; set; }
        public double Rtp { get; set; }
        public double HitFrequency { get; set; }
        public double StandardDeviation { get; set; }
        public int MaxGratisWin { get; set; }
        public double MaxExposure { get; set; }
    }
}
