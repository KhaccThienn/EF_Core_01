using EF_Core_01.Models;

namespace EF_Core_01.Services
{
    public class StoredProcedureServices
    {
        private readonly ModelContext                 _context;
        private readonly IConfiguration               _config;
        private readonly string                       _con;
        public StoredProcedureServices(IConfiguration config)
        {
            _context  = new ModelContext();
            _config   = config;
            _con      = _config.GetConnectionString("OrclDb");
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(int? categoryId)
        {
            List<Product> products = new List<Product>();

            using (OracleConnection conn = new OracleConnection(_con))
            {
                await conn.OpenAsync();

                using (OracleCommand cmd = new OracleCommand(Constant.GETALLPRODUCT, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (categoryId.HasValue)
                    {
                        cmd.Parameters.Add(new OracleParameter(Constant.P_CATEGORYID, OracleDbType.Int32, categoryId, ParameterDirection.Input));
                    }

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Product product = new Product
                            {
                                Id         = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name       = reader.GetString(reader.GetOrdinal("Name")),
                                Price      = reader.GetDecimal(reader.GetOrdinal("Price")),
                                Status     = reader.GetInt32(reader.GetOrdinal("Status")),
                                Categoryid = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                                Createdate = reader.GetDateTime(reader.GetOrdinal("CreateDate")),

                            };
                            products.Add(product);
                        }
                    }
                }
            }

            return products;
        }

        public async void DeleteProductById(int? id)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            if (id.HasValue)
            {
                cmd.Parameters.Add(new OracleParameter(Constant.P_ID, OracleDbType.Int32, id, ParameterDirection.Input));
            }
            await ExecuteQueryAsync(Constant.DELETEPRODUCT, cmd);
        }

        public async void InsertProduct(Product product)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.CommandType   = CommandType.StoredProcedure;

            cmd.Parameters.Add(new OracleParameter(Constant.P_NAME, OracleDbType.Varchar2, product.Name, ParameterDirection.Input));
            cmd.Parameters.Add(new OracleParameter(Constant.P_PRICE, OracleDbType.Decimal, product.Price, ParameterDirection.Input));
            cmd.Parameters.Add(new OracleParameter(Constant.P_STATUS, OracleDbType.Int32, product.Status, ParameterDirection.Input));
            cmd.Parameters.Add(new OracleParameter(Constant.P_CATEGORYID, OracleDbType.Int32, product.Categoryid, ParameterDirection.Input));
            cmd.Parameters.Add(new OracleParameter(Constant.P_CREATEDATE, OracleDbType.Date, product.Createdate, ParameterDirection.Input));
            //await cmd.ExecuteNonQueryAsync();
            await ExecuteQueryAsync(Constant.INSERTPRODUCT, cmd);
        }

        private async Task ExecuteQueryAsync(string spName, OracleCommand cmd)
        {
            using (OracleConnection conn = new OracleConnection(_con))  // Your connection string
            {
                await conn.OpenAsync();

                // Assign the connection to the command
                cmd.Connection = conn;

                // Set the stored procedure name
                cmd.CommandText = spName;

                cmd.CommandType = CommandType.StoredProcedure;
                await cmd.ExecuteNonQueryAsync();
            }
        }

    }
}
