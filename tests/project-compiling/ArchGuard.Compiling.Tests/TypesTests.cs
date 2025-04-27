namespace ArchGuard.Compiling.Tests;

public class TypesTests
{
    [Fact]
    public void Test()
    {
        // Arrange
        var project =
            "C:\\Users\\matheus\\source\\repos\\ArchGuard\\"
            + "tests\\project-compiling\\ArchGuard.Compiling.Assembly\\ArchGuard.Compiling.Assembly.csproj";

        // Act
        var types = Types.FromProject(project).GetTypes();
        var ordered = types.Select(type => type.FullName).Order(StringComparer.Ordinal);

        // Assert
        ordered.ShouldBe(
            [
                "ArchGuard.Compiling.Assembly.Class",
                "ArchGuard.Compiling.Assembly.ClassWithFields",
                "ArchGuard.Compiling.Assembly.ClassWithMethods",
                "ArchGuard.Compiling.Assembly.ClassWithMultipleConstructors",
                "ArchGuard.Compiling.Assembly.ClassWithProperties",
                "ArchGuard.Compiling.Assembly.Generic`1",
                "ArchGuard.Compiling.Assembly.Generic`2",
                "ArchGuard.Compiling.Assembly.IGeneric`1",
                "ArchGuard.Compiling.Assembly.IGeneric`2",
                "ArchGuard.Compiling.Assembly.IInterface",
                "ArchGuard.Compiling.Assembly.InternalClass",
                "ArchGuard.Compiling.Assembly.Parent",
                "ArchGuard.Compiling.Assembly.Parent+Nested",
            ]
        );
    }
}
