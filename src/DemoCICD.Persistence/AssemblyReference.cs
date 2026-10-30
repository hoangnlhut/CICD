using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DemoCICD.Persistence
{
    public static class AssemblyReference
    {
        // get all reference information of the current project
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
