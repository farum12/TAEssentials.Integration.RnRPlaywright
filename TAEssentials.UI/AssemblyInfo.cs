// Defines level of parallelism for all tests in this framework
[assembly: Parallelizable(ParallelScope.Fixtures)]

// Defines list of exceptions - Features which won't be run in parallel
namespace TAEssentials.UI
{
    //[NUnit.Framework.Parallelizable(NUnit.Framework.ParallelScope.None)]
    //public partial class ClassNameFromSpecFile
}