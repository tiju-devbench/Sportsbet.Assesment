
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MlbDepthChart : DepthChart
    {
        public override void Initialize()
        {
            GameType = "MLB";
            Chart.Add("SP", new List<int>());
            Chart.Add("RP", new List<int>());
            Chart.Add("C", new List<int>());
            Chart.Add("1B", new List<int>());
            Chart.Add("2B", new List<int>());
            Chart.Add("3B", new List<int>());
            Chart.Add("SS", new List<int>());
            Chart.Add("LF", new List<int>());
            Chart.Add("RF", new List<int>());
            Chart.Add("CF", new List<int>());
            Chart.Add("DH", new List<int>());
        }
    }
}
