using Microsoft.CodeAnalysis.MSBuild;

namespace ArchGuard.Core;

public sealed class ProjectCompiler
{
    public ProjectSpec Compile(string projectFilePath)
    {
        using var workspace = MSBuildWorkspace.Create();

        var project = workspace.OpenProjectAsync(projectFilePath).Result;

        return new ProjectSpec(project);
    }
}
