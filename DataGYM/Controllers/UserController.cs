using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataGYM.Models;
using DataAccess.DB;
using BusinessModel;
using System.Configuration;
using System.Text.RegularExpressions;
using DataGYM.Tools;
using DataGYM.Filtro;

namespace DataGYM.Controllers
{
    public class UserController : Controller
    {
        [Autoriza("Administrador,Instructor,Cajero")]
        public ActionResult Index()
        {

            
            List<USER_TABLE> users;

            using (DBContext<USER_TABLE> context = new DBContext<USER_TABLE>(new DataGYMEntities()))
            {
                users = context.genericBM.GetAll().ToList();
            }

            List<UserViewModel> lista = new List<UserViewModel>();
            UserViewModel userVM = new UserViewModel();

            foreach (var item in users)
            {
                userVM = this.Convertir(item);
                using (DBContext<MEMBERSHIP_TABLE> context = new DBContext<MEMBERSHIP_TABLE>(new DataGYMEntities()))
                {
                    userVM.nombreMembresia = context.genericBM.Get(item.FKMB.GetValueOrDefault()).DESCRIPTION;
                }
                using (DBContext<ROLE_TABLE> context = new DBContext<ROLE_TABLE>(new DataGYMEntities()))
                {
                    userVM.nombreRol = context.genericBM.Get(item.FKROLE.GetValueOrDefault()).NAME;
                }
                using (DBContext<SITE_TABLE> context = new DBContext<SITE_TABLE>(new DataGYMEntities()))
                {
                    userVM.nombreSite = context.genericBM.Get(item.FKSITE.GetValueOrDefault()).LOCATION;
                }

                lista.Add(userVM);
            }

            return View(lista);
        }
        [Autoriza("Administrador,Instructor")]
        public ActionResult MisRutinas(int id) {

            
            UserViewModel user = new UserViewModel();
            List<ROUTINE_TABLE> rutinas;

            using (DBContext<ROUTINE_TABLE> context = new DBContext<ROUTINE_TABLE>(new DataGYMEntities()))
            {
                rutinas = context.genericBM.GetAll().ToList();
            }

            List<RoutineViewModel> rutinasVM = new List<RoutineViewModel>();

            foreach (var item in rutinas)
            {
                if (item.USER_ID == id)
                {
                    rutinasVM.Add(this.Convertir(item));
                }
                
            }

            user.lista_rutinasVM = rutinasVM;

            return View(user);
        }
        [Autoriza("Administrador,Instructor")]
        public ActionResult VerRutina(string id)
        {
            ROUTINE_TABLE rutina = new ROUTINE_TABLE();
            RoutineViewModel rutinaVM; ;

            List<EXERCISE_TABLE> ejercicios;

            List<ExerciseViewModel> ejerciciosVM = new List<ExerciseViewModel>();

            using (DBContext<EXERCISE_TABLE> context = new DBContext<EXERCISE_TABLE>(new DataGYMEntities()))
            {
                ejercicios = context.genericBM.GetAll().ToList();
            }

            using (DBContext<ROUTINE_TABLE> context = new DBContext<ROUTINE_TABLE>(new DataGYMEntities()))
            {
                rutina = context.genericBM.Get(id);
            }

            foreach (var item in ejercicios)
            {
                if (item.ROUTINE_ID == id)
                {
                    EXERCISE_TYPE_TABLE eT = new EXERCISE_TYPE_TABLE();

                    using (DBContext<EXERCISE_TYPE_TABLE> context = new DBContext<EXERCISE_TYPE_TABLE>(new DataGYMEntities()))
                    {
                        eT = context.genericBM.Get(item.EXERCISE_TYPE_ID.GetValueOrDefault());
                    }

                    ExerciseViewModel temp = new ExerciseViewModel();

                    temp = this.Convertir(item);
                    temp.EXERCISE_TYPE_ID_NAME = eT.EXERCISE_TYPE_NAME;

                    ejerciciosVM.Add(temp);
                }

            }



            rutinaVM = this.Convertir(rutina);

            rutinaVM.lista_ejerciciosVM = ejerciciosVM;

            return View(rutinaVM);
        }
        [Autoriza("Administrador,Instructor,Cajero")]
        public ActionResult MisPagos(int id)
        {

            List<PAY_TABLE> payments;

            using (DBContext<PAY_TABLE> context = new DBContext<PAY_TABLE>(new DataGYMEntities()))
            {
                payments = context.genericBM.GetAll().ToList();
            }

            List<PayViewModel> lista = new List<PayViewModel>();

            foreach (var item in payments)
            {
                if (item.USER_ID == id && item.IS_ACTIVE == true)
                {
                    lista.Add(this.Convertir(item));
                }
                
            }

            return View(lista);
        }
        [Autoriza("Administrador,Instructor")]
        public ActionResult Create()
        {
            UserViewModel user = new UserViewModel();

            using (DBContext<MEMBERSHIP_TABLE> context = new DBContext<MEMBERSHIP_TABLE>(new DataGYMEntities()))
            {

                user.lista_membresias = context.genericBM.GetAll().ToList();
                
            }

            using (DBContext<ROLE_TABLE> context = new DBContext<ROLE_TABLE>(new DataGYMEntities()))
            {

                user.lista_roles = context.genericBM.GetAll().ToList();

            }

            using (DBContext<SITE_TABLE> context = new DBContext<SITE_TABLE>(new DataGYMEntities()))
            {

                user.lista_sites = context.genericBM.GetAll().ToList();

            }

            return View(user);
        }

        [HttpPost]
        public ActionResult Create(UserViewModel UserViewModel)
        {

            PAY_TABLE pay = new PAY_TABLE();
            string key = ConfigurationManager.AppSettings["SecretKeyPay"];
            string payID = RemoveSpecialCharacters(Seguridad.EncryptString(key, DateTime.Now.ToString()));

            USER_TABLE user = this.Convertir(UserViewModel);

            user.IS_ACTIVE = true;
            user.CREATE_DATE = DateTime.Now;
            user.FKPAY = payID;
            user.Password = Encripta.Hash(UserViewModel.Password);

            using (DBContext<USER_TABLE> context = new DBContext<USER_TABLE>(new DataGYMEntities()))
            {
                context.genericBM.Add(user);
                context.Complete();
            }

            DataGYMEntities db = new DataGYMEntities();
            int idUsuario = db.USER_TABLE.Where(c => c.FKPAY == payID).Select(x => x.UserID).FirstOrDefault();

            using (DBContext<PAY_TABLE> context = new DBContext<PAY_TABLE>(new DataGYMEntities()))
            {
                pay.PAYMENT_ID = payID;
                pay.EXPIRES = DateTime.Now.AddMonths(1);
                pay.CREATE_DATE = DateTime.Now;
                pay.USER_ID = idUsuario;
                pay.IS_ACTIVE = true;
                context.genericBM.Add(pay);
                context.Complete();
            }
             

            return RedirectToAction("Index");
        }

        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
        }
        [Autoriza("Administrador,Instructor")]
        public ActionResult Edit(int id)
        {

            UserViewModel userVM = new UserViewModel();

            USER_TABLE user;

            using (DBContext<USER_TABLE> context = new DBContext<USER_TABLE>(new DataGYMEntities()))
            {
                user = context.genericBM.Get(id);

            }

            userVM = this.Convertir(user);


            using (DBContext<MEMBERSHIP_TABLE> context = new DBContext<MEMBERSHIP_TABLE>(new DataGYMEntities()))
            {

                userVM.lista_membresias = context.genericBM.GetAll().ToList();

            }

            using (DBContext<ROLE_TABLE> context = new DBContext<ROLE_TABLE>(new DataGYMEntities()))
            {

                userVM.lista_roles = context.genericBM.GetAll().ToList();

            }

            using (DBContext<SITE_TABLE> context = new DBContext<SITE_TABLE>(new DataGYMEntities()))
            {

                userVM.lista_sites = context.genericBM.GetAll().ToList();

            }

            return View(userVM);
        }


        [HttpPost]
        public ActionResult Edit(UserViewModel UserViewModel)
        {
            USER_TABLE user;

            using (DBContext<USER_TABLE> context = new DBContext<USER_TABLE>(new DataGYMEntities()))
            {
                user = context.genericBM.Get(UserViewModel.UserID);
                context.Complete();
            }

            user.Name = UserViewModel.Name;
            user.LastName = UserViewModel.LastName;
            user.Identification = UserViewModel.Identification;
            user.Email = UserViewModel.Email;
            if (user.Password != UserViewModel.Password) { 
                user.Password = Encripta.Hash(UserViewModel.Password);
            }
            user.Address = UserViewModel.Address;
            user.PhoneNumber = UserViewModel.PhoneNumber;
            user.ManagerName = UserViewModel.ManagerName;
            user.FKMB = UserViewModel.FKMB;
            user.FKROLE = UserViewModel.FKROLE;
            user.FKSITE = UserViewModel.FKSITE;
            user.IS_ACTIVE = UserViewModel.IS_ACTIVE;

            using (DBContext<USER_TABLE> context = new DBContext<USER_TABLE>(new DataGYMEntities()))
            {
                context.genericBM.Update(user);
                context.Complete();
            }

            return RedirectToAction("Index");
        }
        [Autoriza("Administrador,Instructor")]
        public ActionResult Details(int id)
        {


            USER_TABLE user;
            using (DBContext<USER_TABLE> context = new DBContext<USER_TABLE>(new DataGYMEntities()))
            {
                user = context.genericBM.Get(id);

            }

            return View(this.Convertir(user));
        }
        [Autoriza("Administrador,Instructor")]
        public ActionResult Delete(int id)
        {


            USER_TABLE user;
            using (DBContext<USER_TABLE> context = new DBContext<USER_TABLE>(new DataGYMEntities()))
            {
                user = context.genericBM.Get(id);

            }

            return View(this.Convertir(user));
        }

        [HttpPost]
        public ActionResult Delete(UserViewModel UserViewModel)
        {


            using (DBContext<USER_TABLE> context = new DBContext<USER_TABLE>(new DataGYMEntities()))
            {
                context.genericBM.Remove(this.Convertir(UserViewModel));
                context.Complete();
            }

            return RedirectToAction("Index");
        }

        #region Helper

        private UserViewModel Convertir(USER_TABLE user)
        {
            UserViewModel userViewModel = new UserViewModel
            {

                UserID = user.UserID,
                Name = user.Name,
                LastName = user.LastName,
                Identification = user.Identification,
                Email = user.Email,
                Password = user.Password,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                ManagerName = user.ManagerName,
                FKMB = user.FKMB,
                FKPAY = user.FKPAY,
                FKROLE = user.FKROLE,
                FKSITE = user.FKSITE,
                IS_ACTIVE = user.IS_ACTIVE,
                CREATE_DATE = user.CREATE_DATE
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
                Password = userViewModel.Password,
                Address = userViewModel.Address,
                PhoneNumber = userViewModel.PhoneNumber,
                ManagerName = userViewModel.ManagerName,
                FKMB = userViewModel.FKMB,
                FKPAY = userViewModel.FKPAY,
                FKROLE = userViewModel.FKROLE,
                FKSITE = userViewModel.FKSITE,
                IS_ACTIVE = userViewModel.IS_ACTIVE,
                CREATE_DATE = userViewModel.CREATE_DATE
            };
            return user;
        }

        private RoutineViewModel Convertir(ROUTINE_TABLE routine)
        {
            RoutineViewModel routineViewModel = new RoutineViewModel
            {

                ROUTINE_ID = routine.ROUTINE_ID,
                Name_Routine = routine.NAME_ROUTINE,
                NOTE_Routine = routine.NOTE_ROUTINE,
                USER_ID = routine.USER_ID,
                CREATE_DATE_ROUTINE = routine.CREATE_DATE_ROUTINE,
                IS_ACTIVE = routine.IS_ACTIVE

            };
            return routineViewModel;
        }

        private ROUTINE_TABLE Convertir(RoutineViewModel routineViewModel)
        {
            ROUTINE_TABLE routine = new ROUTINE_TABLE
            {

                ROUTINE_ID = routineViewModel.ROUTINE_ID,
                NAME_ROUTINE = routineViewModel.Name_Routine,
                NOTE_ROUTINE = routineViewModel.NOTE_Routine,
                USER_ID = routineViewModel.USER_ID,
                CREATE_DATE_ROUTINE = routineViewModel.CREATE_DATE_ROUTINE,
                IS_ACTIVE = routineViewModel.IS_ACTIVE

            };
            return routine;
        }

        private ExerciseViewModel Convertir(EXERCISE_TABLE exercise)
        {
            ExerciseViewModel exerciseViewModel = new ExerciseViewModel
            {

                EXERCISE_ID = exercise.EXERCISE_ID,
                EXERCISE_TYPE_ID = exercise.EXERCISE_TYPE_ID.GetValueOrDefault(),
                EXERCISE_DAY = exercise.EXERCISE_DAY,
                EXERCISE_NAME = exercise.EXERCISE_NAME,
                EXERCISE_REP_COUNT = exercise.EXERCISE_REP_COUNT.GetValueOrDefault(),
                EXERCISE_REP_ROUND = exercise.EXERCISE_REP_ROUND.GetValueOrDefault(),
                EXECISE_NOTE = exercise.EXECISE_NOTE,
                IS_ACTIVE = exercise.IS_ACTIVE

            };
            return exerciseViewModel;
        }

        private EXERCISE_TABLE Convertir(ExerciseViewModel exerciseViewModel)
        {
            EXERCISE_TABLE exercise = new EXERCISE_TABLE
            {

                EXERCISE_ID = exerciseViewModel.EXERCISE_ID,
                EXERCISE_TYPE_ID = exerciseViewModel.EXERCISE_TYPE_ID,
                EXERCISE_DAY = exerciseViewModel.EXERCISE_DAY,
                EXERCISE_NAME = exerciseViewModel.EXERCISE_NAME,
                EXERCISE_REP_COUNT = exerciseViewModel.EXERCISE_REP_COUNT,
                EXERCISE_REP_ROUND = exerciseViewModel.EXERCISE_REP_ROUND,
                EXECISE_NOTE = exerciseViewModel.EXECISE_NOTE,
                IS_ACTIVE = exerciseViewModel.IS_ACTIVE

            };
            return exercise;
        }

        private PayViewModel Convertir(PAY_TABLE pay)
        {
            PayViewModel payViewModel = new PayViewModel
            {

                PAYMENT_ID = pay.PAYMENT_ID,
                PAYMENT_TYPE = pay.PAYMENT_TYPE,
                CARD_NUMBER = pay.CARD_NUMBER,
                EXPIRES = pay.EXPIRES

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
                EXPIRES = payViewModel.EXPIRES

            };
            return pay;
        }

        #endregion
    }
}
