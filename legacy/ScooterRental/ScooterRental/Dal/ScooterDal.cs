using ScooterRental.Models;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Web;

namespace ScooterRental.Dal
{
    public class ScooterDal
    {
        private static SQLiteConnection sqliteConnection;
        private static string localFolder;
        private static string sqliteName = "ScooterRentalDB";

        public ScooterDal()
        {
            localFolder = System.Web.Hosting.HostingEnvironment.MapPath("~/Dal/sqlite");

            if (!File.Exists(string.Format("{0}\\{1}.sqlite", localFolder, sqliteName)))
            {
                CreateDataBaseSQLite();
            }

            this.CreateTableSQlite();
        }

        private SQLiteConnection DbConnection()
        {
            sqliteConnection = new SQLiteConnection(string.Format("Data Source={0}\\{1}.sqlite; Version=3;", localFolder, sqliteName));
            sqliteConnection.Open();
            return sqliteConnection;
        }
        private void CreateDataBaseSQLite()
        {
            try
            {
                SQLiteConnection.CreateFile(string.Format("{0}\\{1}.sqlite", localFolder, sqliteName));
                this.CreateTableSQlite();
                this.SeedDataSQlite();
            }
            catch
            {
                throw;
            }
        }
        private void CreateTableSQlite()
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Scooter(Id INTEGER NOT NULL PRIMARY KEY, PassportNumber Varchar(50) NULL)";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SeedDataSQlite()
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Scooter (ID) VALUES (1)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Scooter (ID) VALUES (2)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Scooter (ID) VALUES (3)";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Scooter> GetAll()
        {
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Scooter";
                    SQLiteDataAdapter da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                }

                var scooters = new List<Scooter>();
                foreach (DataRow row in dt.Rows)
                {
                    var passportNumber = row.Field<string>("PassportNumber");

                    if (String.IsNullOrEmpty(passportNumber))
                    {
                        scooters.Add(new Scooter(
                            row.Field<long>("Id")));
                    }
                    else
                    {
                        scooters.Add(new Scooter(
                            row.Field<long>("Id"),
                            new User(passportNumber)));
                    }
                }

                return scooters;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Scooter Get(int id)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Scooter Where Id=" + id + " LIMIT 1";
                    SQLiteDataAdapter da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                }

                if (dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    
                    var passportNumber = row.Field<string>("PassportNumber");

                    if (String.IsNullOrEmpty(passportNumber))
                    {
                        return new Scooter(
                            row.Field<long>("Id"));
                    }
                    else
                    {
                        return new Scooter(
                            row.Field<long>("Id"),
                            new User(passportNumber));
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Scooter scooter)
        {
            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    if (scooter.Id != 0)
                    {
                        cmd.CommandText = "UPDATE Scooter SET PassportNumber=@PassportNumber WHERE Id=@Id";
                        cmd.Parameters.AddWithValue("@Id", scooter.Id);
                        cmd.Parameters.AddWithValue("@PassportNumber", scooter.User?.PassportNumber);
                        cmd.ExecuteNonQuery();
                    }
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}