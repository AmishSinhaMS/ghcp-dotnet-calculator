using calculator;

namespace calculator.tests;

public class CalculatorTest
{
    [Theory]
    [InlineData(2, 3, 5)]
    [InlineData(-2, 3, 1)]
    [InlineData(0, 0, 0)]
    public void Add_ValidOperands_ReturnsSum(double firstOperand, double secondOperand, double expected)
    {
        double result = CalculatorOperations.Add(firstOperand, secondOperand);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(7, 3, 4)]
    [InlineData(-2, -3, 1)]
    [InlineData(0, 5, -5)]
    public void Subtract_ValidOperands_ReturnsDifference(double firstOperand, double secondOperand, double expected)
    {
        double result = CalculatorOperations.Subtract(firstOperand, secondOperand);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(4, 3, 12)]
    [InlineData(-4, 3, -12)]
    [InlineData(4, 0, 0)]
    public void Multiply_ValidOperands_ReturnsProduct(double firstOperand, double secondOperand, double expected)
    {
        double result = CalculatorOperations.Multiply(firstOperand, secondOperand);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(12, 3, 4)]
    [InlineData(7, 2, 3.5)]
    [InlineData(-12, 3, -4)]
    public void Divide_NonZeroDivisor_ReturnsQuotient(double firstOperand, double secondOperand, double expected)
    {
        double result = CalculatorOperations.Divide(firstOperand, secondOperand);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Divide_ZeroDivisor_ThrowsDivideByZeroException()
    {
        Assert.Throws<DivideByZeroException>(() => CalculatorOperations.Divide(10, 0));
    }

    [Theory]
    [InlineData(10, 3, 1)]
    [InlineData(12, 4, 0)]
    [InlineData(-10, 3, -1)]
    public void Modulo_NonZeroDivisor_ReturnsRemainder(double firstOperand, double secondOperand, double expected)
    {
        double result = CalculatorOperations.Modulo(firstOperand, secondOperand);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Modulo_ZeroDivisor_ThrowsDivideByZeroException()
    {
        Assert.Throws<DivideByZeroException>(() => CalculatorOperations.Modulo(10, 0));
    }

    [Theory]
    [InlineData(2, 3, 8)]
    [InlineData(5, 0, 1)]
    [InlineData(9, 0.5, 3)]
    public void Exponent_ValidOperands_ReturnsPower(double firstOperand, double secondOperand, double expected)
    {
        double result = CalculatorOperations.Exponent(firstOperand, secondOperand);

        Assert.Equal(expected, result, precision: 10);
    }

    [Theory]
    [InlineData(2, 3, "+", 5)]
    [InlineData(7, 4, "-", 3)]
    [InlineData(3, 5, "*", 15)]
    [InlineData(8, 2, "/", 4)]
    [InlineData(8, 3, "%", 2)]
    [InlineData(3, 2, "^", 9)]
    public void Calculate_SupportedOperator_ReturnsExpectedResult(
        double firstOperand,
        double secondOperand,
        string operatorSymbol,
        double expected)
    {
        double result = CalculatorOperations.Calculate(firstOperand, secondOperand, operatorSymbol);

        Assert.Equal(expected, result, precision: 10);
    }

    [Fact]
    public void Calculate_UnsupportedOperator_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => CalculatorOperations.Calculate(1, 2, "?"));
    }

    [Theory]
    [InlineData("+")]
    [InlineData("-")]
    [InlineData("*")]
    [InlineData("/")]
    [InlineData("%")]
    [InlineData("^")]
    public void IsSupportedOperator_KnownOperator_ReturnsTrue(string operatorSymbol)
    {
        Assert.True(CalculatorOperations.IsSupportedOperator(operatorSymbol));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("?")]
    public void IsSupportedOperator_UnknownOperator_ReturnsFalse(string? operatorSymbol)
    {
        Assert.False(CalculatorOperations.IsSupportedOperator(operatorSymbol));
    }
}
