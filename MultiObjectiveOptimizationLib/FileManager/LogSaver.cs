using System;
using System.IO;

namespace MultiObjectiveOptimizationLib.FileManager
{
    public static class Log
    {
        private const string _fileName = "log.txt";

        public static void Save(Exception info)
        {
            string log = info.ToString() + "\n\n";
            File.AppendAllText(_fileName,log);
        }
    }
}