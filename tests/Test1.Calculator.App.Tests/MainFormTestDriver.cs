using Test1.Calculator.App;
using Xunit;

namespace Test1.Calculator.App.Tests;

internal sealed class MainFormTestDriver : IDisposable
{
    private readonly MainForm _form;

    public MainFormTestDriver()
    {
        _form = new MainForm();
        _form.ShowInTaskbar = false;
        _form.StartPosition = FormStartPosition.Manual;
        _form.Location = new Point(-10_000, -10_000);
        _form.CreateControl();
        _form.Show();
    }

    public T Find<T>(string name)
        where T : Control
    {
        var matches = _form.Controls.Find(name, true);
        Assert.Single(matches);
        Assert.IsType<T>(matches[0]);
        return (T)matches[0];
    }

    public void Click(string buttonName) => Find<Button>(buttonName).PerformClick();

    public void ClickSequence(params string[] names)
    {
        foreach (var name in names)
        {
            Click(name);
        }
    }

    public string DisplayText => Find<TextBox>("txtDisplay").Text;

    public void Dispose() => _form.Dispose();
}