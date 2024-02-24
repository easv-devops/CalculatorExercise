
using Dapper;
using Npgsql;

namespace Calculator;

public class Calc : ICalculator
{
    private NpgsqlConnection _connection;

    public Calc(NpgsqlConnection connection)
    {
        _connection = connection;
    }

    public double Add(double n1, double n2)
    {
        double result = n1 + n2;
        SaveToDatabase(n1, n2, "Addition", result);
        return result;
    }

    public double Subtract(double n1, double n2)
    {
        double result = n1 - n2;  
        SaveToDatabase(n1, n2, "Subtraction", result);  
        return result;
    }

    public double Multiply(double n1, double n2)
    {
        double result = n1 * n2;
        SaveToDatabase(n1, n2, "Multiplication", result);
        return result;
    }

    public double Divide(double n1, double n2)
    {
        if (n2 == 0)
        {
            Console.WriteLine("Error: Division by zero.");
            return 0;
        }

        double result = n1 / n2;
        SaveToDatabase(n1, n2, "Division", result);
        return result;
    }

    private void SaveToDatabase(double n1, double n2, string operation, double result)
    {
        var sql = @"
                INSERT INTO calc.History (number1, number2, operation, result)
                VALUES (@n1, @n2, @operation, @result)";

        _connection.Execute(sql, new { n1, n2, operation, result });
    }
}