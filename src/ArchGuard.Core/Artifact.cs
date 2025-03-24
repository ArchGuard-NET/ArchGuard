using Microsoft.CodeAnalysis;

namespace ArchGuard.Core;

public abstract class Artifact
{
    protected readonly ISymbol _symbol;

    public abstract TypeArtifact Type { get; }
    public string Namespace => SymbolHelper.GetNamespace(_symbol);
    public string Name => SymbolHelper.GetName(_symbol);
    public string FullName => SymbolHelper.GetFullName(_symbol);

    protected Artifact(ISymbol symbol)
    {
        _symbol = symbol;
    }
}
