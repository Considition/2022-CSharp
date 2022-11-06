using CompetitiveCoders.com_Considition2022.models;

namespace CompetitiveCoders.com_Considition2022.responses
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