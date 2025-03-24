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
        while (containingNamespace is not null && !containingNamespace.IsGlobalNamespace)
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

        if (symbol is not INamedTypeSymbol namedTypeSymbol)
            return name;

        var containingType = namedTypeSymbol.ContainingType;
        while (containingType is not null)
        {
            name = $"{containingType.Name}+{name}";
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

        if (symbol is not INamedTypeSymbol)
            return name;

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
