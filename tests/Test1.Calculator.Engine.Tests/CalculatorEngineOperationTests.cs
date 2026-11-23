using System.Globalization;
using Test1.Calculator.Engine;
using Xunit;

namespace Test1.Calculator.Engine.Tests;

public class CalculatorEngineOperationTests
{
    private static CalculatorEngine RunSequence(string keys)
    {
        var engine = new CalculatorEngine();
        foreach (var ch in keys)
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

        return engine;
    }

    [Theory]
    [InlineData("2+3=", "5")]
    [InlineData("9-4=", "5")]
    [InlineData("6*7=", "42")]
    [InlineData("8/2=", "4")]
    public void WholeNumbers_ShowResult(string keys, string expected)
    {
        var engine = RunSequence(keys);
        Assert.Equal(expected, engine.DisplayText);
    }

    [Theory]
    [InlineData("0.1+0.2=", "0.3")]
    [InlineData("0.3-0.1=", "0.2")]
    [InlineData("0.1*0.2=", "0.02")]
    [InlineData("0.3/0.1=", "3")]
    [InlineData("10/4=", "2.5")]
    public void DecimalNumbers_ShowExactResult(string keys, string expected)
    {
        var engine = RunSequence(keys);
        Assert.Equal(expected, engine.DisplayText);
    }

    [Theory]
    [InlineData("1.5*2=", "3")]
    [InlineData("7.5+7.5=", "15")]
    [InlineData("0.5-0.5=", "0")]
    public void Result_RemovesTrailingZeros(string keys, string expected)
    {
        var engine = RunSequence(keys);
        Assert.Equal(expected, engine.DisplayText);
    }

    [Fact]
    public void NegativeResult_ShowsMinusSign()
    {
        var engine = RunSequence("3-5=");
        Assert.Equal("-2", engine.DisplayText);
        engine.SelectOperation(Operation.Add);
        engine.InputDigit(1);
        engine.Calculate();
        Assert.Equal("-1", engine.DisplayText);
    }

    [Theory]
    [InlineData("1/3=", "0.3333333333333333333333333333")]
    [InlineData("2/3=", "0.6666666666666666666666666667")]
    public void LongDivision_ShowsRoundedDecimalText(string keys, string expected)
    {
        var engine = RunSequence(keys);
        Assert.Equal(expected, engine.DisplayText);
    }

    [Fact]
    public void ChainedOperations_CalculateFromLeftToRight()
    {
        var engine = new CalculatorEngine();
        engine.InputDigit(2);
        engine.SelectOperation(Operation.Add);
        engine.InputDigit(3);
        engine.SelectOperation(Operation.Multiply);
        Assert.Equal("5", engine.DisplayText);
        engine.InputDigit(4);
        engine.Calculate();
        Assert.Equal("20", engine.DisplayText);

        var engine2 = RunSequence("10-2-3=");
        Assert.Equal("5", engine2.DisplayText);
    }

    [Fact]
    public void DisplayAfterOperation_ShowsFirstNumberThenNewEntry()
    {
        var engine = new CalculatorEngine();
        engine.InputDigit(1);
        engine.InputDigit(2);
        engine.SelectOperation(Operation.Add);
        Assert.Equal("12", engine.DisplayText);
        engine.InputDigit(3);
        Assert.Equal("3", engine.DisplayText);

        var engine2 = new CalculatorEngine();
        engine2.InputDigit(1);
        engine2.InputDigit(2);
        engine2.SelectOperation(Operation.Add);
        engine2.InputDecimalPoint();
        Assert.Equal("0.", engine2.DisplayText);
        engine2.Calculate();
        Assert.Equal("12", engine2.DisplayText);
    }

    [Fact]
    public void OperationWithoutNewNumber_ReplacesPendingOperation()
    {
        var engine = RunSequence("5+*3=");
        Assert.Equal("15", engine.DisplayText);
    }

    [Fact]
    public void DigitAfterEquals_StartsNewCalculation()
    {
        var engine = new CalculatorEngine();
        engine.InputDigit(2);
        engine.SelectOperation(Operation.Add);
        engine.InputDigit(3);
        engine.Calculate();
        engine.InputDigit(7);
        Assert.Equal("7", engine.DisplayText);
        engine.SelectOperation(Operation.Add);
        engine.InputDigit(1);
        engine.Calculate();
        Assert.Equal("8", engine.DisplayText);

        var engine2 = new CalculatorEngine();
        engine2.InputDigit(2);
        engine2.SelectOperation(Operation.Add);
        engine2.InputDigit(3);
        engine2.Calculate();
        engine2.InputDecimalPoint();
        Assert.Equal("0.", engine2.DisplayText);
    }

    [Fact]
    public void OperationAfterEquals_UsesResultAsFirstNumber()
    {
        var engine = RunSequence("2+3=*4=");
        Assert.Equal("20", engine.DisplayText);
    }

    [Theory]
    [InlineData("5+=", "5")]
    [InlineData("=", "0")]
    [InlineData("1.50=", "1.5")]
    [InlineData("2+3==", "5")]
    public void Equals_EdgeCases(string keys, string expected)
    {
        var engine = RunSequence(keys);
        Assert.Equal(expected, engine.DisplayText);
    }

    [Fact]
    public void NumberEndingWithPoint_IsValid()
    {
        var engine = RunSequence("5.+2=");
        Assert.Equal("7", engine.DisplayText);
    }

    [Fact]
    public void Delete_AfterResultAndAfterOperation()
    {
        var engine = new CalculatorEngine();
        engine.InputDigit(2);
        engine.SelectOperation(Operation.Add);
        engine.InputDigit(3);
        engine.Calculate();
        engine.Delete();
        Assert.Equal("5", engine.DisplayText);

        var engine2 = new CalculatorEngine();
        engine2.InputDigit(1);
        engine2.InputDigit(2);
        engine2.SelectOperation(Operation.Add);
        engine2.Delete();
        Assert.Equal("12", engine2.DisplayText);
        engine2.InputDigit(3);
        engine2.Calculate();
        Assert.Equal("15", engine2.DisplayText);

        var engine3 = new CalculatorEngine();
        engine3.InputDigit(1);
        engine3.InputDigit(2);
        engine3.SelectOperation(Operation.Add);
        engine3.InputDigit(3);
        engine3.InputDigit(4);
        engine3.Delete();
        engine3.Calculate();
        Assert.Equal("15", engine3.DisplayText);
    }

    [Theory]
    [InlineData("9999999999999999999999999999")]
    [InlineData("0.1111111111111111111111111111")]
    public void InputDigit_IgnoresDigitAfter28Digits(string prefix)
    {
        var engine = new CalculatorEngine();
        if (prefix.StartsWith("0.", StringComparison.Ordinal))
        {
            engine.InputDecimalPoint();
            foreach (var ch in prefix[2..])
            {
                engine.InputDigit(ch - '0');
            }
        }
        else
        {
            foreach (var ch in prefix)
            {
                engine.InputDigit(ch - '0');
            }
        }

        var before = engine.DisplayText;
        engine.InputDigit(prefix.Contains('1') ? 1 : 9);
        Assert.Equal(before, engine.DisplayText);
    }

    [Fact]
    public void Overflow_ShowsErrorAndRecovers()
    {
        var engine = new CalculatorEngine();
        for (var i = 0; i < 28; i++)
        {
            Assert.Null(Record.Exception(() => engine.InputDigit(9)));
        }

        var ex = Record.Exception(() => engine.SelectOperation(Operation.Multiply));
        Assert.Null(ex);
        ex = Record.Exception(() => engine.InputDigit(1));
        Assert.Null(ex);
        ex = Record.Exception(() => engine.InputDigit(0));
        Assert.Null(ex);
        ex = Record.Exception(() => engine.Calculate());
        Assert.Null(ex);
        Assert.Equal("Result is too large", engine.DisplayText);

        var before = engine.DisplayText;
        ex = Record.Exception(() => engine.InputDecimalPoint());
        Assert.Null(ex);
        Assert.Equal(before, engine.DisplayText);
        ex = Record.Exception(() => engine.Delete());
        Assert.Null(ex);
        Assert.Equal(before, engine.DisplayText);
        ex = Record.Exception(() => engine.SelectOperation(Operation.Add));
        Assert.Null(ex);
        Assert.Equal(before, engine.DisplayText);
        ex = Record.Exception(() => engine.Calculate());
        Assert.Null(ex);
        Assert.Equal(before, engine.DisplayText);

        ex = Record.Exception(() => engine.InputDigit(4));
        Assert.Null(ex);
        Assert.Equal("4", engine.DisplayText);
        engine.SelectOperation(Operation.Add);
        engine.InputDigit(1);
        engine.Calculate();
        Assert.Equal("5", engine.DisplayText);
    }

    [Fact]
    public void OverflowInChain_ShowsErrorAndClearResets()
    {
        var engine = new CalculatorEngine();
        for (var i = 0; i < 28; i++)
        {
            engine.InputDigit(9);
        }

        engine.SelectOperation(Operation.Multiply);
        engine.InputDigit(9);
        var ex = Record.Exception(() => engine.SelectOperation(Operation.Add));
        Assert.Null(ex);
        Assert.Equal("Result is too large", engine.DisplayText);

        engine.Clear();
        Assert.Equal("0", engine.DisplayText);
        engine.InputDigit(2);
        engine.SelectOperation(Operation.Add);
        engine.InputDigit(2);
        engine.Calculate();
        Assert.Equal("4", engine.DisplayText);
    }

    [Fact]
    public void Clear_ResetsPendingOperationAndResult()
    {
        Assert.Equal("3", RunSequence("2+C3=").DisplayText);
        Assert.Equal("4", RunSequence("2+3=C4").DisplayText);
    }

    [Fact]
    public void UndefinedOperation_IsIgnored()
    {
        var engine = new CalculatorEngine();
        engine.InputDigit(5);
        var ex = Record.Exception(() => engine.SelectOperation((Operation)99));
        Assert.Null(ex);
        Assert.Equal("5", engine.DisplayText);
        engine.Calculate();
        Assert.Equal("5", engine.DisplayText);
    }

    [Theory]
    [InlineData("de-DE")]
    [InlineData("tr-TR")]
    public void DecimalNumbers_SameResultInCulture(string cultureName)
    {
        var previous = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = new CultureInfo(cultureName);
            Assert.Equal("0.3", RunSequence("0.1+0.2=").DisplayText);
            Assert.Equal("0.2", RunSequence("0.3-0.1=").DisplayText);
            Assert.Equal("0.02", RunSequence("0.1*0.2=").DisplayText);
            Assert.Equal("3", RunSequence("0.3/0.1=").DisplayText);
            Assert.Equal("2.5", RunSequence("10/4=").DisplayText);
        }
        finally
        {
            CultureInfo.CurrentCulture = previous;
        }
    }

    [Theory]
    [InlineData("de-DE")]
    [InlineData("tr-TR")]
    public void Result_RemovesTrailingZerosInCulture(string cultureName)
    {
        var previous = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = new CultureInfo(cultureName);
            Assert.Equal("3", RunSequence("1.5*2=").DisplayText);
            Assert.Equal("15", RunSequence("7.5+7.5=").DisplayText);
            Assert.Equal("0", RunSequence("0.5-0.5=").DisplayText);
        }
        finally
        {
            CultureInfo.CurrentCulture = previous;
        }
    }

    [Theory]
    [InlineData("de-DE")]
    [InlineData("tr-TR")]
    public void NumberEndingWithPoint_IsValidInCulture(string cultureName)
    {
        var previous = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = new CultureInfo(cultureName);
            Assert.Equal("7", RunSequence("5.+2=").DisplayText);
        }
        finally
        {
            CultureInfo.CurrentCulture = previous;
        }
    }

    [Theory]
    [InlineData("8/0=")]
    [InlineData("0/0=")]
    [InlineData("8/.=")]
    public void DivideByZero_ShowsErrorAndRecovers(string keys)
    {
        var engine = new CalculatorEngine();
        foreach (var ch in keys)
        {
            var ex = Record.Exception(() => ApplyKey(engine, ch));
            Assert.Null(ex);
        }

        Assert.Equal("Cannot divide by zero", engine.DisplayText);

        var before = engine.DisplayText;
        Assert.Null(Record.Exception(() => engine.InputDecimalPoint()));
        Assert.Equal(before, engine.DisplayText);
        Assert.Null(Record.Exception(() => engine.Delete()));
        Assert.Equal(before, engine.DisplayText);
        Assert.Null(Record.Exception(() => engine.SelectOperation(Operation.Add)));
        Assert.Equal(before, engine.DisplayText);
        Assert.Null(Record.Exception(() => engine.Calculate()));
        Assert.Equal(before, engine.DisplayText);

        Assert.Null(Record.Exception(() => engine.InputDigit(4)));
        Assert.Equal("4", engine.DisplayText);
        engine.SelectOperation(Operation.Add);
        engine.InputDigit(1);
        engine.Calculate();
        Assert.Equal("5", engine.DisplayText);
    }

    [Fact]
    public void DivideByZeroInChain_ShowsErrorAndClearResets()
    {
        var engine = new CalculatorEngine();
        engine.InputDigit(8);
        engine.SelectOperation(Operation.Divide);
        engine.InputDigit(0);
        var ex = Record.Exception(() => engine.SelectOperation(Operation.Add));
        Assert.Null(ex);
        Assert.Equal("Cannot divide by zero", engine.DisplayText);

        engine.Clear();
        Assert.Equal("0", engine.DisplayText);
        engine.InputDigit(2);
        engine.SelectOperation(Operation.Add);
        engine.InputDigit(2);
        engine.Calculate();
        Assert.Equal("4", engine.DisplayText);
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
}