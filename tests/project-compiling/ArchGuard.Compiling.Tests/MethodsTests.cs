namespace ArchGuard.Compiling.Tests;

public sealed class MethodsTests
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
                type.FullName.Equals("ArchGuard.Compiling.Assembly.ClassWithMethods", StringComparison.Ordinal)
            );

        // Act
        var methods = type.GetMethods().OrderBy(method => method.FullName, StringComparer.Ordinal);

        // Assert
        var @string = methods.ElementAt(0);
        @string.FullName.ShouldBe("ArchGuard.Compiling.Assembly.ClassWithMethods.String()");
        @string.ContainingType.ShouldBe(type);
        @string.ReturnType.FullName.ShouldBe("System.String");

        var @void = methods.ElementAt(1);
        @void.FullName.ShouldBe("ArchGuard.Compiling.Assembly.ClassWithMethods.Void()");
        @void.ContainingType.ShouldBe(type);
        @void.ReturnType.FullName.ShouldBe("System.Void");
    }
}
