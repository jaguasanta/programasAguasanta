using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProSite.DAL.Entities;
using SchoolProSite.DAL.Exceptions;
using SchoolProSite.DAL.Interfaces;
using SchoolProSite.Web.Models;

namespace SchoolProSite.Web.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IDaoInstructor daoInstructor;

        public InstructorController(IDaoInstructor daoInstructor)
        {
            this.daoInstructor = daoInstructor;
        }

        // GET: InstructorController
        public ActionResult Index()
        {
            var instructor = this.daoInstructor
                               .GetInstructors()
                               .Select(cd => new Models.InstructorModel()
                               {
                                   Id = cd.Id,
                                   LastName = cd.LastName,
                                   FirstName = cd.FirstName,
                                   HireDate = cd.HireDate
                               }

                               );
            return View(instructor);
        }

        // GET: InstructorController/Details/5
        public ActionResult Details(int id)
        {
            var instructor = this.daoInstructor.GetInstructor(id);

            var modelInstructor = new Models.InstructorModel()
            {

                Id = instructor.Id,
                LastName = instructor.LastName,
                FirstName = instructor.FirstName,
                HireDate = instructor.HireDate

            };

            return View(modelInstructor);
        }

        // GET: InstructorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InstructorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.InstructorModel instructorModel)
        {
            try
            {
                Instructor instructor = new Instructor()
                {
                    LastName = instructorModel.LastName,
                    FirstName = instructorModel.FirstName,
                    HireDate = instructorModel.HireDate,
                    CreationUser = 1,
                    CreationDate = DateTime.Now
                };

                this.daoInstructor.SaveInstructor(instructor);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InstructorController/Edit/5
        public ActionResult Edit(int id)
        {
            var instructor = this.daoInstructor.GetInstructor(id);

            var modelInstructor = new Models.InstructorModel()
            {

                Id = instructor.Id,
                LastName = instructor.LastName,
                FirstName = instructor.FirstName,
                HireDate = instructor.HireDate
            };

            return View(modelInstructor);
        }

        // POST: InstructorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.InstructorModel instructorModel)
        {
            try
            {
                Instructor instructor =  new Instructor()
                {
                    LastName = instructorModel.LastName,
                    FirstName = instructorModel.FirstName,
                    UserMod = 1,
                    HireDate = instructorModel.HireDate,
                    Id = instructorModel.Id,
                    ModifyDate = DateTime.Now
                };

                this.daoInstructor.UpdateInstructor(instructor);

                return RedirectToAction(nameof(Index));
            }
            catch (DaoInstructorException daoEx)
            {
                ViewBag.Message = daoEx.Message;
                return View();
            }
            catch
            {
                return View();
            }
        }

    }
}
