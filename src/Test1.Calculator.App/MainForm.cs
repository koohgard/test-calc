using Test1.Calculator.Engine;

namespace Test1.Calculator.App;

public partial class MainForm : Form
{
    private readonly CalculatorEngine _engine = new();

    public MainForm()
    {
        InitializeComponent();
        UpdateDisplay();
    }

    private void UpdateDisplay() => txtDisplay.Text = _engine.DisplayText;

    private void DigitButton_Click(object? sender, EventArgs e)
    {
        if (sender is Button { Tag: int digit })
        {
            _engine.InputDigit(digit);
            UpdateDisplay();
        }
    }

    private void btnDecimalPoint_Click(object? sender, EventArgs e)
    {
        _engine.InputDecimalPoint();
        UpdateDisplay();
    }

    private void btnAdd_Click(object? sender, EventArgs e)
    {
        _engine.SelectOperation(Operation.Add);
        UpdateDisplay();
    }

    private void btnSubtract_Click(object? sender, EventArgs e)
    {
        _engine.SelectOperation(Operation.Subtract);
        UpdateDisplay();
    }

    private void btnMultiply_Click(object? sender, EventArgs e)
    {
        _engine.SelectOperation(Operation.Multiply);
        UpdateDisplay();
    }

    private void btnDivide_Click(object? sender, EventArgs e)
    {
        _engine.SelectOperation(Operation.Divide);
        UpdateDisplay();
    }

    private void btnEquals_Click(object? sender, EventArgs e)
    {
        _engine.Calculate();
        UpdateDisplay();
    }

    private void btnClear_Click(object? sender, EventArgs e)
    {
        _engine.Clear();
        UpdateDisplay();
    }

    private void btnDelete_Click(object? sender, EventArgs e)
    {
        _engine.Delete();
        UpdateDisplay();
    }
}