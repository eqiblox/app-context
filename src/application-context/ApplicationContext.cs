using System.Reflection;

namespace Eqiblox.ApplicationContext;

public sealed class ApplicationContext : IApplicationContext
{
    public static ApplicationContext Singleton { get; } = new();

    private Assembly? _entryAssembly;

    public Assembly? EntryAssembly
    {
        get => _entryAssembly;
        set
        {
            _entryAssembly = value;
            Name = GetDllName(value);
        }
    }

    public string? Name { get; private set; }

    private ApplicationContext() => EntryAssembly = Assembly.GetEntryAssembly();

    public static void SetEntryClass(Type entryClassType)
    {
        ArgumentNullException.ThrowIfNull(entryClassType);

        Singleton.EntryAssembly = entryClassType.Assembly;
    }

    private static string? GetDllName(Assembly? assembly)
    {
        var location = assembly?.Location;

        if (!string.IsNullOrEmpty(location)) return Path.GetFileNameWithoutExtension(location);

        return Environment.GetEnvironmentVariable("AppName") ?? assembly?.GetName().Name;
    }
}