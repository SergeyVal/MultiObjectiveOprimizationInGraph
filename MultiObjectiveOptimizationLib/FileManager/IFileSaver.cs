namespace MultiObjectiveOptimizationLib.FileManager
{
    public interface IFileSaver<T>
    {
        void SaveToFile(string fileName, T info);
    }
}