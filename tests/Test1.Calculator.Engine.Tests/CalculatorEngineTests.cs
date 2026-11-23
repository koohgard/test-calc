using System.Globalization;
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

    [Theory]
    [InlineData(0, "0")]
    [InlineData(1, "1")]
    [InlineData(2, "2")]
    [InlineData(3, "3")]
    [InlineData(4, "4")]
    [InlineData(5, "5")]
    [InlineData(6, "6")]
    [InlineData(7, "7")]
    [InlineData(8, "8")]
    [InlineData(9, "9")]
    public void InputDigit_OnNewEngine_ShowsDigit(int digit, string expected)
    {
        var engine = new CalculatorEngine();
        engine.InputDigit(digit);
        Assert.Equal(expected, engine.DisplayText);
    }

    [Fact]
    public void InputDigits_123_Shows123()
    {
        var engine = new CalculatorEngine();
        engine.InputDigit(1);
        engine.InputDigit(2);
        engine.InputDigit(3);
        Assert.Equal("123", engine.DisplayText);
    }

    [Fact]
    public void InputDigits_000_ShowsZero()
    {
        var engine = new CalculatorEngine();
        engine.InputDigit(0);
        engine.InputDigit(0);
        engine.InputDigit(0);
        Assert.Equal("0", engine.DisplayText);
    }

    [Fact]
    public void InputDigits_07_Shows7()
    {
        var engine = new CalculatorEngine();
        engine.InputDigit(0);
        engine.InputDigit(7);
        Assert.Equal("7", engine.DisplayText);
    }

    [Fact]
    public void InputDigitPointDigit_Shows1Point5()
    {
        var engine = new CalculatorEngine();
        engine.InputDigit(1);
        engine.InputDecimalPoint();
        engine.InputDigit(5);
        Assert.Equal("1.5", engine.DisplayText);
    }

    [Fact]
    public void InputDecimalPoint_OnNewEngine_ShowsZeroPoint_ThenDigitShowsZeroPoint5()
    {
        var engine = new CalculatorEngine();
        engine.InputDecimalPoint();
        Assert.Equal("0.", engine.DisplayText);
        engine.InputDigit(5);
        Assert.Equal("0.5", engine.DisplayText);
    }

    [Fact]
    public void InputDecimalPoint_Twice_IgnoresSecondPoint()
    {
        var engine = new CalculatorEngine();
        engine.InputDigit(1);
        engine.InputDecimalPoint();
        engine.InputDecimalPoint();
        engine.InputDigit(5);
        Assert.Equal("1.5", engine.DisplayText);
    }

    [Fact]
    public void InputDecimalPoint_AfterDigits_IgnoresSecondPoint()
    {
        var engine = new CalculatorEngine();
        engine.InputDigit(1);
        engine.InputDecimalPoint();
        engine.InputDigit(5);
        engine.InputDecimalPoint();
        engine.InputDigit(2);
        Assert.Equal("1.52", engine.DisplayText);
    }

    [Fact]
    public void InputTrailingZero_KeepsTrailingZero()
    {
        var engine = new CalculatorEngine();
        engine.InputDigit(1);
        engine.InputDecimalPoint();
        engine.InputDigit(5);
        engine.InputDigit(0);
        Assert.Equal("1.50", engine.DisplayText);
    }

    [Fact]
    public void Delete_After123_Shows12()
    {
        var engine = new CalculatorEngine();
        engine.InputDigit(1);
        engine.InputDigit(2);
        engine.InputDigit(3);
        engine.Delete();
        Assert.Equal("12", engine.DisplayText);
    }

    [Fact]
    public void Delete_AfterZeroPoint5_ShowsZeroPoint_ThenZero()
    {
        var engine = new CalculatorEngine();
        engine.InputDecimalPoint();
        engine.InputDigit(5);
        engine.Delete();
        Assert.Equal("0.", engine.DisplayText);
        engine.Delete();
        Assert.Equal("0", engine.DisplayText);
    }

    [Fact]
    public void Delete_AfterSingleDigit_ShowsZero()
    {
        var engine = new CalculatorEngine();
        engine.InputDigit(7);
        engine.Delete();
        Assert.Equal("0", engine.DisplayText);
    }

    [Fact]
    public void Delete_OnNewEngine_ShowsZero_AndDoesNotThrow()
    {
        var engine = new CalculatorEngine();
        for (var i = 0; i < 4; i++)
        {
            var ex = Record.Exception(() => engine.Delete());
            Assert.Null(ex);
            Assert.Equal("0", engine.DisplayText);
        }
    }

    [Fact]
    public void Delete_RemovesDecimalPoint_ThenPointCanBeAddedAgain()
    {
        var engine = new CalculatorEngine();
        engine.InputDigit(1);
        engine.InputDigit(2);
        engine.InputDecimalPoint();
        engine.Delete();
        Assert.Equal("12", engine.DisplayText);
        engine.InputDecimalPoint();
        engine.InputDigit(5);
        Assert.Equal("12.5", engine.DisplayText);
    }

    [Fact]
    public void Clear_AfterInput_ShowsZero()
    {
        var engine = new CalculatorEngine();
        engine.InputDigit(1);
        engine.InputDigit(2);
        engine.InputDecimalPoint();
        engine.InputDigit(3);
        engine.Clear();
        Assert.Equal("0", engine.DisplayText);
    }

    [Fact]
    public void Clear_ThenDigit_ShowsDigit()
    {
        var engine = new CalculatorEngine();
        engine.InputDigit(1);
        engine.InputDigit(2);
        engine.InputDecimalPoint();
        engine.InputDigit(3);
        engine.Clear();
        engine.InputDigit(4);
        Assert.Equal("4", engine.DisplayText);
    }

    [Fact]
    public void Clear_ThenDecimalPoint_ShowsZeroPoint()
    {
        var engine = new CalculatorEngine();
        engine.InputDigit(1);
        engine.InputDigit(2);
        engine.InputDecimalPoint();
        engine.InputDigit(3);
        engine.Clear();
        engine.InputDecimalPoint();
        Assert.Equal("0.", engine.DisplayText);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(10)]
    public void InputDigit_OutOfRange_DoesNotChangeDisplay(int invalidDigit)
    {
        var engine = new CalculatorEngine();
        engine.InputDigit(5);
        var before = engine.DisplayText;
        var ex = Record.Exception(() => engine.InputDigit(invalidDigit));
        Assert.Null(ex);
        Assert.Equal(before, engine.DisplayText);
    }

    [Theory]
    [InlineData("de-DE")]
    [InlineData("tr-TR")]
    public void DecimalInput_UsesInvariantPoint_InCulture(string cultureName)
    {
        var previous = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = new CultureInfo(cultureName);

            var engineAc5 = new CalculatorEngine();
            engineAc5.InputDigit(1);
            engineAc5.InputDecimalPoint();
            engineAc5.InputDigit(5);
            Assert.Equal("1.5", engineAc5.DisplayText);

            var engineAc6 = new CalculatorEngine();
            engineAc6.InputDecimalPoint();
            Assert.Equal("0.", engineAc6.DisplayText);
            engineAc6.InputDigit(5);
            Assert.Equal("0.5", engineAc6.DisplayText);

            var engineAc7a = new CalculatorEngine();
            engineAc7a.InputDigit(1);
            engineAc7a.InputDecimalPoint();
            engineAc7a.InputDecimalPoint();
            engineAc7a.InputDigit(5);
            Assert.Equal("1.5", engineAc7a.DisplayText);

            var engineAc7b = new CalculatorEngine();
            engineAc7b.InputDigit(1);
            engineAc7b.InputDecimalPoint();
            engineAc7b.InputDigit(5);
            engineAc7b.InputDecimalPoint();
            engineAc7b.InputDigit(2);
            Assert.Equal("1.52", engineAc7b.DisplayText);
        }
        finally
        {
            CultureInfo.CurrentCulture = previous;
        }
    }
}