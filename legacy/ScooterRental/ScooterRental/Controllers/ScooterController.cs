using ScooterRental.Dal;
using ScooterRental.Models;

using System;
using System.Web.Mvc;

namespace ScooterRental.Controllers
{
    public class ScooterController : Controller
    {
        private readonly ScooterDal scooterDal;
        private readonly MonitorDal monitorDal;

        public ScooterController()
        {
            this.scooterDal = new ScooterDal();
            this.monitorDal = new MonitorDal();
        }

        public ActionResult Rent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Rent(int scooterId, string passportNumber)
        {
            try
            {
                var scooter = this.scooterDal.Get(scooterId);
                scooter.Rent(passportNumber);

                this.scooterDal.Update(scooter);

                //Monitor
                var locationAreaId = new Random().Next(1, 200);
                monitorDal.Insert(new Monitor(scooter, locationAreaId));

                ViewBag.Alert = "The Scooted Was Rented";
            }
            catch (ApplicationException appEx)
            {
                ViewBag.Alert = appEx.Message;
            }
            return View();
        }

        public ActionResult TurnBack()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TurnBack(int scooterId)
        {
            try
            {
                var scooter = this.scooterDal.Get(scooterId);
                scooter.TurnBack();

                this.scooterDal.Update(scooter);

                //Monitor
                var locationAreaId = new Random().Next(1, 200);
                monitorDal.Insert(new Monitor(scooter, locationAreaId));

                ViewBag.Alert = "The Scooted Was Turned Back";
            }
            catch (ApplicationException appEx)
            {
                ViewBag.Alert = appEx.Message;
            }
            return View();
        }

        public ActionResult Check(int? scooterId = null)
        {
            try
            {
                if (scooterId != null)
                {
                    var monitor = this.monitorDal.GetByScooter(scooterId.GetValueOrDefault());

                    if (monitor == null)
                        ViewBag.Alert = "The Scooted Was Not Rented";

                    return View(monitor);
                }
            }
            catch (ApplicationException appEx)
            {
                ViewBag.Alert = appEx.Message;
            }
            return View();
        }
    }
}