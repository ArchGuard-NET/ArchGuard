namespace ArchGuard.Compiling.Assembly;

public sealed class Class;

public sealed class Generic<T>;

public sealed class Generic<T1, T2>;

public interface IInterface;

public interface IGeneric<T>;

public interface IGeneric<T1, T2>;

public sealed class Parent
{
    private sealed class Nested;
}

internal sealed class InternalClass;

public sealed class ClassWithProperties
{
    public required string String { get; set; }
    public required int Int { get; set; }
}

public sealed class ClassWithMethods
{
    public void Void() { }

    public string String()
    {
        return string.Empty;
    }
}

public sealed class ClassWithFields
{
    private readonly string _string;
}

public sealed class ClassWithMultipleConstructors
{
    public ClassWithMultipleConstructors() { }

    public ClassWithMultipleConstructors(string @string) { }
}
