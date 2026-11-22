using Test1.Calculator.Engine;
using Xunit;

namespace Test1.Calculator.Engine.Tests;

public class CalculatorEngineTests
{
    [Fact]
    public void NewEngine_DisplayText_IsZero()
    {
        var engine = new CalculatorEngine();
        Assert.Equal("0", engine.DisplayText);
    }
}