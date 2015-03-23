namespace MultiObjectiveOptimizationLib.FileManager
{
    public interface IFileLoader<T>
    {
        T LoadFromFile(string fileName);
    }
}