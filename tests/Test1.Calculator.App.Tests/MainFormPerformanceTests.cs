using System.Diagnostics;
using Xunit;

namespace Test1.Calculator.App.Tests;

public class MainFormPerformanceTests
{
    [Fact]
    public void Click_EqualsAfterWarmUp_ShowsResultInLessThan100Milliseconds()
    {
        StaTestRunner.Run(() =>
        {
            using var driver = new MainFormTestDriver();

            driver.ClickSequence("btnDigit1", "btnAdd", "btnDigit1", "btnEquals");
            driver.Click("btnClear");

            driver.ClickSequence("btnDigit1", "btnDigit2", "btnAdd", "btnDigit3");

            var equalsButton = driver.Find<Button>("btnEquals");
            var stopwatch = Stopwatch.StartNew();
            equalsButton.PerformClick();
            stopwatch.Stop();

            Assert.Equal("15", driver.DisplayText);
            Assert.True(
                stopwatch.ElapsedMilliseconds < 100,
                $"Equals click took {stopwatch.ElapsedMilliseconds} ms; expected less than 100 ms.");
        });
    }
}