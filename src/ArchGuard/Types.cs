using ArchGuard.Core;

namespace ArchGuard;

public static class Types
{
    public static TypesContext FromProject(string projectFilePath)
    {
        var project = new ProjectCompiler().Compile(projectFilePath);
        var types = new TypesLoader().GetTypes(project);

        return new TypesContext(types);
    }
}
