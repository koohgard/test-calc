using System.Globalization;
using Xunit;

namespace Test1.Calculator.App.Tests;

public class MainFormKeyboardTests
{
    [Fact]
    public void MainForm_KeyPreview_IsTrue()
    {
        StaTestRunner.Run(() =>
        {
            using var driver = new MainFormTestDriver();
            Assert.True(driver.KeyPreview);
        });
    }

    [Fact]
    public void Keyboard_TypeExpressionAndEnter_Shows15()
    {
        StaTestRunner.Run(() =>
        {
            using var driver = new MainFormTestDriver();
            driver.TypeText("7.5*2");
            driver.PressKey(Keys.Enter);
            Assert.Equal("15", driver.DisplayText);
        });
    }

    [Fact]
    public void Keyboard_RepeatedDecimalPoint_IsIgnored()
    {
        StaTestRunner.Run(() =>
        {
            using var driver = new MainFormTestDriver();
            driver.TypeText("1..5");
            Assert.Equal("1.5", driver.DisplayText);
        });
    }

    [Fact]
    public void Keyboard_SecondDecimalPoint_IsIgnored()
    {
        StaTestRunner.Run(() =>
        {
            using var driver = new MainFormTestDriver();
            driver.TypeText("1.2.3");
            Assert.Equal("1.23", driver.DisplayText);
        });
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
    public void Keyboard_EachDigitCharacterAfterEscape_ShowsThatDigit(int digit)
    {
        StaTestRunner.Run(() =>
        {
            using var driver = new MainFormTestDriver();
            driver.PressKey(Keys.Escape);
            driver.TypeText(digit.ToString());
            Assert.Equal(digit.ToString(), driver.DisplayText);
        });
    }

    [Theory]
    [InlineData(Keys.NumPad0, "0")]
    [InlineData(Keys.NumPad1, "1")]
    [InlineData(Keys.NumPad2, "2")]
    [InlineData(Keys.NumPad3, "3")]
    [InlineData(Keys.NumPad4, "4")]
    [InlineData(Keys.NumPad5, "5")]
    [InlineData(Keys.NumPad6, "6")]
    [InlineData(Keys.NumPad7, "7")]
    [InlineData(Keys.NumPad8, "8")]
    [InlineData(Keys.NumPad9, "9")]
    public void Keyboard_EachNumPadKeyAfterEscape_ShowsThatDigitOnce(Keys key, string expected)
    {
        StaTestRunner.Run(() =>
        {
            using var driver = new MainFormTestDriver();
            driver.PressKey(Keys.Escape);
            driver.PressKey(key);
            Assert.Equal(expected, driver.DisplayText);
        });
    }

    [Theory]
    [InlineData("7-2=", "5")]
    [InlineData("3*4=", "12")]
    [InlineData("8/2=", "4")]
    [InlineData("1+1=", "2")]
    public void Keyboard_MainKeyboardOperations_GiveExpectedResults(string expression, string expected)
    {
        StaTestRunner.Run(() =>
        {
            using var driver = new MainFormTestDriver();
            driver.TypeText(expression);
            Assert.Equal(expected, driver.DisplayText);
        });
    }

    [Theory]
    [InlineData(
        new[] { Keys.NumPad1, Keys.Decimal, Keys.NumPad5, Keys.Add, Keys.NumPad2, Keys.Enter },
        "3.5")]
    [InlineData(
        new[] { Keys.NumPad9, Keys.Subtract, Keys.NumPad4, Keys.Enter },
        "5")]
    [InlineData(
        new[] { Keys.NumPad6, Keys.Multiply, Keys.NumPad7, Keys.Enter },
        "42")]
    [InlineData(
        new[] { Keys.NumPad9, Keys.Divide, Keys.NumPad3, Keys.Enter },
        "3")]
    public void Keyboard_NumPadOperations_GiveExpectedResults(Keys[] keys, string expected)
    {
        StaTestRunner.Run(() =>
        {
            using var driver = new MainFormTestDriver();
            foreach (var key in keys)
            {
                driver.PressKey(key);
            }

            Assert.Equal(expected, driver.DisplayText);
        });
    }

    [Fact]
    public void Keyboard_BackThenEscape_DeletesThenClears()
    {
        StaTestRunner.Run(() =>
        {
            using var driver = new MainFormTestDriver();
            driver.TypeText("123");
            driver.PressKey(Keys.Back);
            Assert.Equal("12", driver.DisplayText);
            driver.PressKey(Keys.Escape);
            Assert.Equal("0", driver.DisplayText);
        });
    }

    [Fact]
    public void Keyboard_EnterAndEscapeWithFocusedButton_GoToEngine()
    {
        StaTestRunner.Run(() =>
        {
            using var driver = new MainFormTestDriver();
            driver.Click("btnAdd");
            driver.TypeText("5+2");
            driver.PressKey(Keys.Enter);
            Assert.Equal("7", driver.DisplayText);
            driver.PressKey(Keys.Escape);
            Assert.Equal("0", driver.DisplayText);
        });
    }

    [Fact]
    public void Keyboard_DivideByZero_ShowsMessageAndRecovers()
    {
        StaTestRunner.Run(() =>
        {
            using var driver = new MainFormTestDriver();
            var exception = Record.Exception(() => driver.TypeText("5/0="));
            Assert.Null(exception);
            Assert.Equal("Cannot divide by zero", driver.DisplayText);
            driver.PressKey(Keys.Escape);
            driver.TypeText("2+2=");
            Assert.Equal("4", driver.DisplayText);
        });
    }

    [Fact]
    public void Keyboard_UnmappedCharacters_AreIgnored()
    {
        StaTestRunner.Run(() =>
        {
            using var driver = new MainFormTestDriver();
            var exception = Record.Exception(() => driver.TypeText("a,% "));
            Assert.Null(exception);
            Assert.Equal("0", driver.DisplayText);
        });
    }

    [Fact]
    public void Keyboard_TypeExpressionWithGermanCulture_Shows15()
    {
        StaTestRunner.Run(() =>
        {
            var previousCulture = CultureInfo.CurrentCulture;
            var previousUiCulture = CultureInfo.CurrentUICulture;
            try
            {
                var german = CultureInfo.GetCultureInfo("de-DE");
                CultureInfo.CurrentCulture = german;
                CultureInfo.CurrentUICulture = german;

                using var driver = new MainFormTestDriver();
                driver.TypeText("7.5*2");
                driver.PressKey(Keys.Enter);
                Assert.Equal("15", driver.DisplayText);
            }
            finally
            {
                CultureInfo.CurrentCulture = previousCulture;
                CultureInfo.CurrentUICulture = previousUiCulture;
            }
        });
    }

    [Fact]
    public void Keyboard_DecimalKeyWithGermanCulture_ShowsPoint()
    {
        StaTestRunner.Run(() =>
        {
            var previousCulture = CultureInfo.CurrentCulture;
            var previousUiCulture = CultureInfo.CurrentUICulture;
            try
            {
                var german = CultureInfo.GetCultureInfo("de-DE");
                CultureInfo.CurrentCulture = german;
                CultureInfo.CurrentUICulture = german;

                using var driver = new MainFormTestDriver();
                driver.PressKey(Keys.Decimal);
                Assert.Equal("0.", driver.DisplayText);
            }
            finally
            {
                CultureInfo.CurrentCulture = previousCulture;
                CultureInfo.CurrentUICulture = previousUiCulture;
            }
        });
    }
}