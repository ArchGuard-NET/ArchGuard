using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace ArchGuard.Core;

public sealed class TypeArtifact : Artifact, IEquatable<TypeArtifact>
{
    public IImmutableList<TypeArtifact> ParentTypes { get; }
    public override TypeArtifact Type => this;

    public TypeArtifact(INamedTypeSymbol symbol)
        : base(symbol) { }

    public bool Equals(TypeArtifact? other)
    {
        if (other is null)
            return false;

        return other.FullName.Equals(FullName, StringComparison.Ordinal);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (obj is not TypeArtifact type)
            return false;

        return type.Equals(this);
    }

    public override int GetHashCode()
    {
        return new { FullName }.GetHashCode();
    }
}
