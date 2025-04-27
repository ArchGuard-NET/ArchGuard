using Microsoft.CodeAnalysis;

namespace ArchGuard.Core;

public abstract class Artifact
{
    protected readonly ISymbol _symbol;

    internal ProjectSpecification Project { get; init; }

    public abstract TypeArtifact ContainingType { get; }
    public string Namespace => SymbolHelper.GetNamespace(_symbol);
    public string Name => SymbolHelper.GetName(_symbol);
    public string FullName => SymbolHelper.GetFullName(_symbol);

    protected Artifact(ISymbol symbol, ProjectSpecification project)
    {
        _symbol = symbol;

        Project = project;
    }
}
