namespace ScooterRental.Infrastructure.Data.Repositories
{
    using System;
    using System.Data;
    using System.Data.SQLite;
    using System.IO;

    using Microsoft.Extensions.Hosting;

    using ScooterRental.Domain.Interfaces.Data.Repositories;
    using ScooterRental.Domain.Models;

    public class TrackingRepository : ITrackingRepository
    {
        private static SQLiteConnection sqliteConnection;
        private static string localFolder;
        private static string sqliteName = "ScooterRentalDB";

        public TrackingRepository(IHostingEnvironment host)
        {
            localFolder = host.ContentRootPath + "\\Data";

            if (!Directory.Exists(localFolder))
            {
                Directory.CreateDirectory(localFolder);
            }

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
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Tracking(Id INTEGER NOT NULL PRIMARY KEY, ScooterId INT NOT NULL, LocationId INT NOT NULL)";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tracking GetByScooter(int scooterId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Tracking Where ScooterId=" + scooterId + " order by Id desc LIMIT 1";
                    SQLiteDataAdapter da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                }

                if (dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];

                    var trackingId = row.Field<long>("Id");
                    scooterId = row.Field<int>("ScooterId");
                    var locationId = row.Field<int>("LocationId");

                    return new Tracking(trackingId, scooterId, locationId);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insert(Tracking tracking)
        {
            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    cmd.CommandText = "Insert into Tracking (ScooterId, LocationId) VALUES (@ScooterId, @LocationId)";
                    cmd.Parameters.AddWithValue("@ScooterId", tracking.ScooterId);
                    cmd.Parameters.AddWithValue("@LocationId", tracking.LocationId);
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
