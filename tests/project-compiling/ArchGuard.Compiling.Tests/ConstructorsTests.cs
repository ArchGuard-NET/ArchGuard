namespace ArchGuard.Compiling.Tests;

public sealed class ConstructorsTests
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
                type.FullName.Equals(
                    "ArchGuard.Compiling.Assembly.ClassWithMultipleConstructors",
                    StringComparison.Ordinal
                )
            );

        // Act
        var constructors = type.GetConstructors().OrderBy(constructor => constructor.FullName, StringComparer.Ordinal);

        // Assert
        var empty = constructors.ElementAt(0);
        empty.FullName.ShouldBe("ArchGuard.Compiling.Assembly.ClassWithMultipleConstructors._ctor()");
        empty.ContainingType.ShouldBe(type);

        var stringParameter = constructors.ElementAt(1);
        stringParameter.FullName.ShouldBe("ArchGuard.Compiling.Assembly.ClassWithMultipleConstructors._ctor(String)");
        stringParameter.ContainingType.ShouldBe(type);
    }
}
