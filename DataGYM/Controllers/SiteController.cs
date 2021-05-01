using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataGYM.Models;
using DataAccess.DB;
using BusinessModel;
using DataGYM.Filtro;

namespace DataGYM.Controllers
{
    public class SiteController : Controller
    {
        [Autoriza("Administrador")]
        public ActionResult Index()
        {


            List<SITE_TABLE> sites;

            using (DBContext<SITE_TABLE> context = new DBContext<SITE_TABLE>(new DataGYMEntities()))
            {
                sites = context.genericBM.GetAll().ToList();
            }

            List<SiteViewModel> lista = new List<SiteViewModel>();

            foreach (var item in sites)
            {
                lista.Add(this.Convertir(item));
            }

            return View(lista);
        }
        [Autoriza("Administrador")]
        public ActionResult Create()
        {


            return View();
        }

        [HttpPost]
        public ActionResult Create(SiteViewModel SiteViewModel)
        {


            SITE_TABLE site = this.Convertir(SiteViewModel);

            using (DBContext<SITE_TABLE> context = new DBContext<SITE_TABLE>(new DataGYMEntities()))
            {

                context.genericBM.Add(site);
                context.Complete();
            }

            return RedirectToAction("Index");
        }


        [Autoriza("Administrador")]
        public ActionResult Edit(int id)
        {


            SITE_TABLE site;
            using (DBContext<SITE_TABLE> context = new DBContext<SITE_TABLE>(new DataGYMEntities()))
            {
                site = context.genericBM.Get(id);

            }

            SiteViewModel siteVM = this.Convertir(site);

            return View(siteVM);
        }


        [HttpPost]
        public ActionResult Edit(SiteViewModel SiteViewModel)
        {


            using (DBContext<SITE_TABLE> context = new DBContext<SITE_TABLE>(new DataGYMEntities()))
            {
                context.genericBM.Update(this.Convertir(SiteViewModel));
                context.Complete();
            }

            return RedirectToAction("Index");
        }
        [Autoriza("Administrador")]
        public ActionResult Details(int id)
        {


            SITE_TABLE site;
            using (DBContext<SITE_TABLE> context = new DBContext<SITE_TABLE>(new DataGYMEntities()))
            {
                site = context.genericBM.Get(id);

            }

            return View(this.Convertir(site));
        }
        [Autoriza("Administrador")]
        public ActionResult Delete(int id)
        {


            SITE_TABLE site;
            using (DBContext<SITE_TABLE> context = new DBContext<SITE_TABLE>(new DataGYMEntities()))
            {
                site = context.genericBM.Get(id);

            }

            return View(this.Convertir(site));
        }

        [HttpPost]
        public ActionResult Delete(SiteViewModel SiteViewModel)
        {


            using (DBContext<SITE_TABLE> context = new DBContext<SITE_TABLE>(new DataGYMEntities()))
            {
                context.genericBM.Remove(this.Convertir(SiteViewModel));
                context.Complete();
            }

            return RedirectToAction("Index");
        }

        #region Helper

        private SiteViewModel Convertir(SITE_TABLE site)
        {
            SiteViewModel siteViewModel = new SiteViewModel
            {

                SITE_ID = site.SITE_ID,
                LOCATION = site.LOCATION

            };
            return siteViewModel;
        }

        private SITE_TABLE Convertir(SiteViewModel siteViewModel)
        {
            SITE_TABLE site = new SITE_TABLE
            {

                SITE_ID = siteViewModel.SITE_ID,
                LOCATION = siteViewModel.LOCATION

            };
            return site;
        }

        #endregion
    }
}
