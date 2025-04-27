namespace ArchGuard.Compiling.Tests;

public sealed class PropertiesTests
{
    [Fact]
    public void Test()
    {
        // Arrange
        var project =
            "C:\\Users\\matheus\\source\\repos\\ArchGuard\\"
            + "tests\\project-compiling\\ArchGuard.Compiling.Assembly\\ArchGuard.Compiling.Assembly.csproj";

        var type = Types
            .FromProject(project)
            .GetTypes()
            .First(type =>
                type.FullName.Equals("ArchGuard.Compiling.Assembly.ClassWithProperties", StringComparison.Ordinal)
            );

        // Act
        var properties = type.GetProperties().OrderBy(property => property.FullName, StringComparer.Ordinal);

        // Assert
        var @int = properties.ElementAt(0);
        @int.FullName.ShouldBe("ArchGuard.Compiling.Assembly.ClassWithProperties.Int");
        @int.ContainingType.ShouldBe(type);
        @int.Type.FullName.ShouldBe("System.Int32");

        var @string = properties.ElementAt(1);
        @string.FullName.ShouldBe("ArchGuard.Compiling.Assembly.ClassWithProperties.String");
        @string.ContainingType.ShouldBe(type);
        @string.Type.FullName.ShouldBe("System.String");
    }
}
