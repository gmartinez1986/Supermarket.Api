using Microsoft.Data.Sqlite;
using Dapper;

public class DataBaseContext
{
    private readonly string connectionString;

    public DataBaseContext(string databasePath)
    {
        connectionString = $"{databasePath}";
    }

    public void CreateTableProducts()
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            var createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Products (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name VARCHAR(50),
                    Brand VARCHAR(50),
                    DateOfExpirity DATETIME,
                    Price VARCHAR(50)
                );";

            connection.Execute(createTableQuery);

            //Verificar si la tabla está vacía.
            var countQuery = "SELECT COUNT(*) FROM Products";
            var count = connection.ExecuteScalar<int>(countQuery);

            if (count == 0)
            {
                //Agregar datos de prueba.
                var insertQuery = @"
                    INSERT INTO Products (Name, Brand, DateOfExpirity, Price)
                    VALUES
                        ('Product 1', 'Brand 1', '2023-08-30', 19.99),
                        ('Product 2', 'Brand 2', '2023-09-15', 29.99),
                        ('Product 3', 'Brand 3', '2023-10-10', 39.99);";

                connection.Execute(insertQuery);
            }
        }
    }

    public void CreateTablePurchases()
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            var createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Purchases (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Date DATETIME,
                    IdProduct INTEGER,
                    Total VARCHAR(50),
                    FOREIGN KEY (IdProduct) REFERENCES Products (Id)
                );";

            connection.Execute(createTableQuery);

            //Verificar si la tabla está vacía.
            var countQuery = "SELECT COUNT(*) FROM Purchases";
            var count = connection.ExecuteScalar<int>(countQuery);

            if (count == 0)
            {
                //Agregar datos de prueba.
                var insertQuery = @"
                    INSERT INTO Purchases (Date, IdProduct, Total)
                    VALUES 
                        ('2023-06-15', 1, '100.50'),
                        ('2023-06-16', 2, '50.75'),
                        ('2023-06-17', 1, '75.20');";

                connection.Execute(insertQuery);
            }
        }
    }
}