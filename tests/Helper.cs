namespace tests;

using Dapper;
using Npgsql;

public class Helper
{
    public static readonly NpgsqlDataSource DataSource;

    static Helper()
    {
        var envVarKeyName = "pgconn";

        var rawConnectionString = Environment.GetEnvironmentVariable(envVarKeyName)!;
        if (rawConnectionString == null)
        {
            throw new Exception($@"YOUR CONN STRING PGCONN IS EMPTY.");
        }

        try
        {
            var uri = new Uri(rawConnectionString);
            var properlyFormattedConnectionString = string.Format(
                "Server={0};Database={1};User Id={2};Password={3};Port={4};Pooling=false;",
                uri.Host,
                uri.AbsolutePath.Trim('/'),
                uri.UserInfo.Split(':')[0],
                uri.UserInfo.Split(':')[1],
                uri.Port > 0 ? uri.Port : 5432);
            DataSource =
                new NpgsqlDataSourceBuilder(properlyFormattedConnectionString).Build();
            DataSource.OpenConnection().Close();
        }
        catch (Exception e)
        {
            throw new Exception($@"Your connection string is found, but could not be used.", e);
        }
    }


    public static void TriggerRebuild()
    {
        using (var conn = DataSource.OpenConnection())
        {
            try
            {
                conn.Execute(RebuildScript);
            }
            catch (Exception e)
            {
                throw new Exception($@"
THERE WAS AN ERROR REBUILDING THE DATABASE.", e);
            }
        }
    }


    public static string RebuildScript = $@"
DROP SCHEMA IF EXISTS calc CASCADE;

CREATE SCHEMA calc;

CREATE TABLE IF NOT EXISTS calc.history
(
 
    number1 DOUBLE PRECISION NOT NULL,
    number2 DOUBLE PRECISION NOT NULL,
    operation VARCHAR NOT NULL,
    result DOUBLE PRECISION NOT NULL
);

";
}