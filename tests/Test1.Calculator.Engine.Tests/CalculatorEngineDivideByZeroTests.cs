using System.Globalization;
using Test1.Calculator.Engine;
using Xunit;

namespace Test1.Calculator.Engine.Tests;

public class CalculatorEngineDivideByZeroTests
{
    private const string DivideByZeroMessage = "Cannot divide by zero";

    private static readonly string[] Ac1KeySequences =
    [
        "8/0=",
        "0/0=",
        "8/.=",
        "8/0.000=",
        "0.5/0=",
        "123456789/0=",
    ];

    private static CalculatorEngine RunSequence(string keys)
    {
        var engine = new CalculatorEngine();
        foreach (var ch in keys)
        {
            var ex = Record.Exception(() => ApplyKey(engine, ch));
            Assert.Null(ex);
        }

        return engine;
    }

    private static void RunSequenceOn(CalculatorEngine engine, string keys)
    {
        foreach (var ch in keys)
        {
            var ex = Record.Exception(() => ApplyKey(engine, ch));
            Assert.Null(ex);
        }
    }

    private static void ApplyKey(CalculatorEngine engine, char ch)
    {
        switch (ch)
        {
            case >= '0' and <= '9':
                engine.InputDigit(ch - '0');
                break;
            case '.':
                engine.InputDecimalPoint();
                break;
            case '+':
                engine.SelectOperation(Operation.Add);
                break;
            case '-':
                engine.SelectOperation(Operation.Subtract);
                break;
            case '*':
                engine.SelectOperation(Operation.Multiply);
                break;
            case '/':
                engine.SelectOperation(Operation.Divide);
                break;
            case '=':
                engine.Calculate();
                break;
            case 'D':
                engine.Delete();
                break;
            case 'C':
                engine.Clear();
                break;
        }
    }

    public static IEnumerable<object[]> AllOperations()
    {
        yield return new object[] { Operation.Add };
        yield return new object[] { Operation.Subtract };
        yield return new object[] { Operation.Multiply };
        yield return new object[] { Operation.Divide };
    }

    [Theory]
    [InlineData("8/0=")]
    [InlineData("0/0=")]
    [InlineData("8/.=")]
    [InlineData("8/0.000=")]
    [InlineData("0.5/0=")]
    [InlineData("123456789/0=")]
    public void DivideByZero_ShowsError(string keys)
    {
        var engine = RunSequence(keys);
        Assert.Equal(DivideByZeroMessage, engine.DisplayText);
    }

    [Theory]
    [MemberData(nameof(AllOperations))]
    public void DivideByZeroInChain_ThenOperation_ShowsError(Operation op)
    {
        var engine = new CalculatorEngine();
        Assert.Null(Record.Exception(() => engine.InputDigit(8)));
        Assert.Null(Record.Exception(() => engine.SelectOperation(Operation.Divide)));
        Assert.Null(Record.Exception(() => engine.InputDigit(0)));
        var ex = Record.Exception(() => engine.SelectOperation(op));
        Assert.Null(ex);
        Assert.Equal(DivideByZeroMessage, engine.DisplayText);
    }

    [Fact]
    public void ResultZeroAsDividend_DivideByZero_ShowsError()
    {
        var engine = RunSequence("5-5=/0=");
        Assert.Equal(DivideByZeroMessage, engine.DisplayText);
    }

    [Fact]
    public void DivisorDeletedToZero_ShowsError()
    {
        var engine = new CalculatorEngine();
        Assert.Null(Record.Exception(() => engine.InputDigit(8)));
        Assert.Null(Record.Exception(() => engine.SelectOperation(Operation.Divide)));
        Assert.Null(Record.Exception(() => engine.InputDigit(5)));
        Assert.Null(Record.Exception(() => engine.Delete()));
        var ex = Record.Exception(() => engine.Calculate());
        Assert.Null(ex);
        Assert.Equal(DivideByZeroMessage, engine.DisplayText);
    }

    [Fact]
    public void ErrorState_IgnoredActions_DoNotChangeDisplay()
    {
        var engine = RunSequence("8/0=");
        Assert.Equal(DivideByZeroMessage, engine.DisplayText);

        Assert.Null(Record.Exception(() => engine.InputDecimalPoint()));
        Assert.Equal(DivideByZeroMessage, engine.DisplayText);

        Assert.Null(Record.Exception(() => engine.Delete()));
        Assert.Equal(DivideByZeroMessage, engine.DisplayText);

        Assert.Null(Record.Exception(() => engine.SelectOperation(Operation.Add)));
        Assert.Equal(DivideByZeroMessage, engine.DisplayText);
        Assert.Null(Record.Exception(() => engine.SelectOperation(Operation.Subtract)));
        Assert.Equal(DivideByZeroMessage, engine.DisplayText);
        Assert.Null(Record.Exception(() => engine.SelectOperation(Operation.Multiply)));
        Assert.Equal(DivideByZeroMessage, engine.DisplayText);
        Assert.Null(Record.Exception(() => engine.SelectOperation(Operation.Divide)));
        Assert.Equal(DivideByZeroMessage, engine.DisplayText);

        Assert.Null(Record.Exception(() => engine.Calculate()));
        Assert.Equal(DivideByZeroMessage, engine.DisplayText);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]
    public void ErrorState_Digit_StartsNewNumber(int digit)
    {
        var engine = RunSequence("8/0=");
        var ex = Record.Exception(() => engine.InputDigit(digit));
        Assert.Null(ex);
        Assert.Equal(digit.ToString(), engine.DisplayText);
    }

    [Fact]
    public void ErrorState_DigitThenFullCalculation_Works()
    {
        var engine = RunSequence("8/0=");
        RunSequenceOn(engine, "4+1=");
        Assert.Equal("5", engine.DisplayText);
    }

    [Fact]
    public void ErrorState_Clear_ShowsZeroAndRecovers()
    {
        var engine = RunSequence("8/0=");
        var ex = Record.Exception(() => engine.Clear());
        Assert.Null(ex);
        Assert.Equal("0", engine.DisplayText);
        RunSequenceOn(engine, "2+2=");
        Assert.Equal("4", engine.DisplayText);
    }

    [Theory]
    [InlineData("8/0=3=", "3")]
    [InlineData("8/0=3+2=", "5")]
    public void AfterRecovery_NoOldState(string keys, string expected)
    {
        var engine = RunSequence(keys);
        Assert.Equal(expected, engine.DisplayText);
    }

    [Theory]
    [InlineData("0/5=", "0")]
    [InlineData("1/4=", "0.25")]
    public void NonZeroDivision_IsNotError(string keys, string expected)
    {
        var engine = RunSequence(keys);
        Assert.Equal(expected, engine.DisplayText);
    }

    [Theory]
    [InlineData("de-DE")]
    [InlineData("tr-TR")]
    public void DivideByZero_ShowsError_InCulture(string cultureName)
    {
        var previous = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = new CultureInfo(cultureName);
            foreach (var keys in Ac1KeySequences)
            {
                var engine = RunSequence(keys);
                Assert.Equal(DivideByZeroMessage, engine.DisplayText);
            }
        }
        finally
        {
            CultureInfo.CurrentCulture = previous;
        }
    }

    [Theory]
    [InlineData("de-DE")]
    [InlineData("tr-TR")]
    public void ErrorState_DigitAndCalculation_InCulture(string cultureName)
    {
        var previous = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = new CultureInfo(cultureName);
            for (var digit = 0; digit <= 9; digit++)
            {
                var engine = RunSequence("8/0=");
                var ex = Record.Exception(() => engine.InputDigit(digit));
                Assert.Null(ex);
                Assert.Equal(digit.ToString(), engine.DisplayText);
            }

            var calcEngine = RunSequence("8/0=");
            RunSequenceOn(calcEngine, "4+1=");
            Assert.Equal("5", calcEngine.DisplayText);
        }
        finally
        {
            CultureInfo.CurrentCulture = previous;
        }
    }
}