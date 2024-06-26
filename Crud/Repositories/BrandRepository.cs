using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Crud.Repositories
{
    public class BrandRepository
    {
        private readonly DbHelper _dbHelper;

        public BrandRepository(DbHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
        }

        public IEnumerable<Brand> GetBrands()
        {
            List<Brand> brands = new List<Brand>();
            DataTable dataTable = _dbHelper.ExecuteQuery("SELECT * FROM Brands");

            foreach (DataRow row in dataTable.Rows)
            {
                Brand brand = new Brand
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = row["Name"].ToString()
                };
                brands.Add(brand);
            }

            return brands;
        }

        public void AddBrand(Brand brand)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Name", SqlDbType.NVarChar) { Value = brand.Name }
            };

            _dbHelper.ExecuteProcedure("AddBrand", parameters);
        }

        public Brand GetBrandById(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", SqlDbType.Int) { Value = id }
            };

            DataTable dataTable = _dbHelper.ExecuteProcedure("GetBrandById", parameters);

            if (dataTable.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = dataTable.Rows[0];
            Brand brand = new Brand
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = row["Name"].ToString()
            };

            return brand;
        }

        public void UpdateBrand(Brand brand)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", SqlDbType.Int) { Value = brand.Id },
                new SqlParameter("@Name", SqlDbType.NVarChar) { Value = brand.Name }
            };

            _dbHelper.ExecuteProcedure("UpdateBrand", parameters);
        }

        public void DeleteBrand(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", SqlDbType.Int) { Value = id }
            };

            _dbHelper.ExecuteProcedure("DeleteBrand", parameters);
        }
    }
}