using Test1.Calculator.App;
using Xunit;

namespace Test1.Calculator.App.Tests;

public class MainFormTests
{
    [Fact]
    public void MainForm_Title_IsCalculator()
    {
        StaTestRunner.Run(() =>
        {
            using var form = new MainForm();
            Assert.Equal("Calculator", form.Text);
        });
    }

    [Fact]
    public void MainForm_OnOpen_DisplayIsZeroAndReadOnly()
    {
        StaTestRunner.Run(() =>
        {
            using var driver = new MainFormTestDriver();
            var display = driver.Find<TextBox>("txtDisplay");
            Assert.Equal("0", driver.DisplayText);
            Assert.True(display.ReadOnly);
            Assert.False(display.TabStop);
        });
    }

    [Theory]
    [InlineData("btnDigit0", "0")]
    [InlineData("btnDigit1", "1")]
    [InlineData("btnDigit2", "2")]
    [InlineData("btnDigit3", "3")]
    [InlineData("btnDigit4", "4")]
    [InlineData("btnDigit5", "5")]
    [InlineData("btnDigit6", "6")]
    [InlineData("btnDigit7", "7")]
    [InlineData("btnDigit8", "8")]
    [InlineData("btnDigit9", "9")]
    [InlineData("btnDecimalPoint", ".")]
    [InlineData("btnAdd", "+")]
    [InlineData("btnSubtract", "-")]
    [InlineData("btnMultiply", "*")]
    [InlineData("btnDivide", "/")]
    [InlineData("btnEquals", "=")]
    [InlineData("btnClear", "Clear")]
    [InlineData("btnDelete", "Delete")]
    public void MainForm_ContainsAll18Buttons_WithExpectedText(string name, string expectedText)
    {
        StaTestRunner.Run(() =>
        {
            using var driver = new MainFormTestDriver();
            var button = driver.Find<Button>(name);
            Assert.Equal(expectedText, button.Text);
        });
    }

    [Fact]
    public void Click_12Point5Plus2Equals_Shows14Point5()
    {
        StaTestRunner.Run(() =>
        {
            using var driver = new MainFormTestDriver();
            driver.ClickSequence(
                "btnDigit1",
                "btnDigit2",
                "btnDecimalPoint",
                "btnDigit5",
                "btnAdd",
                "btnDigit2",
                "btnEquals");
            Assert.Equal("14.5", driver.DisplayText);
        });
    }
}