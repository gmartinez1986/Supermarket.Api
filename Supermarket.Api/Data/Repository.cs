using Dapper;
using Microsoft.Data.Sqlite;
using Supermarket.Api.Model;

namespace Supermarket.Api.Data
{
    public interface IRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(int Id);
    }

    public class Repository : IRepository
    {
        private static string _connectionString;

        /// <summary>
        /// Devuelve una lista de productos.
        /// </summary>
        public async Task<IEnumerable<Product>> GetProducts()
        {
            try
            {
                GetConnectionString();

                using (var connection = new SqliteConnection(_connectionString))
                {
                    string query = @"SELECT [Id]
                                           ,[Name]
                                           ,[Brand]
                                           ,[DateOfExpirity]
                                           ,[Price]
                                       FROM [Products]";

                    return await connection.QueryAsync<Product>(query);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task<Product> GetProduct(int Id)
        {
            try
            {
                GetConnectionString();

                using (var connection = new SqliteConnection(_connectionString))
                {
                    string query = @"SELECT [Id]
                                           ,[Name]
                                           ,[Brand]
                                           ,[DateOfExpirity]
                                           ,[Price]
                                       FROM [Products]
                                       WHERE Id = @Id";

                    var parameters = new { Id };

                    return await connection.QueryFirstOrDefaultAsync<Product>(query, parameters);
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
