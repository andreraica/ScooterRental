using System;
using Quartz;
using System.Configuration;
using System.Threading.Tasks;
using ScooterRental.Dal;

namespace ScooterRental.Job
{
    public class ExecuteTaskServiceCallJob : IJob
    {
        public static readonly string SchedulingStatus = ConfigurationManager.AppSettings["ExecuteTaskServiceCallSchedulingStatus"];

        public Task Execute(IJobExecutionContext context)
        {
            var task = Task.Run(() =>
            {
                if (SchedulingStatus.Equals("ON"))
                {
                    try
                    {
                        var monitorDal = new MonitorDal();
                        var scooterDal = new ScooterDal();

                        foreach (var scooter in scooterDal.GetAll())
                        {
                            var locationAreaId = new Random().Next(1, 200);

                            if(!scooter.Available)
                                monitorDal.Insert(new Models.Monitor(scooter, locationAreaId));
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            });
            return task;
        }
    }
}