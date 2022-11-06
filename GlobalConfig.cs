using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitiveCoders.com_Considition2022
{
    public class GlobalConfig
    {
        public static bool LogApiResponsesToFile = true;

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
