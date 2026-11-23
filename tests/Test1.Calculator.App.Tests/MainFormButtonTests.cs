using Xunit;

namespace Test1.Calculator.App.Tests;

public class MainFormButtonTests
{
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
    public void Click_EachDigitAfterClear_ShowsThatDigit(int digit)
    {
        StaTestRunner.Run(() =>
        {
            using var driver = new MainFormTestDriver();
            driver.Click("btnClear");
            driver.Click($"btnDigit{digit}");
            var expected = digit.ToString();
            Assert.Equal(expected, driver.DisplayText);
        });
    }

    [Theory]
    [InlineData(7, "btnSubtract", 2, "5")]
    [InlineData(3, "btnMultiply", 4, "12")]
    [InlineData(8, "btnDivide", 2, "4")]
    public void Click_OperationButtons_GiveExpectedResults(
        int leftOperand,
        string operationButton,
        int rightOperand,
        string expected)
    {
        StaTestRunner.Run(() =>
        {
            using var driver = new MainFormTestDriver();
            driver.Click($"btnDigit{leftOperand}");
            driver.Click(operationButton);
            driver.Click($"btnDigit{rightOperand}");
            driver.Click("btnEquals");
            Assert.Equal(expected, driver.DisplayText);
        });
    }

    [Fact]
    public void Click_123ThenDelete_Shows12_ThenClear_Shows0()
    {
        StaTestRunner.Run(() =>
        {
            using var driver = new MainFormTestDriver();
            driver.ClickSequence("btnDigit1", "btnDigit2", "btnDigit3");
            driver.Click("btnDelete");
            Assert.Equal("12", driver.DisplayText);
            driver.Click("btnClear");
            Assert.Equal("0", driver.DisplayText);
        });
    }

    [Fact]
    public void Click_DivideByZero_ShowsErrorWithoutException()
    {
        StaTestRunner.Run(() =>
        {
            using var driver = new MainFormTestDriver();
            var exception = Record.Exception(() =>
            {
                driver.ClickSequence("btnDigit5", "btnDivide", "btnDigit0", "btnEquals");
            });
            Assert.Null(exception);
            Assert.Equal("Cannot divide by zero", driver.DisplayText);
        });
    }

    [Fact]
    public void Click_AfterDivideByZero_ClearThenOnePlusOne_Shows2()
    {
        StaTestRunner.Run(() =>
        {
            using var driver = new MainFormTestDriver();
            driver.ClickSequence("btnDigit5", "btnDivide", "btnDigit0", "btnEquals");
            driver.ClickSequence("btnClear", "btnDigit1", "btnAdd", "btnDigit1", "btnEquals");
            Assert.Equal("2", driver.DisplayText);
        });
    }

    [Fact]
    public void Click_AfterDivideByZero_Digit3_Shows3()
    {
        StaTestRunner.Run(() =>
        {
            using var driver = new MainFormTestDriver();
            driver.ClickSequence("btnDigit5", "btnDivide", "btnDigit0", "btnEquals");
            driver.Click("btnDigit3");
            Assert.Equal("3", driver.DisplayText);
        });
    }
}