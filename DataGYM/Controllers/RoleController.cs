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
    public class RoleController : Controller
    {
        [Autoriza("Administrador")]
        public ActionResult Index()
        {


            List<ROLE_TABLE> roles;

            using (DBContext<ROLE_TABLE> context = new DBContext<ROLE_TABLE>(new DataGYMEntities()))
            {
                roles = context.genericBM.GetAll().ToList();
            }

            List<RoleViewModel> lista = new List<RoleViewModel>();

            foreach (var item in roles)
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
        public ActionResult Create(RoleViewModel RoleViewModel)
        {
            

            ROLE_TABLE rol = this.Convertir(RoleViewModel);

            using (DBContext<ROLE_TABLE> context = new DBContext<ROLE_TABLE>(new DataGYMEntities()))
            {

                context.genericBM.Add(rol);
                context.Complete();
            }

            return RedirectToAction("Index");
        }


        [Autoriza("Administrador")]
        public ActionResult Edit(int id)
        {
            

            ROLE_TABLE rol;
            using (DBContext<ROLE_TABLE> context = new DBContext<ROLE_TABLE>(new DataGYMEntities()))
            {
                rol = context.genericBM.Get(id);

            }

            RoleViewModel rolVM = this.Convertir(rol);

            return View(rolVM);
        }


        [HttpPost]
        public ActionResult Edit(RoleViewModel RoleViewModel)
        {
            

            using (DBContext<ROLE_TABLE> context = new DBContext<ROLE_TABLE>(new DataGYMEntities()))
            {
                context.genericBM.Update(this.Convertir(RoleViewModel));
                context.Complete();
            }

            return RedirectToAction("Index");
        }
        [Autoriza("Administrador")]
        public ActionResult Details(int id)
        {
            

            ROLE_TABLE rol;
            using (DBContext<ROLE_TABLE> context = new DBContext<ROLE_TABLE>(new DataGYMEntities()))
            {
                rol = context.genericBM.Get(id);

            }

            return View(this.Convertir(rol));
        }
        [Autoriza("Administrador")]
        public ActionResult Delete(int id)
        {
            

            ROLE_TABLE rol;
            using (DBContext<ROLE_TABLE> context = new DBContext<ROLE_TABLE>(new DataGYMEntities()))
            {
                rol = context.genericBM.Get(id);

            }

            return View(this.Convertir(rol));
        }

        [HttpPost]
        public ActionResult Delete(RoleViewModel RoleViewModel)
        {
            

            using (DBContext<ROLE_TABLE> context = new DBContext<ROLE_TABLE>(new DataGYMEntities()))
            {
                context.genericBM.Remove(this.Convertir(RoleViewModel));
                context.Complete();
            }

            return RedirectToAction("Index");
        }

        #region Helper

        private RoleViewModel Convertir(ROLE_TABLE role)
        {
            RoleViewModel roleViewModel = new RoleViewModel
            {

                ROLE_ID = role.ROLE_ID,
                NAME = role.NAME,
                DESCRIPTION = role.DESCRIPTION,
                IS_ACTIVE = role.IS_ACTIVE

            };
            return roleViewModel;
        }

        private ROLE_TABLE Convertir(RoleViewModel roleViewModel)
        {
            ROLE_TABLE role = new ROLE_TABLE
            {

                ROLE_ID = roleViewModel.ROLE_ID,
                NAME = roleViewModel.NAME,
                DESCRIPTION = roleViewModel.DESCRIPTION,
                IS_ACTIVE = roleViewModel.IS_ACTIVE

            };
            return role;
        }

        #endregion


    }
}