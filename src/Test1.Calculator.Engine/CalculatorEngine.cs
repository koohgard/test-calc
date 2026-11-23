using System.Globalization;

namespace Test1.Calculator.Engine;

/// <summary>
/// Calculator engine that holds calculation state and display text.
/// </summary>
public sealed class CalculatorEngine
{
    private string _currentNumber = "0";
    private decimal _firstNumber;
    private Operation? _pendingOperation;
    private bool _newEntry;
    private bool _secondNumberEntered;
    private bool _resultShown;
    private bool _errorState;

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

        if (_errorState)
        {
            ResetAllState();
            _currentNumber = ((char)('0' + digit)).ToString();
            return;
        }

        var digitChar = (char)('0' + digit);

        if (_resultShown)
        {
            ResetCalculationState();
            _currentNumber = digitChar.ToString();
            return;
        }

        if (_newEntry)
        {
            _currentNumber = digitChar.ToString();
            _newEntry = false;
            if (_pendingOperation.HasValue)
            {
                _secondNumberEntered = true;
            }

            return;
        }

        if (CountSignificantDigits(_currentNumber) >= 28)
        {
            return;
        }

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
        if (_errorState)
        {
            return;
        }

        if (_resultShown)
        {
            ResetCalculationState();
            _currentNumber = "0.";
            return;
        }

        if (_newEntry)
        {
            _currentNumber = "0.";
            _newEntry = false;
            if (_pendingOperation.HasValue)
            {
                _secondNumberEntered = true;
            }

            return;
        }

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
        if (_errorState || _resultShown)
        {
            return;
        }

        if (_newEntry && _pendingOperation.HasValue)
        {
            return;
        }

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
        ResetAllState();
        _currentNumber = "0";
    }

    /// <summary>
    /// Selects an operation and stores the current or result value as the first operand when needed.
    /// </summary>
    /// <param name="op">The operation to select.</param>
    public void SelectOperation(Operation op)
    {
        if (_errorState)
        {
            return;
        }

        if (!Enum.IsDefined(op))
        {
            return;
        }

        if (_pendingOperation.HasValue && _secondNumberEntered)
        {
            if (!TryApplyOperation(_firstNumber, _pendingOperation.Value, ParseCurrentNumber(), out var chained))
            {
                return;
            }

            _firstNumber = chained;
            _currentNumber = FormatResult(chained);
            _pendingOperation = op;
            _newEntry = true;
            _secondNumberEntered = false;
            _resultShown = false;
            return;
        }

        if (_pendingOperation.HasValue)
        {
            _pendingOperation = op;
            return;
        }

        _firstNumber = ParseCurrentNumber();
        _pendingOperation = op;
        _newEntry = true;
        _secondNumberEntered = false;
        _resultShown = false;
        _currentNumber = FormatResult(_firstNumber);
    }

    /// <summary>
    /// Calculates the pending operation or formats the current value as the result.
    /// </summary>
    public void Calculate()
    {
        if (_errorState)
        {
            return;
        }

        if (_pendingOperation.HasValue && _secondNumberEntered)
        {
            if (!TryApplyOperation(_firstNumber, _pendingOperation.Value, ParseCurrentNumber(), out var result))
            {
                return;
            }

            _currentNumber = FormatResult(result);
            _pendingOperation = null;
            _secondNumberEntered = false;
            _resultShown = true;
            _newEntry = true;
            return;
        }

        if (_pendingOperation.HasValue)
        {
            _currentNumber = FormatResult(_firstNumber);
            _pendingOperation = null;
            _secondNumberEntered = false;
            _resultShown = true;
            _newEntry = true;
            return;
        }

        var formatted = FormatResult(ParseCurrentNumber());
        _currentNumber = formatted;
        _resultShown = true;
        _newEntry = true;
    }

    private bool TryApplyOperation(decimal first, Operation op, decimal second, out decimal result)
    {
        result = 0;
        try
        {
            result = op switch
            {
                Operation.Add => first + second,
                Operation.Subtract => first - second,
                Operation.Multiply => first * second,
                Operation.Divide => first / second,
                _ => throw new InvalidOperationException(),
            };
            return true;
        }
        catch (DivideByZeroException)
        {
            EnterError("Cannot divide by zero");
            return false;
        }
        catch (OverflowException)
        {
            EnterError("Result is too large");
            return false;
        }
    }

    private void EnterError(string message)
    {
        _errorState = true;
        _currentNumber = message;
        _pendingOperation = null;
        _secondNumberEntered = false;
        _newEntry = false;
        _resultShown = false;
    }

    private decimal ParseCurrentNumber()
    {
        return decimal.Parse(
            _currentNumber,
            NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
            CultureInfo.InvariantCulture);
    }

    private static string FormatResult(decimal value)
    {
        if (value == 0m)
        {
            return "0";
        }

        var text = value.ToString(CultureInfo.InvariantCulture);
        if (text.Contains('.'))
        {
            text = text.TrimEnd('0').TrimEnd('.');
        }

        if (text == "-0")
        {
            return "0";
        }

        return text;
    }

    private static int CountSignificantDigits(string text)
    {
        if (!text.Contains('.'))
        {
            return text.Count(char.IsDigit);
        }

        var dotIndex = text.IndexOf('.');
        var integerPart = text[..dotIndex];
        var fractionalPart = text[(dotIndex + 1)..];
        var count = fractionalPart.Count(char.IsDigit);
        if (integerPart is not ("0" or ""))
        {
            count += integerPart.Count(char.IsDigit);
        }

        return count;
    }

    private void ResetCalculationState()
    {
        _firstNumber = 0;
        _pendingOperation = null;
        _newEntry = false;
        _secondNumberEntered = false;
        _resultShown = false;
    }

    private void ResetAllState()
    {
        ResetCalculationState();
        _errorState = false;
    }
}