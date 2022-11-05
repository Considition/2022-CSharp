using DotNet.models;

namespace DotNet
{
    public class SubmitResponse
    {
        public int score;
        public string gameId;
        public bool valid;
        public System.Collections.Generic.List<WeekModel> weeks;
        public System.Collections.Generic.List<WeekModel> dailys;
        public int totalProducedBags;
        public int totalDestroyedBags;
        public string visualizer;
    }
}