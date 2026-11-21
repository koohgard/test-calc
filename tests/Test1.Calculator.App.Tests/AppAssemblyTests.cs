using Test1.Calculator.App;
using Xunit;

namespace Test1.Calculator.App.Tests;

public class AppAssemblyTests
{
    [Fact]
    public void AppAssembly_Loads()
    {
        var assembly = typeof(MainForm).Assembly;
        Assert.NotNull(assembly);
        Assert.Equal("Test1.Calculator.App", assembly.GetName().Name);
    }
}