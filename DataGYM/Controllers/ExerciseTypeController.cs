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
    public class ExerciseTypeController : Controller
    {
        public ActionResult Index()
        {


            List<EXERCISE_TYPE_TABLE> exerciseTypes;

            using (DBContext<EXERCISE_TYPE_TABLE> context = new DBContext<EXERCISE_TYPE_TABLE>(new DataGYMEntities()))
            {
                exerciseTypes = context.genericBM.GetAll().ToList();
            }

            List<ExerciseTypeViewModel> lista = new List<ExerciseTypeViewModel>();

            foreach (var item in exerciseTypes)
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
        public ActionResult Create(ExerciseTypeViewModel ExerciseTypeViewModel)
        {


            EXERCISE_TYPE_TABLE exerciseType = this.Convertir(ExerciseTypeViewModel);

            using (DBContext<EXERCISE_TYPE_TABLE> context = new DBContext<EXERCISE_TYPE_TABLE>(new DataGYMEntities()))
            {

                context.genericBM.Add(exerciseType);
                context.Complete();
            }

            return RedirectToAction("Index");
        }



        public ActionResult Edit(int id)
        {


            EXERCISE_TYPE_TABLE exerciseType;
            using (DBContext<EXERCISE_TYPE_TABLE> context = new DBContext<EXERCISE_TYPE_TABLE>(new DataGYMEntities()))
            {
                exerciseType = context.genericBM.Get(id);

            }

            ExerciseTypeViewModel exerciseTypeVM = this.Convertir(exerciseType);

            return View(exerciseTypeVM);
        }


        [HttpPost]
        public ActionResult Edit(ExerciseTypeViewModel ExerciseTypeViewModel)
        {


            using (DBContext<EXERCISE_TYPE_TABLE> context = new DBContext<EXERCISE_TYPE_TABLE>(new DataGYMEntities()))
            {
                context.genericBM.Update(this.Convertir(ExerciseTypeViewModel));
                context.Complete();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {


            EXERCISE_TYPE_TABLE exerciseType;
            using (DBContext<EXERCISE_TYPE_TABLE> context = new DBContext<EXERCISE_TYPE_TABLE>(new DataGYMEntities()))
            {
                exerciseType = context.genericBM.Get(id);

            }

            return View(this.Convertir(exerciseType));
        }

        public ActionResult Delete(int id)
        {


            EXERCISE_TYPE_TABLE exerciseType;
            using (DBContext<EXERCISE_TYPE_TABLE> context = new DBContext<EXERCISE_TYPE_TABLE>(new DataGYMEntities()))
            {
                exerciseType = context.genericBM.Get(id);

            }

            return View(this.Convertir(exerciseType));
        }

        [HttpPost]
        public ActionResult Delete(ExerciseTypeViewModel ExerciseTypeViewModel)
        {


            using (DBContext<EXERCISE_TYPE_TABLE> context = new DBContext<EXERCISE_TYPE_TABLE>(new DataGYMEntities()))
            {
                context.genericBM.Remove(this.Convertir(ExerciseTypeViewModel));
                context.Complete();
            }

            return RedirectToAction("Index");
        }

        #region Helper

        private ExerciseTypeViewModel Convertir(EXERCISE_TYPE_TABLE exerciseType)
        {
            ExerciseTypeViewModel exerciseTypeViewModel = new ExerciseTypeViewModel
            {

                EXERCISE_TYPE_ID = exerciseType.EXERCISE_TYPE_ID,
                EXERCISE_TYPE_NAME = exerciseType.EXERCISE_TYPE_NAME

            };
            return exerciseTypeViewModel;
        }

        private EXERCISE_TYPE_TABLE Convertir(ExerciseTypeViewModel exerciseTypeViewModel)
        {
            EXERCISE_TYPE_TABLE exerciseType = new EXERCISE_TYPE_TABLE
            {

                EXERCISE_TYPE_ID = exerciseTypeViewModel.EXERCISE_TYPE_ID,
                EXERCISE_TYPE_NAME = exerciseTypeViewModel.EXERCISE_TYPE_NAME

            };
            return exerciseType;
        }

        #endregion
    }
}
