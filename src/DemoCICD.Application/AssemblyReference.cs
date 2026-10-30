using System.Reflection;

namespace DemoCICD.Application
{
    public static class AssemblyReference
    {
        // get all reference information of the current project
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
