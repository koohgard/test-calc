namespace Test1.Calculator.Engine;

/// <summary>
/// Calculator engine that holds calculation state and display text.
/// </summary>
public sealed class CalculatorEngine
{
    private string _currentNumber = "0";

    /// <summary>
    /// Gets the text shown on the calculator display.
    /// </summary>
    public string DisplayText => _currentNumber;

    /// <summary>
    /// Appends a digit (0-9) to the current number, or replaces a leading zero.
    /// </summary>
    /// <param name="digit">The digit to input.</param>
    public void InputDigit(int digit)
    {
        if (digit < 0 || digit > 9)
        {
            return;
        }

        var digitChar = (char)('0' + digit);
        if (_currentNumber == "0")
        {
            _currentNumber = digitChar.ToString();
        }
        else
        {
            _currentNumber += digitChar;
        }
    }

    /// <summary>
    /// Appends a decimal point to the current number if one is not already present.
    /// </summary>
    public void InputDecimalPoint()
    {
        if (!_currentNumber.Contains('.'))
        {
            _currentNumber += '.';
        }
    }

    /// <summary>
    /// Removes the last character of the current number, or resets to zero.
    /// </summary>
    public void Delete()
    {
        if (_currentNumber.Length <= 1)
        {
            _currentNumber = "0";
        }
        else
        {
            _currentNumber = _currentNumber[..^1];
        }
    }

    /// <summary>
    /// Resets the engine to its initial state.
    /// </summary>
    public void Clear()
    {
        _currentNumber = "0";
    }
}