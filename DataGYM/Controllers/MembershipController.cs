using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataGYM.Models;
using DataAccess.DB;
using BusinessModel;

namespace DataGYM.Controllers
{
    public class MembershipController : Controller
    {
        public ActionResult Index()
        {


            List<MEMBERSHIP_TABLE> memberships;

            using (DBContext<MEMBERSHIP_TABLE> context = new DBContext<MEMBERSHIP_TABLE>(new DataGYMEntities()))
            {
                memberships = context.genericBM.GetAll().ToList();
            }

            List<MembershipViewModel> lista = new List<MembershipViewModel>();

            foreach (var item in memberships)
            {
                lista.Add(this.Convertir(item));
            }

            return View(lista);
        }

        public ActionResult Create()
        {


            return View();
        }

        [HttpPost]
        public ActionResult Create(MembershipViewModel MembershipViewModel)
        {


            MEMBERSHIP_TABLE membership = this.Convertir(MembershipViewModel);

            using (DBContext<MEMBERSHIP_TABLE> context = new DBContext<MEMBERSHIP_TABLE>(new DataGYMEntities()))
            {

                context.genericBM.Add(membership);
                context.Complete();
            }

            return RedirectToAction("Index");
        }



        public ActionResult Edit(int id)
        {


            MEMBERSHIP_TABLE membership;
            using (DBContext<MEMBERSHIP_TABLE> context = new DBContext<MEMBERSHIP_TABLE>(new DataGYMEntities()))
            {
                membership = context.genericBM.Get(id);

            }

            MembershipViewModel membershipVM = this.Convertir(membership);

            return View(membershipVM);
        }


        [HttpPost]
        public ActionResult Edit(MembershipViewModel MembershipViewModel)
        {


            using (DBContext<MEMBERSHIP_TABLE> context = new DBContext<MEMBERSHIP_TABLE>(new DataGYMEntities()))
            {
                context.genericBM.Update(this.Convertir(MembershipViewModel));
                context.Complete();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {


            MEMBERSHIP_TABLE membership;
            using (DBContext<MEMBERSHIP_TABLE> context = new DBContext<MEMBERSHIP_TABLE>(new DataGYMEntities()))
            {
                membership = context.genericBM.Get(id);

            }

            return View(this.Convertir(membership));
        }

        public ActionResult Delete(int id)
        {


            MEMBERSHIP_TABLE membership;
            using (DBContext<MEMBERSHIP_TABLE> context = new DBContext<MEMBERSHIP_TABLE>(new DataGYMEntities()))
            {
                membership = context.genericBM.Get(id);

            }

            return View(this.Convertir(membership));
        }

        [HttpPost]
        public ActionResult Delete(MembershipViewModel MembershipViewModel)
        {


            using (DBContext<MEMBERSHIP_TABLE> context = new DBContext<MEMBERSHIP_TABLE>(new DataGYMEntities()))
            {
                context.genericBM.Remove(this.Convertir(MembershipViewModel));
                context.Complete();
            }

            return RedirectToAction("Index");
        }

        #region Helper

        private MembershipViewModel Convertir(MEMBERSHIP_TABLE membership)
        {
            MembershipViewModel membershipViewModel = new MembershipViewModel
            {

                MEMBERSHIP_ID = membership.MEMBERSHIP_ID,
                DESCRIPTION = membership.DESCRIPTION,
                MEM_TYPE = membership.MEM_TYPE

            };
            return membershipViewModel;
        }

        private MEMBERSHIP_TABLE Convertir(MembershipViewModel membershipViewModel)
        {
            MEMBERSHIP_TABLE membership = new MEMBERSHIP_TABLE
            {

                MEMBERSHIP_ID = membershipViewModel.MEMBERSHIP_ID,
                DESCRIPTION = membershipViewModel.DESCRIPTION,
                MEM_TYPE = membershipViewModel.MEM_TYPE

            };
            return membership;
        }

        #endregion
    }
}
