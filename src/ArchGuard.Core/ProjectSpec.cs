using Microsoft.CodeAnalysis;

namespace ArchGuard.Core;

public sealed class ProjectSpec : IEquatable<ProjectSpec>
{
    private readonly Project _project;

    internal string Name => _project.Name;

    public ProjectSpec(Project project)
    {
        _project = project;
    }

    public Compilation GetCompilation()
    {
        return _project.GetCompilationAsync().Result!;
    }

    public bool Equals(ProjectSpec? other)
    {
        if (other is null)
            return false;

        return other.Name.Equals(Name, StringComparison.Ordinal);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (obj is not ProjectSpec project)
            return false;

        return project.Equals(this);
    }

    public override int GetHashCode()
    {
        return new { Name }.GetHashCode();
    }
}
