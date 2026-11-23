using Test1.Calculator.Engine;

namespace Test1.Calculator.App;

public partial class MainForm : Form
{
    private readonly CalculatorEngine _engine = new();

    public MainForm()
    {
        InitializeComponent();
        KeyPreview = true;
        KeyDown += MainForm_KeyDown;
        KeyPress += MainForm_KeyPress;
        UpdateDisplay();
    }

    private void UpdateDisplay() => txtDisplay.Text = _engine.DisplayText;

    private bool HandleKey(char? character, Keys keyData)
    {
        if (character is char ch)
        {
            if (ch is >= '0' and <= '9')
            {
                _engine.InputDigit(ch - '0');
                UpdateDisplay();
                return true;
            }

            if (ch == '.')
            {
                _engine.InputDecimalPoint();
                UpdateDisplay();
                return true;
            }

            switch (ch)
            {
                case '+':
                    _engine.SelectOperation(Operation.Add);
                    UpdateDisplay();
                    return true;
                case '-':
                    _engine.SelectOperation(Operation.Subtract);
                    UpdateDisplay();
                    return true;
                case '*':
                    _engine.SelectOperation(Operation.Multiply);
                    UpdateDisplay();
                    return true;
                case '/':
                    _engine.SelectOperation(Operation.Divide);
                    UpdateDisplay();
                    return true;
                case '=':
                    _engine.Calculate();
                    UpdateDisplay();
                    return true;
                default:
                    return false;
            }
        }

        if ((keyData & Keys.Control) != 0 || (keyData & Keys.Alt) != 0)
        {
            return false;
        }

        switch (keyData & Keys.KeyCode)
        {
            case Keys.Enter:
                _engine.Calculate();
                UpdateDisplay();
                return true;
            case Keys.Back:
                _engine.Delete();
                UpdateDisplay();
                return true;
            case Keys.Escape:
                _engine.Clear();
                UpdateDisplay();
                return true;
            case Keys.NumPad0:
                _engine.InputDigit(0);
                UpdateDisplay();
                return true;
            case Keys.NumPad1:
                _engine.InputDigit(1);
                UpdateDisplay();
                return true;
            case Keys.NumPad2:
                _engine.InputDigit(2);
                UpdateDisplay();
                return true;
            case Keys.NumPad3:
                _engine.InputDigit(3);
                UpdateDisplay();
                return true;
            case Keys.NumPad4:
                _engine.InputDigit(4);
                UpdateDisplay();
                return true;
            case Keys.NumPad5:
                _engine.InputDigit(5);
                UpdateDisplay();
                return true;
            case Keys.NumPad6:
                _engine.InputDigit(6);
                UpdateDisplay();
                return true;
            case Keys.NumPad7:
                _engine.InputDigit(7);
                UpdateDisplay();
                return true;
            case Keys.NumPad8:
                _engine.InputDigit(8);
                UpdateDisplay();
                return true;
            case Keys.NumPad9:
                _engine.InputDigit(9);
                UpdateDisplay();
                return true;
            case Keys.Decimal:
                _engine.InputDecimalPoint();
                UpdateDisplay();
                return true;
            case Keys.Add:
                _engine.SelectOperation(Operation.Add);
                UpdateDisplay();
                return true;
            case Keys.Subtract:
                _engine.SelectOperation(Operation.Subtract);
                UpdateDisplay();
                return true;
            case Keys.Multiply:
                _engine.SelectOperation(Operation.Multiply);
                UpdateDisplay();
                return true;
            case Keys.Divide:
                _engine.SelectOperation(Operation.Divide);
                UpdateDisplay();
                return true;
            default:
                return false;
        }
    }

    private void MainForm_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (HandleKey(e.KeyChar, Keys.None))
        {
            e.Handled = true;
        }
    }

    private void MainForm_KeyDown(object? sender, KeyEventArgs e)
    {
        if (HandleKey(null, e.KeyData))
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
    }

    protected override bool ProcessDialogKey(Keys keyData)
    {
        var keyCode = keyData & Keys.KeyCode;
        if ((keyData & Keys.Modifiers) == Keys.None && (keyCode == Keys.Enter || keyCode == Keys.Escape))
        {
            return false;
        }

        return base.ProcessDialogKey(keyData);
    }

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