using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace ArchGuard.Core;

[DebuggerDisplay("{FullName}")]
public sealed class FieldArtifact : Artifact, IEquatable<FieldArtifact>
{
    private readonly IFieldSymbol _fieldSymbol;

    public override TypeArtifact ContainingType { get; }

    public TypeArtifact Type { get; }

    public FieldArtifact(TypeArtifact type, IFieldSymbol symbol, ProjectSpecification project)
        : base(symbol, project)
    {
        ArgumentNullException.ThrowIfNull(symbol);
        ArgumentNullException.ThrowIfNull(project);

        _fieldSymbol = symbol;

        ContainingType = type;
        Type = new TypeArtifact(_fieldSymbol.Type, Project);
    }

    public bool Equals(FieldArtifact? other)
    {
        if (other is null)
            return false;

        return other.GetHashCode().Equals(GetHashCode());
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (obj is not FieldArtifact property)
            return false;

        return property.Equals(this);
    }

    public override int GetHashCode()
    {
        return FullName.GetHashCode(StringComparison.Ordinal);
    }
}
