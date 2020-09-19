using ScooterRental.Models;

using System;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace ScooterRental.Dal
{
    public class MonitorDal
    {
        private static SQLiteConnection sqliteConnection;
        private static string localFolder;
        private static string sqliteName = "ScooterRentalDB";

        public MonitorDal()
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
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Monitor(Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, ScooterId INT NOT NULL, LocationAreaId INT NOT NULL)";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Monitor GetByScooter(int scooterId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT Monitor.ScooterId, Monitor.LocationAreaId, Scooter.PassportNumber FROM Monitor inner join Scooter on Monitor.ScooterId = Scooter.Id Where Scooter.Id=" + scooterId + " AND Scooter.PassportNumber IS NOT NULL order by Monitor.Id desc LIMIT 1";
                    SQLiteDataAdapter da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        var row = dt.Rows[0];
                        return new Monitor(
                            new Scooter(
                                row.Field<int>("ScooterId"),
                                new User(row.Field<string>("PassportNumber"))),
                            row.Field<int>("LocationAreaId"));
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insert(Monitor monitor)
        {
            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    cmd.CommandText = "INSERT INTO Monitor (ScooterId, LocationAreaId) VALUES (@ScooterId, @LocationAreaId)";
                    cmd.Parameters.AddWithValue("@ScooterId", monitor.Scooter?.Id);
                    cmd.Parameters.AddWithValue("@LocationAreaId", monitor.LocationAreaId);
                    cmd.ExecuteNonQuery();
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}