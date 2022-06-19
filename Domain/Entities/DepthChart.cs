
namespace Domain.Entities
{
    public abstract  class DepthChart
    {
        private readonly Dictionary<string, List<int>> _depthChart = new Dictionary<string, List<int>>();
        
        public DepthChart()
        {
            this.Initialize();
        }
        public string? GameType { get; set; }
        public Dictionary<string, List<int>> Chart { get { return _depthChart; } }

        public abstract void Initialize();
 

    }
}
