namespace calculator;

/// <summary>
/// Provides arithmetic operations for the calculator application.
/// </summary>
public static class CalculatorOperations
{
    /// <summary>
    /// Adds two numbers.
    /// </summary>
    /// <param name="firstOperand">The first addend.</param>
    /// <param name="secondOperand">The second addend.</param>
    /// <returns>The sum of the two operands.</returns>
    public static double Add(double firstOperand, double secondOperand) => firstOperand + secondOperand;

    /// <summary>
    /// Subtracts one number from another.
    /// </summary>
    /// <param name="firstOperand">The minuend.</param>
    /// <param name="secondOperand">The subtrahend.</param>
    /// <returns>The difference between the two operands.</returns>
    public static double Subtract(double firstOperand, double secondOperand) => firstOperand - secondOperand;

    /// <summary>
    /// Multiplies two numbers.
    /// </summary>
    /// <param name="firstOperand">The first factor.</param>
    /// <param name="secondOperand">The second factor.</param>
    /// <returns>The product of the two operands.</returns>
    public static double Multiply(double firstOperand, double secondOperand) => firstOperand * secondOperand;

    /// <summary>
    /// Divides one number by another.
    /// </summary>
    /// <param name="firstOperand">The dividend.</param>
    /// <param name="secondOperand">The divisor.</param>
    /// <returns>The quotient of the two operands.</returns>
    /// <exception cref="DivideByZeroException">Thrown when <paramref name="secondOperand" /> is zero.</exception>
    public static double Divide(double firstOperand, double secondOperand)
    {
        if (secondOperand == 0)
        {
            throw new DivideByZeroException("Division by zero is not allowed.");
        }

        return firstOperand / secondOperand;
    }

    /// <summary>
    /// Calculates the remainder after division.
    /// </summary>
    /// <param name="firstOperand">The dividend.</param>
    /// <param name="secondOperand">The divisor.</param>
    /// <returns>The remainder after division.</returns>
    /// <exception cref="DivideByZeroException">Thrown when <paramref name="secondOperand" /> is zero.</exception>
    public static double Modulo(double firstOperand, double secondOperand)
    {
        if (secondOperand == 0)
        {
            throw new DivideByZeroException("Modulo by zero is not allowed.");
        }

        return firstOperand % secondOperand;
    }

    /// <summary>
    /// Raises one number to the power of another.
    /// </summary>
    /// <param name="firstOperand">The base value.</param>
    /// <param name="secondOperand">The exponent.</param>
    /// <returns>The exponentiation result.</returns>
    public static double Exponent(double firstOperand, double secondOperand) => Math.Pow(firstOperand, secondOperand);

    /// <summary>
    /// Calculates a result from two operands and an operator symbol.
    /// </summary>
    /// <param name="firstOperand">The first operand.</param>
    /// <param name="secondOperand">The second operand.</param>
    /// <param name="operatorSymbol">The arithmetic operator.</param>
    /// <returns>The calculated result.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="operatorSymbol" /> is unsupported.</exception>
    /// <exception cref="DivideByZeroException">Thrown for division or modulo by zero.</exception>
    public static double Calculate(double firstOperand, double secondOperand, string operatorSymbol) =>
        operatorSymbol switch
        {
            "+" => Add(firstOperand, secondOperand),
            "-" => Subtract(firstOperand, secondOperand),
            "*" => Multiply(firstOperand, secondOperand),
            "/" => Divide(firstOperand, secondOperand),
            "%" => Modulo(firstOperand, secondOperand),
            "^" => Exponent(firstOperand, secondOperand),
            _ => throw new ArgumentException($"Unsupported operator '{operatorSymbol}'.", nameof(operatorSymbol))
        };

    /// <summary>
    /// Determines whether an operator symbol is supported by the calculator.
    /// </summary>
    /// <param name="operatorSymbol">The operator symbol to validate.</param>
    /// <returns><see langword="true" /> when the operator is supported; otherwise, <see langword="false" />.</returns>
    public static bool IsSupportedOperator(string? operatorSymbol) =>
        operatorSymbol is "+" or "-" or "*" or "/" or "%" or "^";
}