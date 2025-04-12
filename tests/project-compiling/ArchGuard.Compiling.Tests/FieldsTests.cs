namespace ArchGuard.Compiling.Tests;

public sealed class FieldsTests
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
                type.FullName.Equals("ArchGuard.Compiling.Assembly.ClassWithFields", StringComparison.Ordinal)
            );

        // Act
        var field = type.GetFields().First();

        // Assert
        field.FullName.ShouldBe("ArchGuard.Compiling.Assembly.ClassWithFields._string");
        field.ContainingType.ShouldBe(type);
        field.Type.FullName.ShouldBe("System.String");
    }
}
