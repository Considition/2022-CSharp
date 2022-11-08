using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompetitiveCoders.com_Considition2022.models;
using CompetitiveCoders.com_Considition2022.responses;

namespace CompetitiveCoders.com_Considition2022
{
    public class GlobalConfig
    {
        public static bool LogApiResponsesToFile = true;


        // yeah.. .sorry...
        public static string CurrentMap { get; set; }
        public static int BagType {get; set;}
        public static string ResultsCsvFilename => $"data\\results-{GlobalConfig.CurrentMap}-bag-{GlobalConfig.BagType}.csv";


        public static Dictionary<string, SubmitResponse> ScoringMemo = new();


        public static Dictionary<string, GameResponse> GameResponseMemo = new();

        //sorry
        public static void LogToFile(string fileName, string content)
        {
            if (LogApiResponsesToFile)
            {
                Directory.CreateDirectory("data");
                File.WriteAllText($"Data/{fileName}", content);
            }
        }

        public static object api_call_lock = new object();

    }
}
