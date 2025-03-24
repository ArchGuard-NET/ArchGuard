using ArchGuard.Core;

namespace ArchGuard;

public sealed class TypesContext
{
    private readonly HashSet<TypeArtifact> _types;

    internal TypesContext(HashSet<TypeArtifact> types)
    {
        _types = types;
    }

    public IReadOnlySet<TypeArtifact> GetTypes()
    {
        return _types;
    }
}
