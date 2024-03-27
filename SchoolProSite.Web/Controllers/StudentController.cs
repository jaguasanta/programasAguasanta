using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProSite.DAL.Entities;
using SchoolProSite.DAL.Exceptions;
using SchoolProSite.DAL.Interfaces;
using SchoolProSite.Web.Models;

namespace SchoolProSite.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly IDaoStudent daoStudent;

        public StudentController(IDaoStudent daoStudent)
        {
            this.daoStudent = daoStudent;
        }

        // GET: StudentController
        public ActionResult Index()
        {
            var students = this.daoStudent
                               .GetStudents()
                               .Select(cd => new Models.StudentModel()
                               {
                                   Id = cd.Id,
                                   LastName = cd.LastName,
                                   FirstName = cd.FirstName,
                                   EnrollmentDate = cd.EnrollmentDate
                               }

                               );

            return View(students);
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            var student = this.daoStudent.GetStudent(id);

            var modelStudent = new Models.StudentModel()
            {

                Id = student.Id,
                LastName = student.LastName,
                FirstName = student.FirstName,
                EnrollmentDate = student.EnrollmentDate

            };

            return View(modelStudent);
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.StudentModel studentModel)
        {
            try
            {
                Student student = new Student()
                {
                    LastName = studentModel.LastName,
                    FirstName = studentModel.FirstName,
                    EnrollmentDate = studentModel.EnrollmentDate,
                    CreationUser = 1,
                    CreationDate = DateTime.Now
                 };

                this.daoStudent.SaveStudent(student);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            var student = this.daoStudent.GetStudent(id);

            var modelStudent = new Models.StudentModel()
            {

                Id = student.Id,
                LastName = student.LastName,
                FirstName = student.FirstName,  
                EnrollmentDate = student.EnrollmentDate
            };

            return View(modelStudent);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.StudentModel studentModel)
        {
            try
            {
                Student student = new Student()
                {
                    LastName = studentModel.LastName,
                    FirstName = studentModel.FirstName,
                    UserMod = 1,
                    EnrollmentDate = studentModel.EnrollmentDate,
                    Id = studentModel.Id,
                    ModifyDate = DateTime.Now
                };

                this.daoStudent.UpdateStudent(student);

                return RedirectToAction(nameof(Index));
            }
            catch (DaoStudentException1 daoEx)
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
