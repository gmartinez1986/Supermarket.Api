using Dapper;
using Microsoft.Data.Sqlite;
using Supermarket.Api.Model;

namespace Supermarket.Api.Data
{
    public interface IReportsRepository
    {
        Task<IEnumerable<ReportProductSold>> GetReportProductsSold();
        Task<IEnumerable<ReportProductSold>> GetReporTopProducts();
    }

    public class ReportsRepository : IReportsRepository
    {
        private static string _connectionString;

        /// <summary>
        /// Reporte de productos vendidos por mes y marca.
        /// </summary>
        public async Task<IEnumerable<ReportProductSold>> GetReportProductsSold()
        {
            try
            {
                GetConnectionString();

                using (var connection = new SqliteConnection(_connectionString))
                {
                    //Se utiliza la función strftime para extraer el año y el mes de la fecha de compra (Date) en formato 'YYYY-MM'.
                    string query = @"SELECT
                                        strftime('%Y-%m', p.Date) AS Month,
                                        pr.Brand,
                                        SUM(CAST(p.Total AS DECIMAL)) AS TotalSold
                                    FROM
                                        Purchases p
                                        INNER JOIN Products pr ON p.IdProduct = pr.Id
                                    GROUP BY
                                        Month,
                                        pr.Brand
                                    ORDER BY
                                        Month,
                                        pr.Brand;";

                    return await connection.QueryAsync<ReportProductSold>(query);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        /// <summary>
        /// Reporte de los dos mejores productos vendidos por mes y marca, según la cantidad vendida.
        /// </summary>
        public async Task<IEnumerable<ReportProductSold>> GetReporTopProducts()
        {
            try
            {
                GetConnectionString();

                using (var connection = new SqliteConnection(_connectionString))
                {
                    //Se utiliza la función strftime para extraer el año y el mes de la fecha de compra (Date) en formato 'YYYY-MM'.
                    string query = @"WITH TopProducts AS (
                                        SELECT
                                            strftime('%Y-%m', p.Date) AS Month,
                                            pr.Brand,
                                            pr.Name AS ProductName,
                                            CAST(p.Total AS DECIMAL) AS TotalSold,
                                            ROW_NUMBER() OVER (PARTITION BY strftime('%Y-%m', p.Date), pr.Brand ORDER BY CAST(p.Total AS DECIMAL) DESC) AS RowNum
                                        FROM
                                        Purchases p
                                        INNER JOIN Products pr ON p.IdProduct = pr.Id
                                    )
                                    SELECT
                                        Month,
                                        Brand,
                                        ProductName,
                                        TotalSold
                                    FROM
                                        TopProducts
                                    WHERE
                                        RowNum <= 2
                                    ORDER BY
                                        Month,
                                        Brand,
                                        TotalSold DESC;";

                    return await connection.QueryAsync<ReportProductSold>(query);
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
