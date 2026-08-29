using System.Reflection;

namespace Eqiblox.ApplicationContext;

public interface IApplicationContext
{
    string? Name { get; }
    Assembly EntryAssembly { get; }
}