using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace ArchGuard.Core;

[DebuggerDisplay("{FullName}")]
public sealed class PropertyArtifact : Artifact, IEquatable<PropertyArtifact>
{
    private readonly IPropertySymbol _propertySymbol;

    // TODO: add get & set methods

    public override TypeArtifact ContainingType { get; }

    public TypeArtifact Type { get; }

    public PropertyArtifact(TypeArtifact type, IPropertySymbol symbol, ProjectSpecification project)
        : base(symbol, project)
    {
        ArgumentNullException.ThrowIfNull(symbol);
        ArgumentNullException.ThrowIfNull(project);

        _propertySymbol = symbol;

        ContainingType = type;
        Type = new TypeArtifact(_propertySymbol.Type, Project);
    }

    public bool Equals(PropertyArtifact? other)
    {
        if (other is null)
            return false;

        return other.GetHashCode().Equals(GetHashCode());
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (obj is not PropertyArtifact property)
            return false;

        return property.Equals(this);
    }

    public override int GetHashCode()
    {
        return FullName.GetHashCode(StringComparison.Ordinal);
    }
}
