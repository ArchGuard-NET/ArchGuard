using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace ArchGuard.Core;

[DebuggerDisplay("{FullName}")]
public sealed class TypeArtifact : Artifact, IEquatable<TypeArtifact>
{
    private readonly ITypeSymbol _typeSymbol;

    public TypeArtifact? ParentType => new TypesLoader().GetParentType(this, _symbol.ContainingType);
    public override TypeArtifact ContainingType => this;

    public IEnumerable<ConstructorArtifact> GetConstructors()
    {
        return _typeSymbol
            .GetMembers()
            .OfType<IMethodSymbol>()
            .Where(method => method.MethodKind is MethodKind.Constructor)
            .Select(constructor => new ConstructorArtifact(this, constructor, Project));
    }

    public IEnumerable<PropertyArtifact> GetProperties()
    {
        return _typeSymbol
            .GetMembers()
            .OfType<IPropertySymbol>()
            .Select(symbol => new PropertyArtifact(this, symbol, Project));
    }

    public IEnumerable<MethodArtifact> GetMethods()
    {
        return _typeSymbol
            .GetMembers()
            .OfType<IMethodSymbol>()
            .Where(method =>
                // avoid property { get; set; }
                method.MethodKind
                    is MethodKind.Ordinary
                        or MethodKind.PropertyGet
                        or MethodKind.PropertySet
                // avoid ctor method
                && !method.IsImplicitlyDeclared
            )
            .Select(symbol => new MethodArtifact(this, symbol, Project));
    }

    public IEnumerable<FieldArtifact> GetFields()
    {
        return _typeSymbol.GetMembers().OfType<IFieldSymbol>().Select(field => new FieldArtifact(this, field, Project));
    }

    public TypeArtifact(ITypeSymbol symbol, ProjectSpecification project)
        : base(symbol, project)
    {
        _typeSymbol = (ITypeSymbol)_symbol;
    }

    public bool Equals(TypeArtifact? other)
    {
        if (other is null)
            return false;

        return other.GetHashCode().Equals(GetHashCode());
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
