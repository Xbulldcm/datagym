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
    public class ExerciseController : Controller
    {
        [Autoriza("Administrador,Instructor,Cliente")]
        public ActionResult Index()
        {


            List<EXERCISE_TABLE> ejercicios;

            using (DBContext<EXERCISE_TABLE> context = new DBContext<EXERCISE_TABLE>(new DataGYMEntities()))
            {
                ejercicios = context.genericBM.GetAll().ToList();
            }

            List<ExerciseViewModel> lista = new List<ExerciseViewModel>();

            foreach (var item in ejercicios)
            {
                lista.Add(this.Convertir(item));
            }

            return View(lista);
        }
        [Autoriza("Administrador,Instructor")]
        public ActionResult Create()
        {


            return View();
        }

        [HttpPost]
        public ActionResult Create(ExerciseViewModel ExerciseViewModel)
        {


            EXERCISE_TABLE exercise = this.Convertir(ExerciseViewModel);

            using (DBContext<EXERCISE_TABLE> context = new DBContext<EXERCISE_TABLE>(new DataGYMEntities()))
            {

                context.genericBM.Add(exercise);
                context.Complete();
            }

            return RedirectToAction("Index");
        }


        [Autoriza("Administrador,Instructor")]
        public ActionResult Edit(int id)
        {


            EXERCISE_TABLE exercise;
            using (DBContext<EXERCISE_TABLE> context = new DBContext<EXERCISE_TABLE>(new DataGYMEntities()))
            {
                exercise = context.genericBM.Get(id);

            }

            ExerciseViewModel exerciseVM = this.Convertir(exercise);

            return View(exerciseVM);
        }


        [HttpPost]
        public ActionResult Edit(ExerciseViewModel ExerciseViewModel)
        {


            using (DBContext<EXERCISE_TABLE> context = new DBContext<EXERCISE_TABLE>(new DataGYMEntities()))
            {
                context.genericBM.Update(this.Convertir(ExerciseViewModel));
                context.Complete();
            }

            return RedirectToAction("Index");
        }
        [Autoriza("Administrador,Instructor,Cliente")]
        public ActionResult Details(int id)
        {


            EXERCISE_TABLE exercise;
            using (DBContext<EXERCISE_TABLE> context = new DBContext<EXERCISE_TABLE>(new DataGYMEntities()))
            {
                exercise = context.genericBM.Get(id);

            }

            return View(this.Convertir(exercise));
        }
        [Autoriza("Administrador,Instructor")]
        public ActionResult Delete(int id)
        {


            EXERCISE_TABLE exercise;
            using (DBContext<EXERCISE_TABLE> context = new DBContext<EXERCISE_TABLE>(new DataGYMEntities()))
            {
                exercise = context.genericBM.Get(id);

            }

            return View(this.Convertir(exercise));
        }

        [HttpPost]
        public ActionResult Delete(ExerciseViewModel ExerciseViewModel)
        {


            using (DBContext<EXERCISE_TABLE> context = new DBContext<EXERCISE_TABLE>(new DataGYMEntities()))
            {
                context.genericBM.Remove(this.Convertir(ExerciseViewModel));
                context.Complete();
            }

            return RedirectToAction("Index");
        }

        public JsonResult InsertExercises(string routine_id, List<EXERCISE_TABLE> lista_ejericios)
        {
            using (DataGYMEntities entities = new DataGYMEntities())
            {

                if (lista_ejericios == null)
                {
                    lista_ejericios = new List<EXERCISE_TABLE>();
                }

                //Loop
                foreach (EXERCISE_TABLE exercise in lista_ejericios)
                {
                    exercise.IS_ACTIVE = true;
                    exercise.ROUTINE_ID = routine_id;
                    entities.EXERCISE_TABLE.Add(exercise);
                }
                int registros_insertados = entities.SaveChanges();
                return Json(registros_insertados);
            }
        }
        
        public JsonResult GetExerciseTypeName(int id) {

            EXERCISE_TYPE_TABLE eT = new EXERCISE_TYPE_TABLE();

            using (DBContext<EXERCISE_TYPE_TABLE> context = new DBContext<EXERCISE_TYPE_TABLE>(new DataGYMEntities()))
            {
                eT = context.genericBM.Get(id);
            }

            return Json(eT.EXERCISE_TYPE_NAME);
        }

        #region Helper

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

        #endregion
    }
}
