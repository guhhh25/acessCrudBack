namespace Crud.Repositories
{
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class CarRepository
    {
        private readonly DbHelper _dbHelper;
        private readonly string _connectionString;
        public CarRepository(DbHelper dbHelper, IConfiguration configuration)
        {
            _dbHelper = dbHelper;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void InsertCar(Car car)
        {
            string procedureName = "InsertCar";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@BrandId", car.BrandId),
        new SqlParameter("@Model", car.Model),
        new SqlParameter("@Year", car.Year),
        new SqlParameter("@Color", (object)car.Color ?? DBNull.Value)
            };

            _dbHelper.ExecuteProcedure(procedureName, parameters);
        }

        public void UpdateCar(Car car)
        {
            string procedureName = "UpdateCar";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Id", car.Id),
        new SqlParameter("@BrandId", car.BrandId),
        new SqlParameter("@Model", car.Model),
        new SqlParameter("@Year", car.Year),
        new SqlParameter("@Color", (object)car.Color ?? DBNull.Value)
            };

            _dbHelper.ExecuteProcedure(procedureName, parameters);
        }

        public void DeleteCar(int id)
        {
            string procedureName = "DeleteCar";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Id", id)
            };

            _dbHelper.ExecuteProcedure(procedureName, parameters);
        }

        public Car GetCarById(int id)
        {
            DataTable dataTable = _dbHelper.ExecuteQuery($"SELECT * FROM Cars WHERE Id = {id}");

            if (dataTable.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = dataTable.Rows[0];
            Car car = new Car
            {
                Id = Convert.ToInt32(row["Id"]),
                BrandId = Convert.ToInt32(row["BrandId"]),
                Color = row["Color"].ToString(),
                Year = Convert.ToInt32(row["Year"]),
                Model = row["Model"].ToString()

            };

            return car;
        }

        public List<Car> GetAllCars()
        {
            List<Car> cars = new List<Car>();
            DataTable dataTable = _dbHelper.ExecuteQuery("SELECT c.Id, c.Model, c.Year, c.Color, b.Name as BrandName FROM Cars c JOIN Brands b ON c.BrandId = b.Id");

            foreach (DataRow row in dataTable.Rows)
            {
                Car car = new Car
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Model = row["Model"].ToString(),
                    Year = Convert.ToInt32(row["Year"]),
                    Color = row["Color"].ToString(),
                    BrandName = row["BrandName"].ToString()
                };
                cars.Add(car);
            }

            return cars;
        }
    }
}
