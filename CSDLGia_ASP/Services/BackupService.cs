using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System;

namespace CSDLGia_ASP.Services 
{

    public class BackupService
    {
        private readonly string _connectionString;

        public BackupService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CSDLGia_ASPConnection");
        }

        public void BackupDatabase(string backupPath)
        {
            if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(backupPath)))
            {
                throw new Exception("Backup directory does not exist.");
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $"BACKUP DATABASE CSDLGIAKH_ASP_DEMO TO DISK = '{backupPath}'";

                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }


}





