using Dapper;
using Microsoft.Data.Sqlite;
using Supermarket.Api.Model;

namespace Supermarket.Api.Data
{
    public interface IPurchasesRepository
    {
        Task<IEnumerable<Purchase>> GetPurchases();
        Task<Purchase> GetPurchase(int Id);
        Task<int> CreatePurchase(Purchase purchase);
        Task<int> EditPurchase(Purchase purchase);
        Task<int> DeletePurchase(int id);
    }

    public class PurchasesRepository : IPurchasesRepository
    {
        private static string _connectionString;

        /// <summary>
        /// Devuelve una lista de compras.
        /// </summary>
        public async Task<IEnumerable<Purchase>> GetPurchases()
        {
            try
            {
                GetConnectionString();

                using (var connection = new SqliteConnection(_connectionString))
                {
                    string query = @"SELECT Id, Date, IdProduct, Total
                                     FROM Purchases ;";

                    return await connection.QueryAsync<Purchase>(query);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        /// <summary>
        /// Devuelve una compra en particular a partir de su id.
        /// </summary>
        public async Task<Purchase> GetPurchase(int id)
        {
            try
            {
                GetConnectionString();

                using (var connection = new SqliteConnection(_connectionString))
                {
                    string query = @"SELECT Id, Date, IdProduct, Total
                                     FROM Purchases
                                     WHERE Id = @Id";

                    var parameters = new { Id = id };

                    return await connection.QueryFirstOrDefaultAsync<Purchase>(query, parameters);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        /// <summary>
        /// Crea una nueva compra.
        /// </summary>
        public async Task<int> CreatePurchase(Purchase purchase)
        {
            try
            {
                GetConnectionString();

                using (var connection = new SqliteConnection(_connectionString))
                {
                    var query = @"INSERT INTO Purchases (Date, IdProduct, Total)
                                  VALUES (@Date, @IdProduct, @Total);
                                  SELECT last_insert_rowid();";

                    return await connection.ExecuteScalarAsync<int>(query, purchase);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        /// <summary>
        /// Modificar una compra.
        /// </summary>
        public async Task<int> EditPurchase(Purchase purchase)
        {
            try
            {
                GetConnectionString();

                using (var connection = new SqliteConnection(_connectionString))
                {
                    var query = @"UPDATE Purchases
                                  SET Date = @Date, IdProduct = @IdProduct, Total = @Total
                                  WHERE Id = @Id";

                    return await connection.ExecuteAsync(query, purchase);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }


        /// <summary>
        /// Elimina una compra a partir de su id.
        /// </summary>
        public async Task<int> DeletePurchase(int id)
        {
            try
            {
                GetConnectionString();

                using (var connection = new SqliteConnection(_connectionString))
                {
                    var query = "DELETE FROM Purchases WHERE Id = @Id";

                    var parameters = new { Id = id };

                    return await connection.ExecuteAsync(query, parameters);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        private void GetConnectionString()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                _connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["MyDatabase"];
            }
        }
    }
}
