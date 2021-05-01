using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataGYM.Models;
using DataGYM.Tools;
using DataAccess.DB;
using BusinessModel;
using System.Configuration;
using System.Text.RegularExpressions;
using DataGYM.Filtro;

namespace DataGYM.Controllers
{
    public class RoutineController : Controller
    {
        [Autoriza("Administrador,Instructor")]
        public ActionResult Index()
        {

            RoutineViewModel rutina = new RoutineViewModel();
            List<ROUTINE_TABLE> routines;

            using (DBContext<ROUTINE_TABLE> context = new DBContext<ROUTINE_TABLE>(new DataGYMEntities()))
            {
                routines = context.genericBM.GetAll().ToList();
            }

            List<RoutineViewModel> lista = new List<RoutineViewModel>();

            foreach (var item in routines)
            {
                rutina = this.Convertir(item);
                using (DBContext<ROUTINE_TABLE> context = new DBContext<ROUTINE_TABLE>(new DataGYMEntities()))
                {
                    routines = context.genericBM.GetAll().ToList();
                }
                using (DBContext<USER_TABLE> context = new DBContext<USER_TABLE>(new DataGYMEntities()))
                {
                    rutina.nombreUsuario = context.genericBM.Get(item.USER_ID).Identification + " - " +
                                            context.genericBM.Get(item.USER_ID).Name + " " +
                                            context.genericBM.Get(item.USER_ID).LastName;
                }
                lista.Add(rutina);
            }

            return View(lista);
        }


        [Autoriza("Administrador,Instructor")]
        public ActionResult Create()
        {
            RoutineViewModel routine = new RoutineViewModel();
            List<UserViewModel> lista_usuariosVM = new List<UserViewModel>();
            List<USER_TABLE> usuarios;
            List<EXERCISE_TABLE> lista_ejercicios = new List<EXERCISE_TABLE>();
            routine.lista_ejercicios = lista_ejercicios;
            using (DBContext<EXERCISE_TYPE_TABLE> context = new DBContext<EXERCISE_TYPE_TABLE>(new DataGYMEntities()))
            {

                routine.lista_tipos_ejercicios = context.genericBM.GetAll().ToList();

            }
            
            using (DBContext<USER_TABLE> context = new DBContext<USER_TABLE>(new DataGYMEntities()))
            {
                usuarios = context.genericBM.GetAll().ToList();
                
            }
            UserViewModel temp;
            foreach (var item in usuarios)
            {
                temp = new UserViewModel();
                temp.UserID = item.UserID;
                temp.nombreCompletoIdent = item.Identification + " - " + item.Name + " " + item.LastName;
                lista_usuariosVM.Add(temp);
            }

            routine.lista_usuarios = lista_usuariosVM;

            return View(routine);
        }

        [HttpPost]
        public ActionResult Create(RoutineViewModel RoutineViewModel)
        {


            ROUTINE_TABLE routine = this.Convertir(RoutineViewModel);

            using (DBContext<ROUTINE_TABLE> context = new DBContext<ROUTINE_TABLE>(new DataGYMEntities()))
            {

                context.genericBM.Add(routine);
                context.Complete();
            }

            return RedirectToAction("Index","Home");
        }


        [Autoriza("Administrador,Instructor")]
        public ActionResult Edit(string id)
        {


            ROUTINE_TABLE routine;
            using (DBContext<ROUTINE_TABLE> context = new DBContext<ROUTINE_TABLE>(new DataGYMEntities()))
            {
                routine = context.genericBM.Get(id);

            }

            RoutineViewModel routineVM = this.Convertir(routine);

            return View(routineVM);
        }


        [HttpPost]
        public ActionResult Edit(RoutineViewModel RoutineViewModel)
        {


            using (DBContext<ROUTINE_TABLE> context = new DBContext<ROUTINE_TABLE>(new DataGYMEntities()))
            {
                context.genericBM.Update(this.Convertir(RoutineViewModel));
                context.Complete();
            }

            return RedirectToAction("Index");
        }
        [Autoriza("Administrador,Instructor")]
        public ActionResult Details(int id)
        {


            ROUTINE_TABLE routine;
            using (DBContext<ROUTINE_TABLE> context = new DBContext<ROUTINE_TABLE>(new DataGYMEntities()))
            {
                routine = context.genericBM.Get(id);

            }

            return View(this.Convertir(routine));
        }
        [Autoriza("Administrador,Instructor")]
        public ActionResult Delete(int id)
        {


            ROUTINE_TABLE routine;
            using (DBContext<ROUTINE_TABLE> context = new DBContext<ROUTINE_TABLE>(new DataGYMEntities()))
            {
                routine = context.genericBM.Get(id);

            }

            return View(this.Convertir(routine));
        }

        [HttpPost]
        public ActionResult Delete(RoutineViewModel RoutineViewModel)
        {


            using (DBContext<ROUTINE_TABLE> context = new DBContext<ROUTINE_TABLE>(new DataGYMEntities()))
            {
                context.genericBM.Remove(this.Convertir(RoutineViewModel));
                context.Complete();
            }

            return RedirectToAction("Index");
        }

        public JsonResult InsertRoutine(ROUTINE_TABLE routine)
        {
            using (DataGYMEntities entities = new DataGYMEntities())
            {

                if (routine == null)
                {
                    routine = new ROUTINE_TABLE();
                }
                string key = ConfigurationManager.AppSettings["SecretKeyRoutine"];
                //routine.ROUTINE_ID = DateTime.Now.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss.fffffffK").Replace(":", randN())
                //                                                                                    .Replace("-", randN())
                //                                                                                    .Replace(".", randN())
                //                                                                                    .Replace("’", "")
                //                                                                                    .Replace("‘", "");
                routine.ROUTINE_ID = RemoveSpecialCharacters(Seguridad.EncryptString(key,DateTime.Now.ToString()));
                routine.IS_ACTIVE = true;
                routine.CREATE_DATE_ROUTINE = DateTime.Now;
                entities.ROUTINE_TABLE.Add(routine);
                entities.SaveChanges();


                return Json(routine.ROUTINE_ID);
            }
        }
        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
        }
        private string randN() {
            Random r = new Random();
            return r.Next().ToString();
        }

        #region Helper

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

        #endregion
    }
}
