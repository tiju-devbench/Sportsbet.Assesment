

namespace Domain.Entities
{
    public class NflDepthChart : DepthChart
    {
        public override void Initialize()
        {
            GameType = "NFL";
            Chart.Add("QB", new List<int>());
            Chart.Add("WR", new List<int>());
            Chart.Add("RB", new List<int>());
            Chart.Add("TE", new List<int>());
            Chart.Add("K", new List<int>());
            Chart.Add("P", new List<int>());
            Chart.Add("KR", new List<int>());
            Chart.Add("PR", new List<int>());
        }
    }
}
