using System.Collections.Generic;

namespace GamesSimulator.DTOs
{
    public class GameSimulatorConfigDto
    {
        public int NumberOfSimulatorIterations { get; set; }
        public List<string> ExcludedGames { get; set; }
        public List<string> GamesWithoutLineParameter { get; set; }
        public List<string> GamesWithCascadeWin { get; set; }
        public Dictionary<string, int> MissingGamesNumOfLines {  get; set; }
    }
}
