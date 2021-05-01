using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataGYM.Models;
using DataAccess.DB;
using BusinessModel;
using DataGYM.Filtro;
using DataAccess.DB;

namespace DataGYM.Controllers
{
    public class AppointmentController : Controller
    {
        [Autoriza("Administrador,Cliente,Cajero")]
        public ActionResult Index()
        {


            List<APPOINTMENT_TABLE> appointments;

            using (DBContext<APPOINTMENT_TABLE> context = new DBContext<APPOINTMENT_TABLE>(new DataGYMEntities()))
            {
                appointments = context.genericBM.GetAll().ToList();
            }

            List<AppointmentViewModel> lista = new List<AppointmentViewModel>();

            foreach (var item in appointments)
            {
                lista.Add(this.Convertir(item));
            }

            return View(lista);
        }
        [Autoriza("Administrador,Cliente,Cajero")]
        public ActionResult Create()
        {
            List<SITE_TABLE> sites;
            AppointmentViewModel cita = new AppointmentViewModel();
            using (DBContext<SITE_TABLE> context = new DBContext<SITE_TABLE>(new DataGYMEntities()))
            {

                sites = context.genericBM.GetAll().ToList();
                
            }

            cita.lista_sites = sites;

            return View(cita);
        }

        [HttpPost]
        public ActionResult Create(AppointmentViewModel AppointmentViewModel)
        {

            
            APPOINTMENT_TABLE appointment = this.Convertir(AppointmentViewModel);

            using (DBContext<APPOINTMENT_TABLE> context = new DBContext<APPOINTMENT_TABLE>(new DataGYMEntities()))
            {
                appointment.CREATE_DATE = DateTime.Now;
                context.genericBM.Add(appointment);
                context.Complete();
            }

            return RedirectToAction("Index");
        }


        [Autoriza("Administrador,Cliente,Cajero")]
        public ActionResult Edit(int id)
        {
            List<SITE_TABLE> sites;
            AppointmentViewModel cita = new AppointmentViewModel();
            using (DBContext<SITE_TABLE> context = new DBContext<SITE_TABLE>(new DataGYMEntities()))
            {

                sites = context.genericBM.GetAll().ToList();

            }

            


            APPOINTMENT_TABLE appointment;

            using (DBContext<APPOINTMENT_TABLE> context = new DBContext<APPOINTMENT_TABLE>(new DataGYMEntities()))
            {
                appointment = context.genericBM.Get(id);

            }

            cita = this.Convertir(appointment);
            cita.lista_sites = sites;

            return View(cita);
        }


        [HttpPost]
        public ActionResult Edit(AppointmentViewModel AppointmentViewModel)
        {


            using (DBContext<APPOINTMENT_TABLE> context = new DBContext<APPOINTMENT_TABLE>(new DataGYMEntities()))
            {
                context.genericBM.Update(this.Convertir(AppointmentViewModel));
                context.Complete();
            }

            return RedirectToAction("Index");
        }
        [Autoriza("Administrador,Cliente")]
        public ActionResult Details(int id)
        {


            APPOINTMENT_TABLE appointment;
            using (DBContext<APPOINTMENT_TABLE> context = new DBContext<APPOINTMENT_TABLE>(new DataGYMEntities()))
            {
                appointment = context.genericBM.Get(id);

            }

            return View(this.Convertir(appointment));
        }
        [Autoriza("Administrador,Cliente")]
        public ActionResult Delete(int id)
        {


            APPOINTMENT_TABLE appointment;
            using (DBContext<APPOINTMENT_TABLE> context = new DBContext<APPOINTMENT_TABLE>(new DataGYMEntities()))
            {
                appointment = context.genericBM.Get(id);

            }

            return View(this.Convertir(appointment));
        }

        [HttpPost]
        public ActionResult Delete(AppointmentViewModel AppointmentViewModel)
        {


            using (DBContext<APPOINTMENT_TABLE> context = new DBContext<APPOINTMENT_TABLE>(new DataGYMEntities()))
            {
                context.genericBM.Remove(this.Convertir(AppointmentViewModel));
                context.Complete();
            }

            return RedirectToAction("Index");
        }

        #region Helper

        private AppointmentViewModel Convertir(APPOINTMENT_TABLE appointment)
        {
            AppointmentViewModel appointmentViewModel = new AppointmentViewModel
            {

                APPOINTMENT_ID = appointment.APPOINTMENT_ID,
                NAME = appointment.NAME,
                LASTNAME = appointment.LASTNAME,
                CONTACT_NUMBER = appointment.CONTACT_NUMBER,
                DATE_APPOINTMENT = appointment.DATE_APPOINTMENT,
                TIME_APPOINTMENT = appointment.TIME_APPOINTMENT,
                MESSAGE_APPOINTMENT = appointment.MESSAGE_APPOINTMENT,
                SITE_ID = appointment.SITE_ID,
                IS_ACTIVE = appointment.IS_ACTIVE.GetValueOrDefault()

            };
            return appointmentViewModel;
        }

        private APPOINTMENT_TABLE Convertir(AppointmentViewModel appointmentViewModel)
        {
            APPOINTMENT_TABLE appointment = new APPOINTMENT_TABLE
            {

                APPOINTMENT_ID = appointmentViewModel.APPOINTMENT_ID,
                NAME = appointmentViewModel.NAME,
                LASTNAME = appointmentViewModel.LASTNAME,
                CONTACT_NUMBER = appointmentViewModel.CONTACT_NUMBER,
                DATE_APPOINTMENT = appointmentViewModel.DATE_APPOINTMENT,
                TIME_APPOINTMENT = appointmentViewModel.TIME_APPOINTMENT,
                MESSAGE_APPOINTMENT = appointmentViewModel.MESSAGE_APPOINTMENT,
                SITE_ID = appointmentViewModel.SITE_ID,
                IS_ACTIVE = appointmentViewModel.IS_ACTIVE

            };
            return appointment;
        }

        #endregion
    }
}
