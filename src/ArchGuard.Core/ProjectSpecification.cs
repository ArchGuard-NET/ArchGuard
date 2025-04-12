using Microsoft.CodeAnalysis;

namespace ArchGuard.Core;

public sealed class ProjectSpecification : IEquatable<ProjectSpecification>
{
    private readonly Project _project;

    internal string Name => _project.Name;

    public ProjectSpecification(Project project)
    {
        _project = project;
    }

    public Compilation GetCompilation()
    {
        return _project.GetCompilationAsync().Result!;
    }

    public bool Equals(ProjectSpecification? other)
    {
        if (other is null)
            return false;

        return other.Name.Equals(Name, StringComparison.Ordinal);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (obj is not ProjectSpecification project)
            return false;

        return project.Equals(this);
    }

    public override int GetHashCode()
    {
        return new { Name }.GetHashCode();
    }
}
