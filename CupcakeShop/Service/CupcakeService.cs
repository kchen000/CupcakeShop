using CupcakeShop.Domain;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace CupcakeShop.Service
{
    public class CupcakeService : ICupcakeService
    {

        public Cupcake GetCupcakeById(int id)
        {
            return GetAllCupcakes().SingleOrDefault(x => x.Id == id);
        }

        public IList<Cupcake> GetAllCupcakes()
        {
            IList<Cupcake> cupcakes = new List<Cupcake>();
            var connection = createConnection();
            connection.Open();
            SqlCommand command = new SqlCommand()
            {
                CommandText = "SELECT * FROM Cupcake",
                Connection = connection
            };
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetOrdinal("Id");
                int name = reader.GetOrdinal("Name");
                int price = reader.GetOrdinal("Price");
                int ingredients = reader.GetOrdinal("Ingredients");
                int createdOn = reader.GetOrdinal("CreatedOn");
                int lastModified = reader.GetOrdinal("LastModified");
                int imageFileName = reader.GetOrdinal("ImageFileName");
                int featured = reader.GetOrdinal("Featured");
                cupcakes.Add(new Cupcake()
                {
                    Id = reader.GetInt32(id),
                    Name = reader.GetString(name),
                    Price = reader.GetDecimal(price),
                    Ingredients = reader.GetString(ingredients),
                    CreatedOn = reader.GetDateTime(createdOn),
                    LastModified = reader.GetDateTime(lastModified),
                    ImageFileName = reader.GetString(imageFileName),
                    Featured = reader.GetBoolean(featured),
                });
            }
            connection.Close();
            return cupcakes;
        }

        public void InsertCupcake(Cupcake cupcake)
        {
            var connection = createConnection();
            connection.Open();
            SqlCommand command = new SqlCommand()
            {
                CommandText = "INSERT INTO Cupcake VALUES (@Name, @Price, @CreatedOn, @LastModified, @Ingredients, @ImageFileName, @Featured)",
                Connection = connection
            };
            SqlParameter priceParemeter = new SqlParameter()
            {
                ParameterName = "Price",
                DbType = System.Data.DbType.Decimal,
                Value = cupcake.Price,
                SqlDbType = System.Data.SqlDbType.Decimal,
                SqlValue = cupcake.Price
            };
            command.Parameters.AddWithValue("Name", cupcake.Name);
            command.Parameters.Add(priceParemeter);
            command.Parameters.AddWithValue("CreatedOn", cupcake.CreatedOn);
            command.Parameters.AddWithValue("LastModified", cupcake.LastModified);
            command.Parameters.AddWithValue("Ingredients", cupcake.Ingredients);
            command.Parameters.AddWithValue("ImageFileName", cupcake.ImageFileName == null ? "" : cupcake.ImageFileName);
            command.Parameters.AddWithValue("Featured", cupcake.Featured);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void UpdateCupcake(Cupcake cupcake)
        {
            //create connection string and update cupcake
            var connection = createConnection();
            connection.Open();
            SqlCommand command = new SqlCommand()
            {
                CommandText = "UPDATE Cupcake SET Name = @Name, Price = @Price, LastModified = @LastModified, Ingredients = @Ingredients, ImageFileName = @ImageFileName, Featured = @Featured WHERE Id = @Id",
                Connection = connection
            };

            SqlParameter priceParemeter = new SqlParameter()
            {
                ParameterName = "Price",
                DbType = System.Data.DbType.Decimal,
                Value = cupcake.Price,
                SqlDbType = System.Data.SqlDbType.Decimal,
                SqlValue = cupcake.Price
            };
            command.Parameters.AddWithValue("Name", cupcake.Name);
            command.Parameters.Add(priceParemeter);
            command.Parameters.AddWithValue("LastModified", cupcake.LastModified);
            command.Parameters.AddWithValue("Id", cupcake.Id);
            command.Parameters.AddWithValue("Ingredients", cupcake.Ingredients);
            command.Parameters.AddWithValue("ImageFileName", cupcake.ImageFileName);
            command.Parameters.AddWithValue("Featured", cupcake.Featured);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void DeleteCupcake(Cupcake cupcake)
        {
            //create connection string and delete cupcake
            var connection = createConnection();
            connection.Open();
            SqlCommand command = new SqlCommand()
            {
                CommandText = "DELETE Cupcake WHERE Id = @Id",
                Connection = connection
            };
            command.Parameters.AddWithValue("Id", cupcake.Id);
            command.ExecuteNonQuery();
            connection.Close();
        }
        private SqlConnection createConnection()
        {
            return new SqlConnection()
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["CupcakeShop"].ConnectionString
            };
        }
    }
}