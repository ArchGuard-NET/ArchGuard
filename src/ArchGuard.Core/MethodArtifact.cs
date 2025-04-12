using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace ArchGuard.Core;

[DebuggerDisplay("{FullName}")]
public sealed class MethodArtifact : Artifact, IEquatable<MethodArtifact>
{
    private readonly IMethodSymbol _methodSymbol;

    public override TypeArtifact ContainingType { get; }

    public TypeArtifact ReturnType { get; }

    public MethodArtifact(TypeArtifact type, IMethodSymbol symbol, ProjectSpecification project)
        : base(symbol, project)
    {
        ArgumentNullException.ThrowIfNull(symbol);
        ArgumentNullException.ThrowIfNull(project);

        _methodSymbol = symbol;

        ContainingType = type;
        ReturnType = new TypeArtifact(_methodSymbol.ReturnType, Project);
    }

    public bool Equals(MethodArtifact? other)
    {
        if (other is null)
            return false;

        return other.GetHashCode().Equals(GetHashCode());
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (obj is not MethodArtifact property)
            return false;

        return property.Equals(this);
    }

    public override int GetHashCode()
    {
        return FullName.GetHashCode(StringComparison.Ordinal);
    }
}
