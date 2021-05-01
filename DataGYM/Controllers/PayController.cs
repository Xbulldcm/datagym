using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataGYM.Models;
using DataAccess.DB;
using BusinessModel;
using DataGYM.Filtro;
using System.Text.RegularExpressions;
using System.Configuration;
using DataGYM.Tools;

namespace DataGYM.Controllers
{
    public class PayController : Controller
    {
        [Autoriza("Administrador,Cliente")]
        public ActionResult Index()
        {


            List<PAY_TABLE> payments;

            using (DBContext<PAY_TABLE> context = new DBContext<PAY_TABLE>(new DataGYMEntities()))
            {
                payments = context.genericBM.GetAll().ToList();
            }

            List<PayViewModel> lista = new List<PayViewModel>();

            foreach (var item in payments)
            {
                lista.Add(this.Convertir(item));
            }

            return View(lista);
        }
        [Autoriza("Administrador,Cliente")]
        public ActionResult Pagos()
        {
            PayViewModel p = new PayViewModel();

            List<USER_TABLE> usuarios;

            using (DBContext<USER_TABLE> context = new DBContext<USER_TABLE>(new DataGYMEntities()))
            {
                usuarios = context.genericBM.GetAll().ToList();
            }

            List<UserViewModel> lista = new List<UserViewModel>();

            foreach (var item in usuarios)
            {
                lista.Add(this.Convertir(item));
            }

            p.lista_usuariosVM = lista;

            return View(p);
        }
        [Autoriza("Administrador,Cliente")]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult GetPagosUsuario(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PayViewModel PayViewModel)
        {


            PAY_TABLE pay = this.Convertir(PayViewModel);

            using (DBContext<PAY_TABLE> context = new DBContext<PAY_TABLE>(new DataGYMEntities()))
            {

                context.genericBM.Add(pay);
                context.Complete();
            }

            return RedirectToAction("Index");
        }


        [Autoriza("Administrador,Cliente,Cajero")]
        public ActionResult Edit(string id)
        {

            PAY_TABLE pay;
            using (DBContext<PAY_TABLE> context = new DBContext<PAY_TABLE>(new DataGYMEntities()))
            {
                pay = context.genericBM.Get(id);

            }

            PayViewModel payVM = this.Convertir(pay);

            return View(payVM);
        }


        [HttpPost]
        public ActionResult Edit(PayViewModel PayViewModel)
        {
            PAY_TABLE pay = new PAY_TABLE();

            using (DBContext<PAY_TABLE> context = new DBContext<PAY_TABLE>(new DataGYMEntities()))
            {
                pay = context.genericBM.Get(PayViewModel.PAYMENT_ID);

            }

            using (DBContext<PAY_TABLE> context = new DBContext<PAY_TABLE>(new DataGYMEntities()))
            {
                pay.PAYMENT_TYPE = PayViewModel.PAYMENT_TYPE;
                pay.CARD_NUMBER = PayViewModel.CARD_NUMBER;
                pay.IS_ACTIVE = PayViewModel.IS_ACTIVE;
                if (PayViewModel.IS_ACTIVE == false)
                {
                    pay.PAID_DATE = DateTime.Now;
                }
                context.genericBM.Update(pay);
                context.Complete();
            }

            if (PayViewModel.IS_ACTIVE == false) { 

                string key = ConfigurationManager.AppSettings["SecretKeyPay"];
                string payID = RemoveSpecialCharacters(Seguridad.EncryptString(key, DateTime.Now.ToString()));

                using (DBContext<PAY_TABLE> context = new DBContext<PAY_TABLE>(new DataGYMEntities()))
                {
                    pay.PAYMENT_ID = payID;
                    pay.EXPIRES = pay.EXPIRES.AddMonths(1);
                    pay.CREATE_DATE = DateTime.Now;
                    pay.CARD_NUMBER = "";
                    pay.PAYMENT_TYPE = "";
                    pay.PAID_DATE = null;
                    pay.IS_ACTIVE = true;
                    context.genericBM.Add(pay);
                    context.Complete();
                }
            }

            

            return RedirectToAction("Index","Home");
        }

        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
        }

        [Autoriza("Administrador,Cliente")]
        public ActionResult Details(string id)
        {


            PAY_TABLE pay;
            using (DBContext<PAY_TABLE> context = new DBContext<PAY_TABLE>(new DataGYMEntities()))
            {
                pay = context.genericBM.Get(id);

            }

            return View(this.Convertir(pay));
        }
        [Autoriza("Administrador,Cliente")]
        public ActionResult Delete(string id)
        {


            PAY_TABLE pay;
            using (DBContext<PAY_TABLE> context = new DBContext<PAY_TABLE>(new DataGYMEntities()))
            {
                pay = context.genericBM.Get(id);

            }

            return View(this.Convertir(pay));
        }

        [HttpPost]
        public ActionResult Delete(PayViewModel PayViewModel)
        {


            using (DBContext<PAY_TABLE> context = new DBContext<PAY_TABLE>(new DataGYMEntities()))
            {
                context.genericBM.Remove(this.Convertir(PayViewModel));
                context.Complete();
            }

            return RedirectToAction("Index");
        }

        #region Helper

        private PayViewModel Convertir(PAY_TABLE pay)
        {
            PayViewModel payViewModel = new PayViewModel
            {

                PAYMENT_ID = pay.PAYMENT_ID,
                PAYMENT_TYPE = pay.PAYMENT_TYPE,
                CARD_NUMBER = pay.CARD_NUMBER,
                EXPIRES = pay.EXPIRES,
                IS_ACTIVE = pay.IS_ACTIVE

            };
            return payViewModel;
        }

        private PAY_TABLE Convertir(PayViewModel payViewModel)
        {
            PAY_TABLE pay = new PAY_TABLE
            {

                PAYMENT_ID = payViewModel.PAYMENT_ID,
                PAYMENT_TYPE = payViewModel.PAYMENT_TYPE,
                CARD_NUMBER = payViewModel.CARD_NUMBER,
                EXPIRES = payViewModel.EXPIRES,
                IS_ACTIVE = payViewModel.IS_ACTIVE
            };
            return pay;
        }

        private UserViewModel Convertir(USER_TABLE user)
        {
            UserViewModel userViewModel = new UserViewModel
            {

                UserID = user.UserID,
                Name = user.Name,
                LastName = user.LastName,
                Identification = user.Identification,
                Email = user.Email,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                ManagerName = user.ManagerName,
                FKMB = user.FKMB,
                FKPAY = user.FKPAY,
                FKROLE = user.FKROLE,
                FKSITE = user.FKSITE,
                IS_ACTIVE = user.IS_ACTIVE

            };
            return userViewModel;
        }

        private USER_TABLE Convertir(UserViewModel userViewModel)
        {
            USER_TABLE user = new USER_TABLE
            {

                UserID = userViewModel.UserID,
                Name = userViewModel.Name,
                LastName = userViewModel.LastName,
                Identification = userViewModel.Identification,
                Email = userViewModel.Email,
                Address = userViewModel.Address,
                PhoneNumber = userViewModel.PhoneNumber,
                ManagerName = userViewModel.ManagerName,
                FKMB = userViewModel.FKMB,
                FKPAY = userViewModel.FKPAY,
                FKROLE = userViewModel.FKROLE,
                FKSITE = userViewModel.FKSITE,
                IS_ACTIVE = userViewModel.IS_ACTIVE

            };
            return user;
        }

        #endregion
    }
}
