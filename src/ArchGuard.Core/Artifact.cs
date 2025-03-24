using Microsoft.CodeAnalysis;

namespace ArchGuard.Core;

public abstract class Artifact
{
    protected readonly ISymbol _symbol;

    internal ProjectSpec Project { get; init; }

    public abstract TypeArtifact Type { get; }
    public string Namespace => SymbolHelper.GetNamespace(_symbol);
    public string Name => SymbolHelper.GetName(_symbol);
    public string FullName => SymbolHelper.GetFullName(_symbol);

    protected Artifact(ISymbol symbol, ProjectSpec project)
    {
        _symbol = symbol;

        Project = project;
    }
}
