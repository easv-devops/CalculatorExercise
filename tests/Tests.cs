using Calculator;
using Dapper;
using FluentAssertions;
using Npgsql;

namespace tests
{
    [TestFixture]
    public class Tests
    {
        private NpgsqlConnection _connection;
        private Calc _calculator;

        [SetUp]
        public void Setup()
        {
            _connection = new NpgsqlConnection(Utilities.ProperlyFormattedConnectionString);
            _connection.Open();
            _calculator = new Calc(_connection);
        }

        [TearDown]
        public void Teardown()
        {
            _connection.Close();
        }

        [Test]
        public void Add_ShouldReturnCorrectResult()
        {
            // ARRANGE
            Helper.TriggerRebuild();
            double number1 = 5;
            double number2 = 3;
            double expected = 8;

            // ACT
            var result = _calculator.Add(number1, number2);

            // ASSERT
            using (var conn = new NpgsqlConnection(Utilities.ProperlyFormattedConnectionString))
            {
                conn.Open();

                Results insertedRecord = conn.QueryFirst<Results>(
                    "SELECT number1, number2, operation, result FROM calc.history");

                insertedRecord.Should().NotBeNull();
                insertedRecord.Number1.Should().Be(number1);
                insertedRecord.Number2.Should().Be(number2);
                insertedRecord.Operation.Should().Be("Addition");
                insertedRecord.Result.Should().Be(expected);
            }
        }
        
        
        [Test]
        public void Subtract_ShouldReturnCorrectResult()
        {
            // ARRANGE
            Helper.TriggerRebuild();
            double number1 = 8;
            double number2 = 3;
            double expected = 5;

            // ACT
            var result = _calculator.Subtract(number1, number2);

            // ASSERT
            using (var conn = new NpgsqlConnection(Utilities.ProperlyFormattedConnectionString))
            {
                conn.Open();

                Results insertedRecord = conn.QueryFirst<Results>(
                    "SELECT number1, number2, operation, result FROM calc.history");

                insertedRecord.Should().NotBeNull();
                insertedRecord.Number1.Should().Be(number1);
                insertedRecord.Number2.Should().Be(number2);
                insertedRecord.Operation.Should().Be("Subtraction");
                insertedRecord.Result.Should().Be(expected);
            }
        }
        
        [Test]
        public void Multiply_ShouldReturnCorrectResult()
        {
            // ARRANGE
            Helper.TriggerRebuild();
            double number1 = 4;
            double number2 = 3;
            double expected = 12;

            // ACT
            var result = _calculator.Multiply(number1, number2);

            // ASSERT
            using (var conn = new NpgsqlConnection(Utilities.ProperlyFormattedConnectionString))
            {
                conn.Open();

                Results insertedRecord = conn.QueryFirst<Results>(
                    "SELECT number1, number2, operation, result FROM calc.history");

                insertedRecord.Should().NotBeNull();
                insertedRecord.Number1.Should().Be(number1);
                insertedRecord.Number2.Should().Be(number2);
                insertedRecord.Operation.Should().Be("Multiplication");
                insertedRecord.Result.Should().Be(expected);
            }
        }

        [Test]
        public void Divide_ShouldReturnCorrectResult()
        {
            // ARRANGE
            Helper.TriggerRebuild();
            double number1 = 15;
            double number2 = 3;
            double expected = 5;

            // ACT
            var result = _calculator.Divide(number1, number2);

            // ASSERT
            using (var conn = new NpgsqlConnection(Utilities.ProperlyFormattedConnectionString))
            {
                conn.Open();

                Results insertedRecord = conn.QueryFirst<Results>(
                    "SELECT number1, number2, operation, result FROM calc.history");

                insertedRecord.Should().NotBeNull();
                insertedRecord.Number1.Should().Be(number1);
                insertedRecord.Number2.Should().Be(number2);
                insertedRecord.Operation.Should().Be("Division");
                insertedRecord.Result.Should().Be(expected);
            }
        }
        
    }
}