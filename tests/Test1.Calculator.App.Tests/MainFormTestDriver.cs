using System.Runtime.InteropServices;
using Test1.Calculator.App;
using Xunit;

namespace Test1.Calculator.App.Tests;

internal sealed partial class MainFormTestDriver : IDisposable
{
    private const uint WmKeyDown = 0x0100;
    private const uint WmKeyUp = 0x0101;
    private const uint WmChar = 0x0102;

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

    public bool KeyPreview => _form.KeyPreview;

    public T Find<T>(string name)
        where T : Control
    {
        var matches = _form.Controls.Find(name, true);
        Assert.Single(matches);
        Assert.IsType<T>(matches[0]);
        return (T)matches[0];
    }

    public void Click(string buttonName)
    {
        var button = Find<Button>(buttonName);
        button.PerformClick();
        if (!button.Focused)
        {
            button.Focus();
        }
    }

    public void ClickSequence(params string[] names)
    {
        foreach (var name in names)
        {
            Click(name);
        }
    }

    public string DisplayText => Find<TextBox>("txtDisplay").Text;

    public void PressKey(Keys key)
    {
        var target = MessageTargetHandle;
        var wParam = (nint)key;
        _ = Native.PostMessage(target, WmKeyDown, wParam, 1);
        Application.DoEvents();
        _ = Native.PostMessage(target, WmKeyUp, wParam, unchecked((nint)0xC0000001));
        Application.DoEvents();
    }

    public void TypeText(string text)
    {
        var target = MessageTargetHandle;
        foreach (var ch in text)
        {
            _ = Native.PostMessage(target, WmChar, ch, 1);
            Application.DoEvents();
        }
    }

    private nint MessageTargetHandle
    {
        get
        {
            var focused = Native.GetFocus();
            if (focused != 0 && Control.FromHandle(focused) is not null)
            {
                return focused;
            }

            var active = GetDeepActiveControl(_form);
            if (active is not null)
            {
                return active.Handle;
            }

            return _form.Handle;
        }
    }

    private static Control? GetDeepActiveControl(Control container)
    {
        if (container is ContainerControl { ActiveControl: { } active })
        {
            return GetDeepActiveControl(active) ?? active;
        }

        return container.Focused ? container : null;
    }

    public void Dispose() => _form.Dispose();

    private static partial class Native
    {
        [LibraryImport("user32.dll", EntryPoint = "PostMessageW")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static partial bool PostMessage(nint hWnd, uint msg, nint wParam, nint lParam);

        [LibraryImport("user32.dll")]
        internal static partial nint GetFocus();
    }
}