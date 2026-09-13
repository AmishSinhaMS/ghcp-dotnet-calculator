using calculator;

bool continueCalculating = true;

while (continueCalculating)
{
	Console.WriteLine("Basic Calculator");
	Console.WriteLine("Supported operators: +, -, *, /, %, ^");
	Console.WriteLine();

	double firstOperand = ReadNumber("Enter the first number: ");
	double secondOperand = ReadNumber("Enter the second number: ");
	string operatorSymbol = ReadOperator("Enter an operator: ");

	try
	{
		double result = CalculatorOperations.Calculate(firstOperand, secondOperand, operatorSymbol);
		Console.WriteLine($"Result: {firstOperand} {operatorSymbol} {secondOperand} = {result}");
	}
	catch (DivideByZeroException ex)
	{
		Console.WriteLine($"Cannot complete calculation: {ex.Message}");
	}
	catch (ArgumentException ex)
	{
		Console.WriteLine($"Cannot complete calculation: {ex.Message}");
	}

	Console.WriteLine();
	continueCalculating = AskToContinue();
}

Console.WriteLine("Goodbye.");

static double ReadNumber(string prompt)
{
	while (true)
	{
		Console.Write(prompt);
		string? input = Console.ReadLine();

		if (double.TryParse(input, out double number))
		{
			return number;
		}

		Console.WriteLine("Enter a valid number.");
	}
}

static string ReadOperator(string prompt)
{
	while (true)
	{
		Console.Write(prompt);
		string? operatorSymbol = Console.ReadLine();

		if (CalculatorOperations.IsSupportedOperator(operatorSymbol))
		{
			return operatorSymbol!;
		}

		Console.WriteLine("Enter one of these operators: +, -, *, /, %, ^.");
	}
}

static bool AskToContinue()
{
	while (true)
	{
		Console.Write("Perform another calculation? (y/n): ");
		string? response = Console.ReadLine();

		if (string.Equals(response, "y", StringComparison.OrdinalIgnoreCase) ||
			string.Equals(response, "yes", StringComparison.OrdinalIgnoreCase))
		{
			return true;
		}

		if (string.Equals(response, "n", StringComparison.OrdinalIgnoreCase) ||
			string.Equals(response, "no", StringComparison.OrdinalIgnoreCase))
		{
			return false;
		}

		Console.WriteLine("Enter y to continue or n to exit.");
	}
}
