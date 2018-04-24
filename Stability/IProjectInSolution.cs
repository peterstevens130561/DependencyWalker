namespace BHI.ArchitectureTools.StabilityCalculator
{
    public interface IProjectInSolution
    {
        string AssemblyName { get; }
        string ProjectFile { get; }
    }
}