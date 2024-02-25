using Calculator;
using Npgsql;

public class Program
{

    public static void Main(string[] args)
    {
    Console.WriteLine("Enter the first number:");
    double number1 = GetInput();
    Console.WriteLine("Enter the second number:");
    double number2 = GetInput();
    Console.WriteLine("Choose the operation (+, -, *, /):");
    string operation = GetOperationInput();

    string connectionString = Utilities.ProperlyFormattedConnectionString;
    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
    {
        connection.Open();

        Calc calculator = new Calc(connection);

        double result = PerformCalculation(calculator, number1, number2, operation);
        Console.WriteLine($"Result: {result}");
    }

    Console.ReadLine();
    
    static double GetInput()
    {
        double input;
        while (!double.TryParse(Console.ReadLine(), out input))
        {
            Console.WriteLine("Invalid input. Please enter a valid number:");
        }

        return input;
    }
    
    static string GetOperationInput()
    {
        string input;
        do
        {
            input = Console.ReadLine()?.Trim();
        } while (string.IsNullOrEmpty(input) || (input != "+" && input != "-" && input != "*" && input != "/"));

        return input;
    }

    static double PerformCalculation(Calc calculator, double number1, double number2, string operation)
    {
        switch (operation)
        {
            case "+":
                return calculator.Add(number1, number2);
            case "-":
                return calculator.Subtract(number1, number2);
            case "*":
                return calculator.Multiply(number1, number2);
            case "/":
                return calculator.Divide(number1, number2);
            default:
                Console.WriteLine("Invalid operation.");
                return 0;
        }

    }}
    }
