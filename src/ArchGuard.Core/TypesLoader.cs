using System.Collections.Concurrent;
using Microsoft.CodeAnalysis;

namespace ArchGuard.Core;

public sealed class TypesLoader
{
    private static readonly ConcurrentDictionary<ProjectSpec, SemaphoreSlim> _locks = new();
    private static readonly ConcurrentDictionary<ProjectSpec, HashSet<TypeArtifact>> _cache = new();

    public HashSet<TypeArtifact> GetTypes(ProjectSpec project)
    {
        if (_cache.TryGetValue(project, out var types))
            return types;

        var semaphore = _locks.GetOrAdd(project, _ => new SemaphoreSlim(1, 1));
        semaphore.Wait();

        try
        {
            if (_cache.TryGetValue(project, out var types2))
                return types2;

            var compilation = project.GetCompilation();
            var assembly = compilation.Assembly;
            var @namespace = compilation.GlobalNamespace;

            var typesG = GetTypes(project, @namespace, assembly);

            _cache.TryAdd(project, typesG);

            return typesG;
        }
        finally
        {
            semaphore.Release();
            _locks.TryRemove(project, out _);
        }
    }

    private HashSet<TypeArtifact> GetTypes(ProjectSpec project, INamespaceSymbol @namespace, IAssemblySymbol assembly)
    {
        var types = new HashSet<TypeArtifact>();
        foreach (
            var type in @namespace
                .GetTypeMembers()
                .Where(type =>
                    assembly is null || type.ContainingAssembly.Equals(assembly, SymbolEqualityComparer.Default)
                )
        )
        {
            types.Add(new TypeArtifact(type, project));
            types.UnionWith(GetAllTypeMembers(project, type, assembly!));
        }

        foreach (var nestedNamespace in @namespace.GetNamespaceMembers())
            types.UnionWith(GetTypes(project, nestedNamespace, assembly));

        return types;
    }

    private static HashSet<TypeArtifact> GetAllTypeMembers(
        ProjectSpec project,
        INamedTypeSymbol typeSymbol,
        IAssemblySymbol assemblySymbol
    )
    {
        var types = new HashSet<TypeArtifact>();
        foreach (
            var nestedType in typeSymbol
                .GetTypeMembers()
                .Where(nestedType =>
                    nestedType.ContainingAssembly.Equals(assemblySymbol, SymbolEqualityComparer.Default)
                )
        )
        {
            types.Add(new TypeArtifact(nestedType, project));
            types.UnionWith(GetAllTypeMembers(project, nestedType, assemblySymbol));
        }

        return types;
    }

    internal TypeArtifact? GetParentType(TypeArtifact type, INamedTypeSymbol? parentNamedTypeSymbol)
    {
        if (parentNamedTypeSymbol is null)
            return null;

        var types = GetTypes(type.Project);
        return types.FirstOrDefault(t =>
            t.FullName.Equals(SymbolHelper.GetFullName(parentNamedTypeSymbol), StringComparison.Ordinal)
        );
    }
}
