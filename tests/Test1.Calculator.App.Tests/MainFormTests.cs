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
}