using calculator;
using System.Globalization;

namespace calculator.tests;

public class CalculatorTest
{
    private static readonly string TestDataPath = Path.Combine(AppContext.BaseDirectory, "TestCases.csv");

    [Theory]
    [MemberData(nameof(GetAddTestCases))]
    public void Add_ValidOperands_ReturnsSum(double firstOperand, double secondOperand, double expected)
    {
        double result = CalculatorOperations.Add(firstOperand, secondOperand);

        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(GetSubtractTestCases))]
    public void Subtract_ValidOperands_ReturnsDifference(double firstOperand, double secondOperand, double expected)
    {
        double result = CalculatorOperations.Subtract(firstOperand, secondOperand);

        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(GetMultiplyTestCases))]
    public void Multiply_ValidOperands_ReturnsProduct(double firstOperand, double secondOperand, double expected)
    {
        double result = CalculatorOperations.Multiply(firstOperand, secondOperand);

        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(GetDivideTestCases))]
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
    [MemberData(nameof(GetModuloTestCases))]
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
    [MemberData(nameof(GetExponentTestCases))]
    public void Exponent_ValidOperands_ReturnsPower(double firstOperand, double secondOperand, double expected)
    {
        double result = CalculatorOperations.Exponent(firstOperand, secondOperand);

        Assert.Equal(expected, result, precision: 10);
    }

    [Theory]
    [MemberData(nameof(GetCalculateTestCases))]
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

    [Fact]
    public void IsSupportedOperator_KnownOperators_ReturnTrue()
    {
        string[] supportedOperators = ["+", "-", "*", "/", "%", "^"];

        foreach (string operatorSymbol in supportedOperators)
        {
            Assert.True(CalculatorOperations.IsSupportedOperator(operatorSymbol));
        }
    }

    [Fact]
    public void IsSupportedOperator_UnknownOperators_ReturnFalse()
    {
        string?[] unsupportedOperators = [null, string.Empty, " ", "?"];

        foreach (string? operatorSymbol in unsupportedOperators)
        {
            Assert.False(CalculatorOperations.IsSupportedOperator(operatorSymbol));
        }
    }

    public static IEnumerable<object[]> GetAddTestCases() => GetOperationTestCases("Add");

    public static IEnumerable<object[]> GetSubtractTestCases() => GetOperationTestCases("Subtract");

    public static IEnumerable<object[]> GetMultiplyTestCases() => GetOperationTestCases("Multiply");

    public static IEnumerable<object[]> GetDivideTestCases() => GetOperationTestCases("Divide");

    public static IEnumerable<object[]> GetModuloTestCases() => GetOperationTestCases("Modulo");

    public static IEnumerable<object[]> GetExponentTestCases() => GetOperationTestCases("Exponent");

    public static IEnumerable<object[]> GetCalculateTestCases() => ReadTestCases()
        .Select(testCase => new object[]
        {
            testCase.Operand1,
            testCase.Operand2,
            GetOperatorSymbol(testCase.Operation),
            testCase.ExpectedResult
        })
        .ToList();

    private static IEnumerable<object[]> GetOperationTestCases(string operation) => ReadTestCases()
        .Where(testCase => testCase.Operation == operation)
        .Select(testCase => new object[] { testCase.Operand1, testCase.Operand2, testCase.ExpectedResult })
        .ToList();

    private static IEnumerable<TestCase> ReadTestCases()
    {
        if (!File.Exists(TestDataPath))
        {
            throw new FileNotFoundException($"Test data file not found: {TestDataPath}");
        }

        return File.ReadLines(TestDataPath)
            .Skip(1)
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(ParseTestCase)
            .ToList();
    }

    private static TestCase ParseTestCase(string line)
    {
        string[] values = line.Split(',');

        if (values.Length != 4)
        {
            throw new FormatException($"Invalid test case row: {line}");
        }

        return new TestCase
        {
            Operand1 = double.Parse(values[0], CultureInfo.InvariantCulture),
            Operand2 = double.Parse(values[1], CultureInfo.InvariantCulture),
            Operation = values[2],
            ExpectedResult = double.Parse(values[3], CultureInfo.InvariantCulture)
        };
    }

    private static string GetOperatorSymbol(string operation) => operation switch
    {
        "Add" => "+",
        "Subtract" => "-",
        "Multiply" => "*",
        "Divide" => "/",
        "Modulo" => "%",
        "Exponent" => "^",
        _ => throw new ArgumentException($"Unsupported operation '{operation}'.", nameof(operation))
    };

    private sealed class TestCase
    {
        public double Operand1 { get; init; }

        public double Operand2 { get; init; }

        public string Operation { get; init; } = string.Empty;

        public double ExpectedResult { get; init; }
    }
}
