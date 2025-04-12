using Microsoft.CodeAnalysis;

namespace ArchGuard.Core;

internal static class SymbolHelper
{
    internal static string GetNamespace(ISymbol symbol)
    {
        ArgumentNullException.ThrowIfNull(symbol);

        var @namespace = symbol.ContainingNamespace;
        var fullName = @namespace.Name;

        var containingNamespace = @namespace.ContainingNamespace;
        while (containingNamespace?.IsGlobalNamespace == false)
        {
            fullName = $"{containingNamespace.Name}.{fullName}";
            containingNamespace = containingNamespace.ContainingNamespace;
        }

        return fullName;
    }

    internal static string GetName(ISymbol symbol)
    {
        ArgumentNullException.ThrowIfNull(symbol);

        var name = symbol.MetadataName;
        if (symbol is IMethodSymbol methodSymbol)
        {
            if (name.Equals(".ctor", StringComparison.OrdinalIgnoreCase))
                name = "_ctor";

            name += "(";
            foreach (var (index, parameter) in methodSymbol.Parameters.Index())
            {
                name += GetName(parameter.Type);
                if (index != methodSymbol.Parameters.Length - 1)
                    name += ", ";
            }
            name += ")";
        }

        var first = true;
        var containingType = symbol.ContainingType;
        while (containingType is not null)
        {
            var concat = first && symbol is not INamedTypeSymbol ? '.' : '+';
            first = false;

            name = $"{containingType.Name}{concat}{name}";
            containingType = containingType.ContainingType;
        }

        if (name.EndsWith('?'))
            name = name[..^1];

        return name;
    }

    internal static string GetFullName(ISymbol symbol)
    {
        ArgumentNullException.ThrowIfNull(symbol);

        var name = GetName(symbol);

        return GetNamespace(symbol) + "." + name;
    }

    internal static bool IsPublic(ISymbol symbol)
    {
        ArgumentNullException.ThrowIfNull(symbol);

        if (symbol is INamedTypeSymbol)
            throw new ArgumentException($"{symbol} must not be {nameof(INamedTypeSymbol)}.");

        return symbol.DeclaredAccessibility is Accessibility.Public;
    }

    public static bool IsInternal(ISymbol symbol)
    {
        ArgumentNullException.ThrowIfNull(symbol);

        if (symbol is INamedTypeSymbol)
            throw new ArgumentException($"{symbol} must not be {nameof(INamedTypeSymbol)}.");

        return symbol.DeclaredAccessibility
            is Accessibility.Internal
                or Accessibility.Friend
                or Accessibility.ProtectedOrFriend
                or Accessibility.ProtectedOrInternal;
    }

    public static bool IsProtected(ISymbol symbol)
    {
        ArgumentNullException.ThrowIfNull(symbol);

        return symbol.DeclaredAccessibility
            is Accessibility.Protected
                or Accessibility.ProtectedOrInternal
                or Accessibility.ProtectedOrFriend
                or Accessibility.ProtectedAndInternal
                or Accessibility.ProtectedAndFriend;
    }

    public static bool IsPrivate(ISymbol symbol)
    {
        ArgumentNullException.ThrowIfNull(symbol);

        return symbol.DeclaredAccessibility is Accessibility.Private;
    }

    public static bool IsPrivateOrProtected(ISymbol symbol)
    {
        ArgumentNullException.ThrowIfNull(symbol);

        return symbol.DeclaredAccessibility is Accessibility.Private or Accessibility.Protected;
    }
}
