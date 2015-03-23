using System.IO;

namespace MultiObjectiveOptimizationLib.FileManager
{
    public class StringSaver<T> : IFileSaver<T>
    {
        public void SaveToFile(string fileName, T info)
        {
            File.AppendAllText(fileName, info.ToString());
        }
    }
}